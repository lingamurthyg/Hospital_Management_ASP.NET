# Clinic Management System

## Overview
This is a modern .NET 8 Clinic Management System migrated from ASP.NET Web Forms 4.5.2. The application follows clean architecture principles with a layered structure.

## Architecture
- **Domain Layer**: Core business entities and interfaces
- **Application Layer**: Business logic, DTOs, and AutoMapper profiles
- **Infrastructure Layer**: Data access with Entity Framework Core 8.0
- **Web Layer**: ASP.NET Core Razor Pages UI

## Technology Stack
- .NET 8
- ASP.NET Core Razor Pages
- Entity Framework Core 8.0
- SQL Server
- AutoMapper 12.0.1
- Serilog 8.0.0
- xUnit, FluentAssertions, Moq (Testing)

## Prerequisites
- .NET 8 SDK
- SQL Server (LocalDB or Express)
- Visual Studio 2022 or VS Code

## Getting Started

### 1. Clone the repository
```bash
git clone <repository-url>
cd FullComp
```

### 2. Update connection string
Edit `src/ClinicManagement.Web/appsettings.json` and update the connection string:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=.\\SQLEXPRESS;Initial Catalog=DBProject;Integrated Security=True;TrustServerCertificate=True"
  }
}
```

### 3. Apply database migrations
```bash
cd src/ClinicManagement.Infrastructure
dotnet ef migrations add InitialCreate --startup-project ../ClinicManagement.Web
dotnet ef database update --startup-project ../ClinicManagement.Web
```

### 4. Run the application
```bash
cd ../ClinicManagement.Web
dotnet run
```

The application will be available at `https://localhost:5001` or `http://localhost:5000`.

## Project Structure
```
FullComp/
├── src/
│   ├── ClinicManagement.Domain/          # Domain entities and interfaces
│   ├── ClinicManagement.Application/     # Business logic and DTOs
│   ├── ClinicManagement.Infrastructure/  # Data access and repositories
│   └── ClinicManagement.Web/             # Razor Pages UI
├── tests/
│   ├── ClinicManagement.UnitTests/       # Unit tests
│   └── ClinicManagement.IntegrationTests/# Integration tests
└── docs/                                  # Documentation
```

## Features
- Patient management
- Doctor management
- Appointment scheduling
- Bill management
- Treatment history tracking
- Department management
- Staff management
- Admin portal

## Migration Notes
This application was migrated from ASP.NET Web Forms 4.5.2 to .NET 8. Key changes include:
- Replaced System.Web with ASP.NET Core
- Migrated Web Forms pages to Razor Pages
- Converted ADO.NET to Entity Framework Core
- Replaced Web.config with appsettings.json
- Implemented clean architecture
- Added dependency injection
- Implemented async/await patterns

## Testing
Run unit tests:
```bash
dotnet test tests/ClinicManagement.UnitTests
```

Run integration tests:
```bash
dotnet test tests/ClinicManagement.IntegrationTests
```

## Build
```bash
dotnet build
```

## License
[Your License Here]

## Contributors
[Your Name/Team]
