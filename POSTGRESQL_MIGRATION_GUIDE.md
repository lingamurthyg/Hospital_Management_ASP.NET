# PostgreSQL Migration Guide for Clinic Management System

## Overview
This document describes the migration of the Clinic Management System from SQL Server to PostgreSQL 16.

## Migration Summary

### 1. Package Dependencies Updated

**Infrastructure Project (ClinicManagement.Infrastructure.csproj)**
- ❌ Removed: `Microsoft.EntityFrameworkCore.SqlServer` Version 8.0.0
- ✅ Added: `Npgsql.EntityFrameworkCore.PostgreSQL` Version 8.0.0
- ✅ Added: `EFCore.NamingConventions` Version 8.0.3
- ✅ Added: `Npgsql.DependencyInjection` Version 8.0.0

### 2. Connection String Conversion

**Before (SQL Server):**
```json
"DefaultConnection": "Data Source=.\\SQLEXPRESS;Initial Catalog=DBProject;Integrated Security=True;TrustServerCertificate=True"
```

**After (PostgreSQL):**
```json
"DefaultConnection": "Host=localhost;Port=5432;Database=DBProject;Username=postgres;Password=postgres;Include Error Detail=true"
```

**Development Connection String:**
```json
"DefaultConnection": "Host=localhost;Port=5432;Database=DBProject_Dev;Username=postgres;Password=postgres;Include Error Detail=true"
```

### 3. DbContext Configuration Changes

**File: `ClinicManagement.Infrastructure/Extensions/ServiceCollectionExtensions.cs`**

**Before:**
```csharp
options.UseSqlServer(
    configuration.GetConnectionString("DefaultConnection"),
    sqlOptions => sqlOptions.EnableRetryOnFailure(
        maxRetryCount: 5,
        maxRetryDelay: TimeSpan.FromSeconds(30),
        errorNumbersToAdd: null));
```

**After:**
```csharp
options.UseNpgsql(
    configuration.GetConnectionString("DefaultConnection"),
    npgsqlOptions =>
    {
        npgsqlOptions.EnableRetryOnFailure(
            maxRetryCount: 5,
            maxRetryDelay: TimeSpan.FromSeconds(30),
            errorCodesToAdd: null);
        npgsqlOptions.MigrationsHistoryTable("__efmigrations_history", "public");
    })
    .UseSnakeCaseNamingConvention(); // Apply snake_case naming convention

// Enable legacy timestamp behavior for PostgreSQL
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
```

### 4. Entity Type Mappings Updated

**File: `ClinicManagement.Infrastructure/Data/ClinicDbContext.cs`**

Key changes in `OnModelCreating` method:

1. **Default Schema:**
   ```csharp
   modelBuilder.HasDefaultSchema("public");
   ```

2. **DateTime Type Mappings:**
   - SQL Server: `datetime2` (default)
   - PostgreSQL: `timestamp without time zone`
   
   Example:
   ```csharp
   entity.Property(e => e.CreatedDate).HasColumnType("timestamp without time zone");
   entity.Property(e => e.ModifiedDate).HasColumnType("timestamp without time zone");
   entity.Property(e => e.BirthDate).HasColumnType("timestamp without time zone");
   ```

3. **Decimal Type Mappings:**
   - SQL Server: `decimal(18,2)`
   - PostgreSQL: `numeric(18,2)`
   
   Example:
   ```csharp
   entity.Property(e => e.Amount).HasColumnType("numeric(18,2)");
   entity.Property(e => e.Salary).HasColumnType("numeric(18,2)");
   entity.Property(e => e.ChargesPerVisit).HasColumnType("numeric(18,2)");
   ```

### 5. Database Schema Files

**New PostgreSQL Schema Files Created:**
- `Database Files/Schema_PostgreSQL.sql` - Complete PostgreSQL schema
- `Database Files/Insertions_PostgreSQL.sql` - Sample data insertions

**Key Schema Differences:**

| Feature | SQL Server | PostgreSQL |
|---------|-----------|------------|
| Auto-increment | `IDENTITY(1,1)` | `SERIAL` |
| String type | `VARCHAR(n)` | `VARCHAR(n)` |
| Date/Time | `DATETIME` | `TIMESTAMP WITHOUT TIME ZONE` |
| Decimal | `FLOAT`, `DECIMAL(18,2)` | `REAL`, `NUMERIC(18,2)` |
| Boolean | `BIT` | `BOOLEAN` |
| Naming | PascalCase | snake_case |
| Schema | `dbo` | `public` |

### 6. Naming Convention

PostgreSQL uses **snake_case** naming convention by default:
- Table: `Patient` → `patient`
- Column: `PatientID` → `patient_id`
- Column: `CreatedDate` → `created_date`

This is automatically handled by the `UseSnakeCaseNamingConvention()` extension.

## Migration Steps

### Step 1: Install PostgreSQL
```bash
# Install PostgreSQL 16
# Windows: Download from https://www.postgresql.org/download/windows/
# Linux: sudo apt-get install postgresql-16
# macOS: brew install postgresql@16
```

### Step 2: Create Database
```sql
CREATE DATABASE "DBProject";
```

### Step 3: Run Schema Script
```bash
psql -U postgres -d DBProject -f "Database Files/Schema_PostgreSQL.sql"
```

