# ClinicManagement.Domain

## Overview

This is the Domain layer of the ClinicManagement application. It contains the core business entities and repository interfaces.

## Project Structure

```
ClinicManagement.Domain/
├── Entities/              # Domain entities (Doctor, Patient, Appointment, etc.)
├── Interfaces/            # Repository interfaces
├── ClinicManagement.Domain.csproj
├── build.sh              # Build script
├── Makefile              # Alternative build configuration
└── README.md             # This file
```

## Building the Project

### Method 1: Using the build script (Recommended)

```bash
./build.sh
```

### Method 2: Using Make

```bash
make build
```

### Method 3: Using dotnet CLI directly

```bash
dotnet build ClinicManagement.Domain.csproj
```

### Method 4: Using dotnet CLI (auto-discovery)

Since this directory contains only one .csproj file, you can also run:

```bash
dotnet build
```

## Build Options

### Debug Build (Default)
```bash
./build.sh
```

### Release Build
```bash
./build.sh -c Release
# or
make release
```

### Clean Build
```bash
dotnet clean
# or
make clean
```

### Restore Packages
```bash
dotnet restore
# or
make restore
```

## Dependencies

This project has no external dependencies. It's a pure .NET 8.0 class library.

## Target Framework

- .NET 8.0 (net8.0)

## Features

- **Nullable Reference Types**: Enabled
- **Implicit Usings**: Enabled
- **Language Version**: Latest

## Entities

The Domain layer includes the following entities:

- **Doctor**: Represents medical doctors
- **Patient**: Represents patients
- **Appointment**: Represents appointments between doctors and patients
- **Department**: Represents hospital departments
- **Bill**: Represents billing information
- **TreatmentHistory**: Represents patient treatment records
- **FreeSlot**: Represents available appointment slots
- **OtherStaff**: Represents non-doctor staff members

## Repository Interfaces

The Domain layer defines repository interfaces for data access:

- **IDoctorRepository**: Doctor data access interface
- **IPatientRepository**: Patient data access interface
- **IAppointmentRepository**: Appointment data access interface

## Usage in Other Projects

This project is referenced by:
- ClinicManagement.Application
- ClinicManagement.Infrastructure
- ClinicManagement.Web
- ClinicManagement.Tests

## Troubleshooting

### Error: MSB1003 - No project or solution file found

**Solution**: Make sure you're running the build command from this directory, or use the build script:
```bash
./build.sh
```

### Error: Project file not found

**Solution**: Verify that `ClinicManagement.Domain.csproj` exists in this directory:
```bash
ls -la ClinicManagement.Domain.csproj
```

### Error: .NET SDK not found

**Solution**: Install .NET 8.0 SDK or later:
```bash
dotnet --version
```

## Contributing

When adding new entities or interfaces:
1. Place entities in the `Entities/` directory
2. Place repository interfaces in the `Interfaces/` directory
3. Follow the existing naming conventions
4. Ensure all code compiles without warnings

## License

[Add license information here]
