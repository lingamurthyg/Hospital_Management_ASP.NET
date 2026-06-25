# PostgreSQL Migration Guide

## Overview
This document describes the migration of the ClinicManagement application from SQL Server to PostgreSQL.

## Changes Made

### 1. Package Dependencies Updated
**File:** `ClinicManagement.Infrastructure/ClinicManagement.Infrastructure.csproj`

**Removed:**
- `Microsoft.EntityFrameworkCore.SqlServer` Version 8.0.0

**Added:**
- `Npgsql.EntityFrameworkCore.PostgreSQL` Version 8.0.0
- `EFCore.NamingConventions` Version 8.0.0
- `Microsoft.EntityFrameworkCore.Relational` Version 8.0.0

### 2. Connection String Updated
**Files:** 
- `ClinicManagement.Web/appsettings.json`
- `ClinicManagement.Web/appsettings.Development.json` (created)

**Old (SQL Server):**
```json
"DefaultConnection": "Data Source=.\\SQLEXPRESS;Initial Catalog=DBProject;Integrated Security=True;TrustServerCertificate=True"
```

**New (PostgreSQL):**
```json
"DefaultConnection": "Host=localhost;Port=5432;Database=DBProject;Username=postgres;Password=postgres;Include Error Detail=true"
```

### 3. DbContext Configuration Updated
**File:** `ClinicManagement.Web/Program.cs`

**Changes:**
- Replaced `UseSqlServer()` with `UseNpgsql()`
- Added snake_case naming convention with `UseSnakeCaseNamingConvention()`
- Configured migrations history table: `__ef_migrations_history` in `public` schema
- Added retry policy for connection resilience
- Enabled sensitive data logging and detailed errors for development

### 4. Entity Type Mappings Updated
**File:** `ClinicManagement.Infrastructure/Data/ClinicDbContext.cs`

**Changes:**
- Set default schema to `public` for PostgreSQL
- Updated all DateTime properties to use `timestamp without time zone`
  - Patient.BirthDate
  - Doctor.BirthDate
  - Appointment.AppointmentDate
  - FreeSlot.SlotDate
  - Bill.BillDate
  - TreatmentHistory.TreatmentDate
  - OtherStaff.BirthDate
- Changed decimal type from `decimal(18,2)` to `numeric(18,2)` for Bill.Amount

### 5. Database Naming Convention
With the `EFCore.NamingConventions` package, all database objects will automatically use snake_case:
- Table names: `patients`, `doctors`, `appointments`, etc.
- Column names: `patient_id`, `birth_date`, `email`, etc.
- Foreign keys: `patient_id`, `doctor_id`, etc.

## Migration Steps

### Prerequisites
1. PostgreSQL 16 installed and running
2. .NET 8.0 SDK installed
3. EF Core tools installed: `dotnet tool install --global dotnet-ef`

### Step 1: Update Connection String
Update the connection string in `appsettings.json` or `appsettings.Development.json` with your PostgreSQL credentials:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=DBProject;Username=your_username;Password=your_password"
  }
}
```

### Step 2: Create Initial Migration
```bash
cd src/ClinicManagement.Infrastructure
dotnet ef migrations add InitialCreate --startup-project ../ClinicManagement.Web
```

### Step 3: Apply Migration to Database
```bash
dotnet ef database update --startup-project ../ClinicManagement.Web
```

### Step 4: Verify Database Schema
Connect to PostgreSQL and verify the schema:
```sql
-- List all tables
\dt

-- Check a specific table structure
\d patients

-- Verify data types
SELECT column_name, data_type 
FROM information_schema.columns 
WHERE table_name = 'patients';
```

## Key Differences: SQL Server vs PostgreSQL

### 1. Data Types
| SQL Server | PostgreSQL |
|------------|------------|
| `datetime2` | `timestamp without time zone` |
| `decimal(18,2)` | `numeric(18,2)` |
| `nvarchar(max)` | `text` |
| `bit` | `boolean` |

### 2. Naming Conventions
- SQL Server: Case-insensitive, typically PascalCase
- PostgreSQL: Case-sensitive (lowercase recommended), snake_case with EFCore.NamingConventions

### 3. Schema
- SQL Server: Default schema is `dbo`
- PostgreSQL: Default schema is `public`

### 4. Identity Columns
- SQL Server: `IDENTITY(1,1)`
- PostgreSQL: `SERIAL` or `GENERATED ALWAYS AS IDENTITY`

## Testing Recommendations

1. **Unit Tests**: Ensure all existing unit tests pass
2. **Integration Tests**: Test database operations
3. **Performance Tests**: Compare query performance
4. **Data Migration**: If migrating existing data, test the migration process

## Rollback Plan

If you need to rollback to SQL Server:
1. Restore the original `.csproj` file
2. Restore the original `Program.cs` file
3. Restore the original `appsettings.json` file
4. Restore the original `ClinicDbContext.cs` file
5. Run: `dotnet restore`

## Additional Resources

- [Npgsql Documentation](https://www.npgsql.org/efcore/)
- [EF Core PostgreSQL Provider](https://www.npgsql.org/efcore/index.html)
- [PostgreSQL Documentation](https://www.postgresql.org/docs/)
- [EFCore.NamingConventions](https://github.com/efcore/EFCore.NamingConventions)

## Support

For issues or questions, please refer to:
- Npgsql GitHub: https://github.com/npgsql/npgsql
- EF Core GitHub: https://github.com/dotnet/efcore
