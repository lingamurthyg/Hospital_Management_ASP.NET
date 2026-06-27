# PostgreSQL Migration Guide for Clinic Management System

## Overview
This document describes the migration of the Clinic Management System from SQL Server to PostgreSQL 16.

## Migration Summary

### Changes Applied

#### 1. Package Dependencies Updated
**File**: `ClinicManagement.Infrastructure/ClinicManagement.Infrastructure.csproj`

**Removed**:
- `Microsoft.EntityFrameworkCore.SqlServer` Version 8.0.0

**Added**:
- `Npgsql.EntityFrameworkCore.PostgreSQL` Version 8.0.0
- `EFCore.NamingConventions` Version 8.0.0

#### 2. Connection String Converted
**Files**: 
- `ClinicManagement.Web/appsettings.json`
- `ClinicManagement.Web/bin/Debug/net8.0/appsettings.json`

**Before**:
```
Data Source=.\\SQLEXPRESS;Initial Catalog=DBProject;Integrated Security=True;TrustServerCertificate=True
```

**After**:
```
Host=localhost;Port=5432;Database=DBProject;Username=postgres;Password=postgres;Include Error Detail=true
```

#### 3. DbContext Configuration Updated
**File**: `ClinicManagement.Web/Program.cs`

**Changes**:
- Replaced `UseSqlServer()` with `UseNpgsql()`
- Added PostgreSQL-specific configurations:
  - Migration history table: `__efmigrations_history` in `public` schema
  - Retry policy with 5 max retries and 30-second delay
  - Snake case naming convention using `UseSnakeCaseNamingConvention()`

#### 4. Entity Type Mappings Updated
**File**: `ClinicManagement.Infrastructure/Data/ClinicDbContext.cs`

**Changes**:
- Set default schema to `public`
- Added PostgreSQL extension: `uuid-ossp`
- Configured all DateTime properties to use `timestamp without time zone`
- Configured decimal properties with precision: `decimal(18,2)`
- Added comprehensive entity configurations for:
  - Patient
  - Doctor
  - Department
  - Appointment
  - Staff
  - Feedback

**Key Type Conversions**:
- SQL Server `datetime2` → PostgreSQL `timestamp without time zone`
- SQL Server `decimal` → PostgreSQL `decimal(18,2)`
- SQL Server `nvarchar(max)` → PostgreSQL `text` (via MaxLength attributes)

#### 5. Naming Convention
All database objects (tables, columns, indexes) will use snake_case naming convention automatically through the `EFCore.NamingConventions` package.

**Examples**:
- `PatientId` → `patient_id`
- `AppointmentDate` → `appointment_date`
- `CreatedDate` → `created_date`

## Next Steps

### 1. Create Initial Migration
```bash
cd /modernize-data/studio-data/TNT1001/APP242299/transformed-code/15/studio-workspace/FullApp/src/ClinicManagement.Web
dotnet ef migrations add InitialPostgreSQLMigration --project ../ClinicManagement.Infrastructure/ClinicManagement.Infrastructure.csproj
```

### 2. Update Database
```bash
dotnet ef database update --project ../ClinicManagement.Infrastructure/ClinicManagement.Infrastructure.csproj
```

### 3. Verify Migration
- Check that all tables are created in PostgreSQL
- Verify snake_case naming convention is applied
- Test CRUD operations for all entities
- Verify relationships and foreign keys

## PostgreSQL Configuration Requirements

### Database Setup
```sql
-- Create database
CREATE DATABASE "DBProject";

-- Create user (if needed)
CREATE USER postgres WITH PASSWORD 'postgres';

-- Grant privileges
GRANT ALL PRIVILEGES ON DATABASE "DBProject" TO postgres;
```

### Connection Settings
- **Host**: localhost
- **Port**: 5432
- **Database**: DBProject
- **Username**: postgres
- **Password**: postgres (update for production)

## Entity Configurations Summary

### Patient
- Primary Key: `id`
- DateTime fields: `birth_date`, `created_date`, `modified_date`
- Relationships: One-to-Many with Appointments and Feedbacks

### Doctor
- Primary Key: `id`
- DateTime fields: `birth_date`, `created_date`, `modified_date`
- Decimal fields: `salary`, `charges_per_visit`
- Relationships: 
  - Many-to-One with Department
  - One-to-Many with Appointments

### Department
- Primary Key: `id`
- DateTime fields: `created_date`, `modified_date`
- Relationships: One-to-Many with Doctors

### Appointment
- Primary Key: `id`
- DateTime fields: `appointment_date`, `created_date`, `modified_date`
- Decimal fields: `bill_amount`
- Relationships:
  - Many-to-One with Patient
  - Many-to-One with Doctor
  - One-to-One with Feedback

### Staff
- Primary Key: `id`
- DateTime fields: `birth_date`, `created_date`, `modified_date`
- Decimal fields: `salary`

### Feedback
- Primary Key: `id`
- DateTime fields: `feedback_date`, `created_date`, `modified_date`
- Relationships:
  - One-to-One with Appointment
  - Many-to-One with Patient
  - Many-to-One with Doctor

## Testing Checklist

- [ ] Database connection successful
- [ ] All tables created with correct schema
- [ ] Snake case naming applied correctly
- [ ] Foreign key constraints working
- [ ] DateTime values stored correctly (UTC)
- [ ] Decimal precision maintained
- [ ] CRUD operations for all entities
- [ ] Navigation properties loading correctly
- [ ] Soft delete (IsActive flag) working
- [ ] Audit fields (CreatedDate, ModifiedDate) auto-populated

## Known Differences from SQL Server

1. **Case Sensitivity**: PostgreSQL is case-sensitive for identifiers. Snake case naming convention helps avoid issues.

2. **DateTime Handling**: PostgreSQL uses `timestamp without time zone` instead of SQL Server's `datetime2`.

3. **String Comparison**: PostgreSQL string comparison is case-sensitive by default. Use `ILIKE` for case-insensitive searches.

4. **Sequences**: PostgreSQL uses sequences for auto-increment instead of SQL Server's IDENTITY.

5. **Schema**: Default schema is `public` in PostgreSQL instead of `dbo` in SQL Server.

## Rollback Plan

If migration needs to be rolled back:

1. Restore the original package references in `.csproj`
2. Restore the original connection string
3. Restore the original `Program.cs` configuration
4. Restore the original `ClinicDbContext.cs`
5. Run SQL Server migrations

## Support and Troubleshooting

### Common Issues

**Issue**: Connection refused
**Solution**: Ensure PostgreSQL service is running and connection parameters are correct

**Issue**: Migration fails with naming conflicts
**Solution**: Verify snake_case naming convention is properly configured

**Issue**: DateTime conversion errors
**Solution**: Ensure all DateTime properties use `timestamp without time zone` column type

**Issue**: Decimal precision loss
**Solution**: Verify decimal properties use `decimal(18,2)` column type

## Migration Completed By
- Date: 2025
- Migration Tool: Entity Framework Core 8.0
- Target Database: PostgreSQL 16
- Source Database: SQL Server 2017

## Files Modified

1. `/src/ClinicManagement.Infrastructure/ClinicManagement.Infrastructure.csproj`
2. `/src/ClinicManagement.Web/appsettings.json`
3. `/src/ClinicManagement.Web/bin/Debug/net8.0/appsettings.json`
4. `/src/ClinicManagement.Web/Program.cs`
5. `/src/ClinicManagement.Infrastructure/Data/ClinicDbContext.cs`

Total Files Modified: 5