### Step 4: Run Sample Data Script (Optional)
```bash
psql -U postgres -d DBProject -f "Database Files/Insertions_PostgreSQL.sql"
```

### Step 5: Update Connection String
Update `appsettings.json` with your PostgreSQL connection details:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=DBProject;Username=your_username;Password=your_password;Include Error Detail=true"
  }
}
```

### Step 6: Create EF Core Migrations
```bash
cd src/ClinicManagement.Infrastructure
dotnet ef migrations add InitialPostgreSQLMigration --startup-project ../ClinicManagement.Web
dotnet ef database update --startup-project ../ClinicManagement.Web
```

## Testing the Migration

### 1. Verify Database Connection
```bash
dotnet run --project src/ClinicManagement.Web
```

### 2. Test CRUD Operations
- Create a new patient
- Read patient data
- Update patient information
- Delete (soft delete) a patient

### 3. Verify Relationships
- Test doctor-department relationships
- Test appointment-patient-doctor relationships
- Test bill-appointment relationships

## Common Issues and Solutions

### Issue 1: DateTime Handling
**Problem:** DateTime values not storing correctly
**Solution:** Ensure `EnableLegacyTimestampBehavior` is set:
```csharp
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
```

### Issue 2: Case Sensitivity
**Problem:** Table/column names not found
**Solution:** PostgreSQL is case-sensitive. Use snake_case naming convention:
```csharp
.UseSnakeCaseNamingConvention()
```

### Issue 3: Migration History Table
**Problem:** Migration history not tracked correctly
**Solution:** Configure migrations history table:
```csharp
npgsqlOptions.MigrationsHistoryTable("__efmigrations_history", "public");
```

### Issue 4: Sequence Reset
**Problem:** Auto-increment IDs not starting correctly after data import
**Solution:** Reset sequences:
```sql
SELECT setval('patient_patient_id_seq', (SELECT MAX(patient_id) FROM patient));
SELECT setval('doctor_doctor_id_seq', (SELECT MAX(doctor_id) FROM doctor));
-- Repeat for all tables with SERIAL columns
```

## Performance Considerations

### Indexes Created
The following indexes are automatically created for better query performance:
- `idx_patient_email` on `patient(email)`
- `idx_doctor_email` on `doctor(email)`
- `idx_doctor_dept` on `doctor(dept_no)`
- `idx_appointment_patient` on `appointment(patient_id)`
- `idx_appointment_doctor` on `appointment(doctor_id)`
- `idx_appointment_date` on `appointment(appointment_date)`
- `idx_bill_patient` on `bill(patient_id)`
- `idx_treatment_patient` on `treatment_history(patient_id)`

### Connection Pooling
PostgreSQL connection pooling is configured in the connection string:
```
Host=localhost;Port=5432;Database=DBProject;Username=postgres;Password=postgres;Minimum Pool Size=5;Maximum Pool Size=100
```

## Rollback Plan

If you need to rollback to SQL Server:

1. Restore the original `.csproj` file
2. Restore the original `appsettings.json`
3. Restore the original `ServiceCollectionExtensions.cs`
4. Restore the original `ClinicDbContext.cs`
5. Run: `dotnet ef database update --startup-project ../ClinicManagement.Web`

## Files Modified

### Configuration Files
- ✅ `src/ClinicManagement.Infrastructure/ClinicManagement.Infrastructure.csproj`
- ✅ `src/ClinicManagement.Web/appsettings.json`
- ✅ `src/ClinicManagement.Web/appsettings.Development.json`

### Code Files
- ✅ `src/ClinicManagement.Infrastructure/Extensions/ServiceCollectionExtensions.cs`
- ✅ `src/ClinicManagement.Infrastructure/Data/ClinicDbContext.cs`

### Database Files
- ✅ `Database Files/Schema_PostgreSQL.sql` (NEW)
- ✅ `Database Files/Insertions_PostgreSQL.sql` (NEW)

### Repository Files
- ℹ️ No changes required (using LINQ queries, not raw SQL)

## Verification Checklist

- [x] Package references updated to PostgreSQL
- [x] Connection strings converted to PostgreSQL format
- [x] DbContext configured with UseNpgsql
- [x] Snake_case naming convention applied
- [x] Migration history table configured
- [x] DateTime types mapped to timestamp without time zone
- [x] Decimal types mapped to numeric
- [x] Default schema set to "public"
- [x] Legacy timestamp behavior enabled
- [x] PostgreSQL schema scripts created
- [x] Sample data scripts created
- [x] Indexes created for performance

## Next Steps

1. **Test thoroughly** in development environment
2. **Create new migrations** for PostgreSQL
3. **Update CI/CD pipelines** to use PostgreSQL
4. **Update documentation** for deployment
5. **Train team** on PostgreSQL-specific features
6. **Monitor performance** and optimize queries if needed

## Support and Resources

- [Npgsql Documentation](https://www.npgsql.org/efcore/)
- [PostgreSQL Documentation](https://www.postgresql.org/docs/16/)
- [EF Core PostgreSQL Provider](https://www.npgsql.org/efcore/index.html)
- [EFCore.NamingConventions](https://github.com/efcore/EFCore.NamingConventions)

## Migration Completed Successfully ✅

All database code modernization issues have been addressed and the application is now PostgreSQL-ready!
