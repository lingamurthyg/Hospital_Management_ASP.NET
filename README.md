# Clinic Management System - .NET 8

A modern clinic management system migrated from ASP.NET Web Forms 4.5.2 to .NET 8 using clean architecture principles.

## Overview

This application manages clinic operations including patient registration, doctor management, appointment scheduling, treatment tracking, and billing.

## Architecture

The application follows clean architecture with clear separation of concerns:

### Layers

1. **Domain Layer** (`ClinicManagement.Domain`)
   - Entities: Patient, Doctor, Appointment, TreatmentHistory, Bill, Feedback, Admin, Staff
   - Interfaces: Repository and Service interfaces
   - Enums: UserType, Gender, AppointmentStatus
   - Exceptions: Custom domain exceptions

2. **Application Layer** (`ClinicManagement.Application`)
   - Services: Business logic implementation
   - DTOs: Data transfer objects
   - Mappings: AutoMapper profiles
   - Validators: FluentValidation rules

3. **Infrastructure Layer** (`ClinicManagement.Infrastructure`)
   - Data: EF Core DbContext and configurations
   - Repositories: Data access implementations
   - Services: External service integrations

4. **Web Layer** (`ClinicManagement.Web`)
   - Pages: Razor Pages for UI
   - ViewModels: Page-specific models
   - Extensions: DI configuration

## Technology Stack

- **.NET 8**: Latest .NET platform
- **ASP.NET Core Razor Pages**: Modern web UI framework
- **Entity Framework Core 8.0**: ORM for data access
- **SQL Server**: Database
- **AutoMapper**: Object mapping
- **Serilog**: Logging framework
- **xUnit**: Testing framework
- **FluentAssertions**: Test assertions
- **Moq**: Mocking framework
- **Bootstrap 5**: UI framework

## Prerequisites

- .NET 8 SDK
- SQL Server (Express or higher)
- Visual Studio 2022 or VS Code

## Setup Instructions

### 1. Database Setup

```bash
# Run the SQL script to create the database
sqlcmd -S .\SQLEXPRESS -i Hospital_mgmt_MSSQL.sql
```

### 2. Configuration

Update the connection string in `src/ClinicManagement.Web/appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=.\\SQLEXPRESS;Initial Catalog=DBProject;Integrated Security=True;TrustServerCertificate=True"
  }
}
```

### 3. Build and Run

```bash
# Restore packages
dotnet restore

# Build the solution
dotnet build

# Run the application
cd src/ClinicManagement.Web
dotnet run
```

Navigate to `https://localhost:5001`

## Features

### Patient Features
- Patient registration and login
- View and book appointments with doctors
- View treatment history
- View billing history
- Submit feedback

### Doctor Features
- Doctor login
- View and manage appointments
- Update patient treatment records
- Generate bills

### Admin Features
- Admin dashboard
- Register new doctors
- Add staff members
- View system statistics

## Project Structure

```
CApp/
├── src/
│   ├── ClinicManagement.Domain/
│   │   ├── Entities/
│   │   ├── Enums/
│   │   ├── Exceptions/
│   │   └── Interfaces/
│   ├── ClinicManagement.Application/
│   │   ├── DTOs/
│   │   ├── Extensions/
│   │   ├── Mappings/
│   │   ├── Services/
│   │   └── Validators/
│   ├── ClinicManagement.Infrastructure/
│   │   ├── Data/
│   │   ├── Extensions/
│   │   ├── Repositories/
│   │   └── Services/
│   └── ClinicManagement.Web/
│       ├── Pages/
│       ├── ViewModels/
│       ├── wwwroot/
│       ├── Program.cs
│       └── appsettings.json
├── tests/
│   ├── ClinicManagement.UnitTests/
│   └── ClinicManagement.IntegrationTests/
├── docs/
│   ├── MIGRATION_NOTES.md
│   └── ARCHITECTURE.md
├── ClinicManagement.sln
└── README.md
```

## Running Tests

```bash
# Run unit tests
dotnet test tests/ClinicManagement.UnitTests

# Run integration tests
dotnet test tests/ClinicManagement.IntegrationTests

# Run all tests
dotnet test
```

## Build Verification

The application has been successfully migrated and builds without errors:

```
Build succeeded.
    2 Warning(s)
    0 Error(s)
```

Note: There is a known vulnerability in AutoMapper 13.0.1. This is a dependency issue and should be addressed in a future update.

## Migration Notes

This application was migrated from ASP.NET Web Forms 4.5.2. Key changes include:

- Clean architecture implementation
- Entity Framework Core instead of ADO.NET
- Async/await patterns throughout
- Modern authentication with password hashing
- Dependency injection
- Comprehensive logging with Serilog

See `docs/MIGRATION_NOTES.md` for detailed migration information.

## Security Notes

- Passwords are hashed using SHA256
- SQL injection protected through EF Core parameterized queries
- CSRF protection enabled by default
- HTTPS enforced

**For Production**: Consider implementing ASP.NET Core Identity for enhanced security features.

## Default Credentials

After running the database script, you can use existing user accounts or register new ones through the application.

## License

See LICENSE file for details.

## Support

For issues or questions, please refer to the original repository or contact the development team.

## Version

- **Current Version**: 1.0.0
- **Migration Date**: 2026-04-24
- **Framework**: .NET 8
- **Original Framework**: ASP.NET Web Forms 4.5.2
