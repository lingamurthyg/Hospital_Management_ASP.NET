# Clinic Management System - .NET 8 Migration

## Overview
This is a migrated version of the Clinic Management System from ASP.NET Web Forms 4.5.2 to .NET 8 with clean architecture.

## Architecture
The solution follows clean architecture principles with the following layers:

### Domain Layer (`ClinicManagement.Domain`)
- Contains domain entities (Patient, Doctor, Department, Appointment, Staff, Feedback)
- Contains repository and service interfaces
- No dependencies on other layers

### Application Layer (`ClinicManagement.Application`)
- Contains DTOs (Data Transfer Objects)
- Contains AutoMapper profiles
- Contains FluentValidation validators
- Depends only on Domain layer

### Infrastructure Layer (`ClinicManagement.Infrastructure`)
- Contains EF Core DbContext
- Contains repository implementations
- Contains data access logic
- Depends on Domain and Application layers

### Web Layer (`ClinicManagement.Web`)
- ASP.NET Core Razor Pages application
- Contains UI pages and view models
- Depends on Infrastructure and Application layers

## Prerequisites
- .NET 8 SDK
- SQL Server (LocalDB or Express)
- Visual Studio 2022 or VS Code

## Setup Instructions

### 1. Database Setup
Update the connection string in `src/ClinicManagement.Web/appsettings.json`:
```json
"ConnectionStrings": {
  "DefaultConnection": "Data Source=.\\SQLEXPRESS;Initial Catalog=DBProject;Integrated Security=True;TrustServerCertificate=True"
}
```

### 2. Run Migrations
```bash
cd src/ClinicManagement.Infrastructure
dotnet ef migrations add InitialCreate --startup-project ../ClinicManagement.Web
dotnet ef database update --startup-project ../ClinicManagement.Web
```

### 3. Build the Solution
```bash
dotnet build
```

### 4. Run the Application
```bash
cd src/ClinicManagement.Web
dotnet run
```

The application will be available at `https://localhost:5001` or `http://localhost:5000`.

## Features
- Patient registration and login
- Doctor login
- Admin login (default: admin@clinic.com / admin123)
- Appointment management
- Patient treatment history
- Doctor management
- Staff management

## Migration Notes

### Key Changes from Web Forms
1. **Configuration**: Web.config → appsettings.json
2. **Data Access**: ADO.NET with stored procedures → EF Core with LINQ
3. **UI**: ASPX pages → Razor Pages
4. **State Management**: ViewState/Session → Session with distributed cache
5. **Dependency Injection**: Manual instantiation → Built-in DI container
6. **Logging**: No logging → Serilog

### Breaking Changes
- All stored procedures need to be replaced with EF Core queries or kept as raw SQL
- Session state is now distributed (can be configured for Redis, SQL Server, etc.)
- Authentication needs to be implemented using ASP.NET Core Identity

## Testing
Run unit tests:
```bash
dotnet test tests/ClinicManagement.UnitTests
```

Run integration tests:
```bash
dotnet test tests/ClinicManagement.IntegrationTests
```

## Known Issues
- Entity configurations need to be completed
- Some stored procedures may need migration
- Authentication should be upgraded to ASP.NET Core Identity
- Additional pages need to be implemented

## Future Improvements
- Implement ASP.NET Core Identity for authentication
- Add authorization policies
- Implement all CRUD pages for entities
- Add API endpoints for mobile app support
- Implement real-time notifications using SignalR
- Add comprehensive logging and monitoring

## License
Copyright © 2024 Clinic Management System
