# PostgreSQL Migration Guide for ClinicManagement System

## Overview
This document provides instructions for completing the migration from SQL Server to PostgreSQL for the ClinicManagement application.

## Migration Status: COMPLETED

### Changes Applied

#### 1. Package Dependencies Updated ✅
**File**: `ClinicManagement.Infrastructure/ClinicManagement.Infrastructure.csproj`

**Changes**:
- ❌ Removed: `Microsoft.EntityFrameworkCore.SqlServer` Version 8.0.0
- ✅ Added: `Npgsql.EntityFrameworkCore.PostgreSQL` Version 8.0.0
- ✅ Added: `EFCore.NamingConventions` Version 8.0.3
- ✅ Added: `Microsoft.EntityFrameworkCore.Relational` Version 8.0.0

#### 2. Connection Strings Updated ✅
**Files**: 
- `ClinicManagement.Web/appsettings.json`
- `ClinicManagement.Web/appsettings.Development.json`

**Old Connection String** (SQL Server):
```
Data Source=.\\SQLEXPRESS;Initial Catalog=DBProject;Integrated Security=True;TrustServerCertificate=True
```

**New Connection String** (PostgreSQL):
```
Host=localhost;Port=5432;Database=DBProject;Username=postgres;Password=postgres;Pooling=true;Minimum Pool Size=0;Maximum Pool Size=100;Connection Lifetime=0;Command Timeout=30;Timeout=15;
```

**Development Database**: `DBProject_Dev`

#### 3. DbContext Configuration Updated ✅
**File**: `ClinicManagement.Infrastructure/Extensions/ServiceCollectionExtensions.cs`

**Changes**:
- Replaced `UseSqlServer()` with `UseNpgsql()`
- Added snake_case naming convention with `UseSnakeCaseNamingConvention()`
- Configured retry policy with 5 retries and 30-second max delay
- Set migrations history table to `__ef_migrations_history` in `public` schema
- Added proper migration assembly configuration

#### 4. Entity Configuration for PostgreSQL ✅
**File**: `ClinicManagement.Infrastructure/Data/ClinicDbContext.cs`

**Changes**:
- Set default schema to `public`
- Configured all DateTime properties to use `timestamp without time zone`
- Added proper column type mappings for decimal fields (decimal(18,2))
- Configured string length constraints for all text fields
- Added unique indexes on email fields
- Set default values using PostgreSQL syntax (`CURRENT_TIMESTAMP`)
- Implemented `ConfigureConventions()` for global DateTime handling

#### 5. Entity Type Mappings ✅

All entities have been configured with PostgreSQL-compatible types:

| .NET Type | PostgreSQL Type | Configuration |
|-----------|----------------|---------------|
| DateTime | timestamp without time zone | Explicit column type |
| DateTime? | timestamp without time zone | Explicit column type |
| decimal | decimal(18,2) | For money/salary fields |
| decimal | decimal(5,2) | For reputation index |
| string | varchar(n) | With max length constraints |
| int | integer | Default mapping |
| bool | boolean | Default mapping |
| enum | integer | Default mapping |

## Next Steps: Database Migration

### Step 1: Install PostgreSQL
Ensure PostgreSQL 16 is installed and running on your system.

### Step 2: Create Database
```sql
CREATE DATABASE "DBProject";
CREATE DATABASE "DBProject_Dev";
```

### Step 3: Update Connection Strings
Update the connection strings in `appsettings.json` and `appsettings.Development.json` with your actual PostgreSQL credentials:
- Host
- Port
- Username
- Password

### Step 4: Remove Old Migrations (if any exist)
```bash
cd /modernize-data/studio-data/TNT1001/APP319159/transformed-code/7/studio-workspace/Backend\ Services/Code/src/ClinicManagement.Infrastructure
rm -rf Migrations/
```

### Step 5: Create Initial Migration
```bash
cd /modernize-data/studio-data/TNT1001/APP319159/transformed-code/7/studio-workspace/Backend\ Services/Code/src/ClinicManagement.Infrastructure
dotnet ef migrations add InitialPostgreSQLMigration --startup-project ../ClinicManagement.Web/ClinicManagement.Web.csproj
```

### Step 6: Apply Migration to Database
```bash
dotnet ef database update --startup-project ../ClinicManagement.Web/ClinicManagement.Web.csproj
```

### Step 7: Verify Database Schema
Connect to PostgreSQL and verify:
```sql
-- Check tables (should be in snake_case)
\dt public.*

-- Verify table structure
\d public.patients
\d public.doctors
\d public.appointments
```

Expected table names (snake_case):
- patients
- doctors
- departments
- appointments
- slots
- bills
- feedbacks
- staff
- admins
- __ef_migrations_history

## Key PostgreSQL Differences Handled

### 1. Naming Convention
- **SQL Server**: PascalCase (e.g., `Patients`, `PatientID`)
- **PostgreSQL**: snake_case (e.g., `patients`, `patient_id`)
- **Solution**: Applied `UseSnakeCaseNamingConvention()`

