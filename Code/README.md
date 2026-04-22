# Clinic Management System - .NET 8 Migration

## Overview
This project has been successfully migrated from ASP.NET Web Forms 4.5.2 to .NET 8 using Clean Architecture principles.

## Architecture

### Clean Architecture Layers
- **Domain Layer**: Core business entities and interfaces
- **Application Layer**: Business logic, services, DTOs, and AutoMapper profiles
- **Infrastructure Layer**: Data access with Entity Framework Core 8.0, repositories
- **Web Layer**: ASP.NET Core Razor Pages for UI

## Project Structure
```
ClinicManagement/
├── src/
│   ├── ClinicManagement.Domain/
│   ├── ClinicManagement.Application/
│   ├── ClinicManagement.Infrastructure/
│   └── ClinicManagement.Web/
├── tests/
│   ├── ClinicManagement.UnitTests/
│   └── ClinicManagement.IntegrationTests/
└── docs/
```

## Key Features
- Patient registration and management
- Doctor management and profiles
- Appointment scheduling
- Bill generation and payment tracking
- Feedback system
- Department management
- Staff management

## Technologies Used
- .NET 8
- ASP.NET Core Razor Pages
- Entity Framework Core 8.0
- SQL Server
- AutoMapper 12.0.1
- Serilog for logging
- xUnit for testing

## Getting Started

### Prerequisites
- .NET 8 SDK
- SQL Server (LocalDB or full instance)

### Setup
1. Update the connection string in `appsettings.json`
2. Run database migrations:
   ```bash
   cd src/ClinicManagement.Infrastructure
   dotnet ef migrations add InitialCreate --startup-project ../ClinicManagement.Web
   dotnet ef database update --startup-project ../ClinicManagement.Web
   ```
3. Run the application:
   ```bash
   cd src/ClinicManagement.Web
   dotnet run
   ```

## Migration Notes
- All Web Forms pages have been converted to Razor Pages
- ViewState replaced with proper state management
- ADO.NET replaced with Entity Framework Core
- System.Web dependencies removed
- Configuration migrated from Web.config to appsettings.json
- Logging migrated to Serilog

## Database
The application uses the existing `DBProject` database with the following main tables:
- Patient
- Doctor
- Department
- Appointment
- Slot
- Bill
- Feedback
- Staff
- Admin

## License
Proprietary
