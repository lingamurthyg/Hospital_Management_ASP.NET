# Clinic Management System - .NET 8

A modern clinic management system built with .NET 8, following Clean Architecture principles.

## Architecture

This solution follows Clean Architecture with the following layers:

- **Domain Layer**: Contains entities, interfaces, and domain logic
- **Application Layer**: Contains business logic, services, and DTOs
- **Infrastructure Layer**: Contains data access, repositories, and external services
- **Web Layer**: Contains API controllers and presentation logic

## Technologies

- .NET 8
- ASP.NET Core Web API
- Entity Framework Core 8.0
- SQL Server
- Swagger/OpenAPI
- xUnit for testing

## Project Structure

```
ClinicManagement/
├── src/
│   ├── ClinicManagement.Domain/
│   │   ├── Entities/
│   │   └── Interfaces/
│   ├── ClinicManagement.Application/
│   │   ├── Services/
│   │   └── DTOs/
│   ├── ClinicManagement.Infrastructure/
│   │   ├── Data/
│   │   └── Repositories/
│   └── ClinicManagement.Web/
│       ├── Controllers/
│       └── Program.cs
└── tests/
    └── ClinicManagement.Tests/
```

## Getting Started

### Prerequisites

- .NET 8 SDK
- SQL Server (LocalDB or Express)

### Setup

1. Update the connection string in `appsettings.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=.\\SQLEXPRESS;Initial Catalog=DBProject;Integrated Security=True;TrustServerCertificate=True"
  }
}
```

2. Run database migrations:
```bash
cd src/ClinicManagement.Web
dotnet ef database update
```

3. Run the application:
```bash
dotnet run
```

4. Access Swagger UI at: `https://localhost:5001/swagger`

## API Endpoints

### Patients
- `GET /api/patients` - Get all patients
- `GET /api/patients/{id}` - Get patient by ID
- `GET /api/patients/search?name={name}` - Search patients by name
- `POST /api/patients/register` - Register new patient
- `POST /api/patients/login` - Patient login

### Doctors
- `GET /api/doctors` - Get all doctors
- `GET /api/doctors/{id}` - Get doctor by ID
- `GET /api/doctors/department/{deptNo}` - Get doctors by department
- `POST /api/doctors/register` - Register new doctor
- `POST /api/doctors/login` - Doctor login

### Appointments
- `GET /api/appointments/{id}` - Get appointment by ID
- `GET /api/appointments/patient/{patientId}` - Get appointments by patient
- `GET /api/appointments/doctor/{doctorId}/pending` - Get pending appointments
- `POST /api/appointments` - Create new appointment
- `PUT /api/appointments/{id}/approve` - Approve appointment
- `PUT /api/appointments/{id}/prescription` - Update prescription

## Features

- Patient registration and management
- Doctor registration and management
- Appointment scheduling and management
- Treatment history tracking
- Bill generation and management
- Department management
- Staff management

## Testing

Run tests using:
```bash
dotnet test
```

## Migration from ASP.NET Web Forms

This application has been migrated from ASP.NET Web Forms 4.5.2 to .NET 8 with the following improvements:

- Modern REST API architecture
- Clean Architecture pattern
- Entity Framework Core for data access
- Dependency Injection
- Async/await patterns
- Comprehensive logging
- Unit testing support
- Swagger documentation

## License

This project is licensed under the MIT License.