### 2. DateTime Handling
- **SQL Server**: `datetime2`
- **PostgreSQL**: `timestamp without time zone`
- **Solution**: Explicit column type configuration

### 3. Default Values
- **SQL Server**: `GETDATE()`
- **PostgreSQL**: `CURRENT_TIMESTAMP`
- **Solution**: Updated default value SQL

### 4. Schema
- **SQL Server**: `dbo` (default)
- **PostgreSQL**: `public` (default)
- **Solution**: Explicitly set to `public`

### 5. Connection Pooling
- **SQL Server**: Built-in
- **PostgreSQL**: Configured in connection string
- **Solution**: Added pooling parameters

## Testing Checklist

After migration, test the following:

- [ ] Database connection successful
- [ ] All tables created with correct schema
- [ ] Foreign key relationships working
- [ ] CRUD operations on all entities
- [ ] Patient registration and login
- [ ] Doctor registration and login
- [ ] Appointment booking
- [ ] Bill generation
- [ ] Feedback submission
- [ ] Search functionality
- [ ] Date/time operations
- [ ] Decimal calculations (bills, salaries)

## Rollback Plan

If issues occur, you can rollback by:

1. Restore the original `.csproj` file with SQL Server packages
2. Restore original connection strings
3. Restore original `ServiceCollectionExtensions.cs`
4. Restore original `ClinicDbContext.cs`
5. Restore SQL Server database from backup

## Performance Considerations

### Connection Pooling
The connection string is configured with:
- Minimum Pool Size: 0
- Maximum Pool Size: 100
- Connection Lifetime: 0 (unlimited)
- Command Timeout: 30 seconds
- Connection Timeout: 15 seconds

### Retry Policy
Configured with:
- Max Retry Count: 5
- Max Retry Delay: 30 seconds
- Handles transient PostgreSQL errors

### Indexes
Unique indexes created on:
- `patients.email`
- `doctors.email`
- `admins.email`

Consider adding additional indexes based on query patterns:
```sql
CREATE INDEX idx_appointments_patient_id ON appointments(patient_id);
CREATE INDEX idx_appointments_doctor_id ON appointments(doctor_id);
CREATE INDEX idx_appointments_date ON appointments(appointment_date);
CREATE INDEX idx_slots_doctor_date ON slots(doctor_id, slot_date);
```

## Known Limitations

1. **Case Sensitivity**: PostgreSQL is case-sensitive for identifiers. The snake_case convention handles this automatically.

2. **Sequence Management**: PostgreSQL uses sequences for auto-increment. EF Core handles this automatically.

3. **Boolean Values**: PostgreSQL uses true/false instead of 1/0. EF Core handles the conversion.

4. **String Comparison**: PostgreSQL string comparison is case-sensitive by default. Use `ILIKE` for case-insensitive searches if needed.

## Support and Troubleshooting

### Common Issues

**Issue**: Connection timeout
**Solution**: Check PostgreSQL service is running and firewall allows connections

**Issue**: Authentication failed
**Solution**: Verify username/password in connection string and PostgreSQL pg_hba.conf

**Issue**: Table names not in snake_case
**Solution**: Ensure `UseSnakeCaseNamingConvention()` is called in DbContext configuration

**Issue**: DateTime timezone issues
**Solution**: All DateTime fields use `timestamp without time zone` - ensure application uses UTC or local time consistently

## Migration Compliance Summary

### Rules Implemented

✅ **Package Dependencies**: Updated to PostgreSQL packages
✅ **Connection Strings**: Converted to PostgreSQL format
✅ **DbContext Configuration**: Updated to use Npgsql with proper settings
✅ **Naming Convention**: Applied snake_case convention
✅ **Type Mappings**: All types mapped to PostgreSQL equivalents
✅ **DateTime Handling**: Configured for PostgreSQL timestamp
✅ **Schema Configuration**: Set to public schema
✅ **Retry Policy**: Implemented for resilience
✅ **Migration History**: Configured for PostgreSQL

### Files Modified

1. `ClinicManagement.Infrastructure/ClinicManagement.Infrastructure.csproj`
2. `ClinicManagement.Web/appsettings.json`
3. `ClinicManagement.Web/appsettings.Development.json`
4. `ClinicManagement.Infrastructure/Extensions/ServiceCollectionExtensions.cs`
5. `ClinicManagement.Infrastructure/Data/ClinicDbContext.cs`

### No Changes Required

- ✅ Domain entities (no database-specific code)
- ✅ Application services (no database-specific code)
- ✅ Repositories (using EF Core abstractions)
- ✅ No raw SQL queries found
- ✅ No stored procedures found

## Conclusion

The code migration from SQL Server to PostgreSQL is **COMPLETE**. All necessary code changes have been applied. The next step is to create and apply database migrations using Entity Framework Core tools.

The application follows clean architecture principles, which made the migration straightforward - only the Infrastructure layer needed changes, while Domain and Application layers remained unchanged.
