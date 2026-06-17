# Clinic Management System

## Overview
This is a modern .NET 8 Clinic Management System migrated from ASP.NET Web Forms 4.5.2. The application follows clean architecture principles with a layered structure.

## Architecture
- **Domain Layer**: Core business entities and interfaces
- **Application Layer**: Business logic and DTOs
- **Infrastructure Layer**: Data access with Entity Framework Core 8.0
- **Web Layer**: ASP.NET Core Razor Pages

## Technology Stack
- .NET 8
- ASP.NET Core Razor Pages
- Entity Framework Core 8.0
- SQL Server
- AutoMapper
- Serilog
- xUnit for testing

## Prerequisites
- .NET 8 SDK
- SQL Server (LocalDB or Express)
- Visual Studio 2022 or VS Code

## Getting Started

### 1. Clone the repository
```bash
git clone <repository-url>
cd BackendServices
```

### 2. Update connection string
Edit `src/ClinicManagement.Web/appsettings.json` and update the connection string:
```json
"ConnectionStrings": {
  "DefaultConnection": "Data Source=.\\SQLEXPRESS;Initial Catalog=DBProject;Integrated Security=True;TrustServerCertificate=True"
}
```

### 3. Restore packages
```bash
dotnet restore
```

### 4. Build the solution
```bash
dotnet build
```

### 5. Run the application
```bash
cd src/ClinicManagement.Web
dotnet run
```

The application will be available at `https://localhost:5001` or `http://localhost:5000`.

## Project Structure
```
BackendServices/
├── src/
│   ├── ClinicManagement.Domain/          # Domain entities and interfaces
│   ├── ClinicManagement.Application/     # Business logic and DTOs
│   ├── ClinicManagement.Infrastructure/  # Data access and repositories
│   └── ClinicManagement.Web/             # Web UI (Razor Pages)
├── tests/
│   ├── ClinicManagement.UnitTests/       # Unit tests
│   └── ClinicManagement.IntegrationTests/# Integration tests
└── docs/                                  # Documentation
```

## Features
- Patient management (CRUD operations)
- Doctor management
- Appointment scheduling
- Treatment history tracking
- Bill management
- Department management
- Staff management

## Migration Notes
This application was migrated from ASP.NET Web Forms 4.5.2 to .NET 8. Key changes include:
- Replaced System.Web with ASP.NET Core
- Migrated from ADO.NET to Entity Framework Core 8.0
- Converted Web Forms pages to Razor Pages
- Replaced ViewState with modern state management
- Updated authentication to ASP.NET Core Identity patterns
- Migrated Web.config to appsettings.json

## Testing
Run unit tests:
```bash
dotnet test tests/ClinicManagement.UnitTests
```

Run integration tests:
```bash
dotnet test tests/ClinicManagement.IntegrationTests
```

## License
[Your License Here]

## Contributors
[Your Name/Team]
