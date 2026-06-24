# ClinicManagement.Application

This is the Application layer of the ClinicManagement system, implementing the business logic and service layer.

## Project Structure

```
ClinicManagement.Application/
├── DTOs/                    # Data Transfer Objects
│   ├── PatientDto.cs
│   ├── DoctorDto.cs
│   └── AppointmentDto.cs
├── Services/                # Business logic services
│   ├── PatientService.cs
│   ├── DoctorService.cs
│   └── AppointmentService.cs
└── ClinicManagement.Application.csproj
```

## Building the Project

### From this directory:
```bash
./build.sh
```

Or using dotnet CLI:
```bash
dotnet build ClinicManagement.Application.csproj
```

### From the solution root:
```bash
cd ../..
dotnet build ClinicManagement.sln
```

Or use the master build script:
```bash
cd ../..
./build.sh Application
```

## Dependencies

- **ClinicManagement.Domain**: Domain entities and interfaces
- **Microsoft.Extensions.Logging.Abstractions**: Logging support

## Target Framework

- .NET 8.0

## Features

- Patient management services
- Doctor management services
- Appointment management services
- Data validation and business rules
- Comprehensive logging
