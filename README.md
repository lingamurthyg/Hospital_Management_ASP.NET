# Clinic Management System - .NET 8 Migration

## Overview
This is a modernized version of the Clinic Management System, migrated from ASP.NET Web Forms 4.5.2 to .NET 8 with Clean Architecture.

## Architecture
The solution follows Clean Architecture principles with the following layers:

### Domain Layer (`ClinicManagement.Domain`)
- Contains domain entities, enums, and repository interfaces
- No dependencies on other layers
- Pure business logic and domain models

### Application Layer (`ClinicManagement.Application`)
- Contains DTOs, AutoMapper profiles, and service interfaces
- Depends only on Domain layer
- Business logic implementation

### Infrastructure Layer (`ClinicManagement.Infrastructure`)
- Contains EF Core DbContext, repository implementations
- Depends on Domain and Application layers
- Data access and external service implementations

### Web Layer (`ClinicManagement.Web`)
- ASP.NET Core Razor Pages application
- Depends on Infrastructure and Application layers
- User interface and API endpoints

## Technology Stack
- .NET 8
- ASP.NET Core Razor Pages
- Entity Framework Core 8.0
- SQL Server
- AutoMapper 12.0
- Serilog for logging
- xUnit for testing

## Prerequisites
- .NET 8 SDK
- SQL Server (LocalDB or Express)
- Visual Studio 2022 or VS Code

## Getting Started

### 1. Database Setup
Update the connection string in `src/ClinicManagement.Web/appsettings.json`:
```json
"ConnectionStrings": {
  "DefaultConnection": "Data Source=.\\SQLEXPRESS;Initial Catalog=DBProject;Integrated Security=True;TrustServerCertificate=True"
}
```

### 2. Build the Solution
```bash
cd /modernize-data/studio-data/TNT1001/APP338391/transformed-code/97/studio-workspace/BackendServices
dotnet restore
dotnet build
```

### 3. Run Migrations (if needed)
```bash
cd src/ClinicManagement.Web
dotnet ef migrations add InitialCreate
dotnet ef database update
```

### 4. Run the Application
```bash
cd src/ClinicManagement.Web
dotnet run
```

The application will be available at `https://localhost:5001` or `http://localhost:5000`.

## Project Structure
```
BackendServices/
├── src/
│   ├── ClinicManagement.Domain/
│   │   ├── Entities/
│   │   ├── Enums/
│   │   └── Interfaces/
│   ├── ClinicManagement.Application/
│   │   ├── DTOs/
│   │   ├── Mappings/
│   │   └── Services/
│   ├── ClinicManagement.Infrastructure/
│   │   ├── Data/
│   │   ├── Repositories/
│   │   └── Extensions/
│   └── ClinicManagement.Web/
│       ├── Pages/
│       ├── wwwroot/
│       └── Program.cs
├── tests/
│   ├── ClinicManagement.UnitTests/
│   └── ClinicManagement.IntegrationTests/
└── ClinicManagement.sln
```

## Migration Notes

### What Was Migrated
- ASP.NET Web Forms pages → Razor Pages
- ADO.NET data access → Entity Framework Core
- Web.config → appsettings.json
- Global.asax → Program.cs
- System.Web dependencies → ASP.NET Core equivalents

### Key Changes
1. **Data Access**: Replaced ADO.NET with EF Core repositories
2. **Configuration**: Moved from Web.config to appsettings.json
3. **Dependency Injection**: Using built-in DI container
4. **Logging**: Replaced with Serilog
5. **Authentication**: Migrated to ASP.NET Core Cookie Authentication

### Known Issues
- Database schema needs to be verified against existing database
- Some stored procedures may need to be migrated to EF Core queries
- User authentication needs to be fully implemented

## Testing
Run unit tests:
```bash
dotnet test tests/ClinicManagement.UnitTests
```

Run integration tests:
```bash
dotnet test tests/ClinicManagement.IntegrationTests
```

## Contributing
This is a migrated legacy application. Please follow clean architecture principles when adding new features.

## License
Internal use only.
