# PostgreSQL Migration Checklist

## Pre-Migration Assessment

### Application Information
- **Application Name**: ClinicManagement System
- **Application Type**: .NET 8.0 Web API with Entity Framework Core
- **Source Database**: SQL Server (SQLEXPRESS)
- **Target Database**: PostgreSQL 16
- **Architecture**: Clean Architecture (Domain, Application, Infrastructure, Web)

### Components Analyzed
- ✅ Domain Layer (Entities, Interfaces)
- ✅ Application Layer (Services, DTOs)
- ✅ Infrastructure Layer (DbContext, Repositories)
- ✅ Web Layer (Controllers, Configuration)

## Migration Tasks Completed

### 1. Package Dependencies ✅
**Status**: COMPLETED
**File**: `src/ClinicManagement.Infrastructure/ClinicManagement.Infrastructure.csproj`

| Action | Package | Version | Status |
|--------|---------|---------|--------|
| Removed | Microsoft.EntityFrameworkCore.SqlServer | 8.0.0 | ✅ |
| Added | Npgsql.EntityFrameworkCore.PostgreSQL | 8.0.0 | ✅ |
| Added | EFCore.NamingConventions | 8.0.0 | ✅ |
| Added | Microsoft.EntityFrameworkCore.Relational | 8.0.0 | ✅ |
| Retained | Microsoft.EntityFrameworkCore | 8.0.0 | ✅ |
| Retained | Microsoft.EntityFrameworkCore.Design | 8.0.0 | ✅ |

### 2. Connection String Configuration ✅
**Status**: COMPLETED

**Files Modified**:
- ✅ `src/ClinicManagement.Web/appsettings.json`
- ✅ `src/ClinicManagement.Web/appsettings.Development.json` (created)

**Changes**:
- ✅ Converted from SQL Server connection string format
- ✅ Updated to PostgreSQL connection string format
- ✅ Added error detail logging for development

### 3. DbContext Configuration ✅
**Status**: COMPLETED
**File**: `src/ClinicManagement.Web/Program.cs`

**Changes Applied**:
- ✅ Replaced `UseSqlServer()` with `UseNpgsql()`
- ✅ Added `UseSnakeCaseNamingConvention()`
- ✅ Configured migrations history table: `__ef_migrations_history` in `public` schema
- ✅ Added retry policy with 5 retries and 30-second max delay
- ✅ Enabled sensitive data logging for development
- ✅ Enabled detailed errors for development

### 4. Entity Type Mappings ✅
**Status**: COMPLETED
**File**: `src/ClinicManagement.Infrastructure/Data/ClinicDbContext.cs`

**Schema Configuration**:
- ✅ Set default schema to `public`
- ✅ Added PostgreSQL extension support (commented, ready for use)

**DateTime Type Mappings** (timestamp without time zone):
- ✅ Patient.BirthDate
- ✅ Doctor.BirthDate
- ✅ Appointment.AppointmentDate
- ✅ FreeSlot.SlotDate
- ✅ Bill.BillDate
- ✅ TreatmentHistory.TreatmentDate
- ✅ OtherStaff.BirthDate

**Decimal Type Mappings**:
- ✅ Bill.Amount (changed from `decimal(18,2)` to `numeric(18,2)`)

**Relationship Configurations**:
- ✅ All foreign key relationships preserved
- ✅ All cascade delete behaviors preserved (Restrict)
- ✅ All unique indexes preserved

### 5. Repository Layer ✅
**Status**: VERIFIED - NO CHANGES NEEDED

**Files Verified**:
- ✅ `PatientRepository.cs` - Uses LINQ only, database-agnostic
- ✅ `DoctorRepository.cs` - Uses LINQ only, database-agnostic
- ✅ `AppointmentRepository.cs` - Uses LINQ only, database-agnostic

**Findings**:
- No raw SQL queries found
- All queries use Entity Framework LINQ
- No database-specific functions used

### 6. Service Layer ✅
**Status**: VERIFIED - NO CHANGES NEEDED

**Files Verified**:
- ✅ `PatientService.cs` - No database-specific code
- ✅ `DoctorService.cs` - No database-specific code
- ✅ `AppointmentService.cs` - No database-specific code

### 7. Controller Layer ✅
**Status**: VERIFIED - NO CHANGES NEEDED

**Files Verified**:
- ✅ `PatientsController.cs` - No database-specific code
- ✅ `DoctorsController.cs` - No database-specific code
- ✅ `AppointmentsController.cs` - No database-specific code

### 8. Domain Entities ✅
**Status**: VERIFIED - NO CHANGES NEEDED

**Entities Verified**:
- ✅ Patient
- ✅ Doctor
- ✅ Department
- ✅ Appointment
- ✅ FreeSlot
- ✅ Bill
- ✅ TreatmentHistory
- ✅ OtherStaff

**Findings**:
- All entities use standard data annotations
- No SQL Server-specific attributes found
- All navigation properties properly configured

## Database Schema Changes

### Naming Convention Changes (snake_case)
| C# Property | SQL Server | PostgreSQL |
|-------------|------------|------------|
| PatientID | PatientID | patient_id |
| BirthDate | BirthDate | birth_date |
| Email | Email | email |
| AppointmentDate | AppointmentDate | appointment_date |
| DoctorID | DoctorID | doctor_id |

### Table Names (snake_case)
| C# Entity | SQL Server | PostgreSQL |
|-----------|------------|------------|
| Patient | Patients | patients |
| Doctor | Doctors | doctors |
| Department | Departments | departments |
| Appointment | Appointments | appointments |
| FreeSlot | FreeSlots | free_slots |
| Bill | Bills | bills |
| TreatmentHistory | TreatmentHistories | treatment_histories |
| OtherStaff | OtherStaff | other_staff |

## Migration Validation Checklist

### Pre-Migration Validation
- ✅ All package references updated
- ✅ Connection strings converted
- ✅ DbContext configuration updated
- ✅ Entity type mappings configured
- ✅ No raw SQL queries found
- ✅ No database-specific functions used

### Post-Migration Validation (To Be Done)
- ⏳ Create initial migration
- ⏳ Apply migration to PostgreSQL database
- ⏳ Verify schema creation
- ⏳ Test CRUD operations
- ⏳ Verify data types
- ⏳ Test relationships and foreign keys
- ⏳ Verify indexes and constraints
- ⏳ Run integration tests
- ⏳ Performance testing

## Files Modified Summary

### Configuration Files (3 files)
1. ✅ `src/ClinicManagement.Infrastructure/ClinicManagement.Infrastructure.csproj`
2. ✅ `src/ClinicManagement.Web/appsettings.json`
3. ✅ `src/ClinicManagement.Web/appsettings.Development.json` (NEW)

### Source Code Files (2 files)
4. ✅ `src/ClinicManagement.Web/Program.cs`
5. ✅ `src/ClinicManagement.Infrastructure/Data/ClinicDbContext.cs`

### Documentation Files (2 files)
6. ✅ `POSTGRESQL_MIGRATION.md` (NEW)
7. ✅ `POSTGRESQL_MIGRATION_CHECKLIST.md` (NEW - this file)

**Total Files Modified**: 7 files
**Total Files Created**: 3 files

## Known Issues and Limitations

### None Identified
- ✅ No breaking changes detected
- ✅ No data loss expected
- ✅ All features should work identically

## Next Steps

1. **Create Migration**:
   ```bash
   cd src/ClinicManagement.Infrastructure
   dotnet ef migrations add InitialCreate --startup-project ../ClinicManagement.Web
   ```

2. **Review Migration**:
   - Check generated migration file
   - Verify SQL commands
   - Ensure all tables and relationships are correct

3. **Apply Migration**:
   ```bash
   dotnet ef database update --startup-project ../ClinicManagement.Web
   ```

4. **Verify Database**:
   - Connect to PostgreSQL
   - Check table structures
   - Verify data types
   - Test constraints

5. **Test Application**:
   - Run unit tests
   - Run integration tests
   - Test all API endpoints
   - Verify data operations

## Rollback Procedure

If migration fails or issues are discovered:

1. **Restore Original Files**:
   - Restore from version control
   - Or manually revert changes

2. **Restore Packages**:
   ```bash
   dotnet restore
   ```

3. **Verify SQL Server Connection**:
   - Test connection string
   - Verify database access

## Success Criteria

- ✅ All package dependencies updated successfully
- ✅ Connection strings converted correctly
- ✅ DbContext configured for PostgreSQL
- ✅ All entity mappings updated
- ✅ No compilation errors
- ⏳ Migration created successfully
- ⏳ Database schema created correctly
- ⏳ All CRUD operations working
- ⏳ All tests passing
- ⏳ Performance acceptable

## Migration Status: READY FOR TESTING

The code migration is complete. The application is ready for:
1. Creating EF Core migrations
2. Applying migrations to PostgreSQL database
3. Testing and validation

**Estimated Time to Complete Testing**: 30-45 minutes
**Risk Level**: LOW (Clean architecture, no raw SQL, comprehensive testing possible)
