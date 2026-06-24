# Build Instructions for ClinicManagement Solution

## Overview

This document provides comprehensive instructions for building the ClinicManagement solution and its individual projects.

## Prerequisites

- .NET 8.0 SDK or later
- Visual Studio 2022 (optional) or any text editor
- Command line terminal (bash, PowerShell, or cmd)

## Solution Structure

```
ClinicManagement/
├── src/
│   ├── ClinicManagement.Domain/          # Domain entities and interfaces
│   ├── ClinicManagement.Application/     # Business logic and services
│   ├── ClinicManagement.Infrastructure/  # Data access and external services
│   └── ClinicManagement.Web/             # Web API and presentation layer
├── tests/
│   └── ClinicManagement.Tests/           # Unit and integration tests
├── ClinicManagement.sln                  # Solution file
└── build.sh                              # Master build script
```

## Building the Entire Solution

### Method 1: Using the Master Build Script (Recommended)

From the solution root directory:

```bash
./build.sh
```

This will build all projects in the correct dependency order.

### Method 2: Using dotnet CLI

From the solution root directory:

```bash
dotnet build ClinicManagement.sln
```

### Method 3: Using Visual Studio

1. Open `ClinicManagement.sln` in Visual Studio
2. Press `Ctrl+Shift+B` or select `Build > Build Solution`

## Building Individual Projects

### Using the Master Build Script

From the solution root directory:

```bash
# Build Domain project
./build.sh Domain

# Build Application project
./build.sh Application

# Build Infrastructure project
./build.sh Infrastructure

# Build Web project
./build.sh Web

# Build Tests project
./build.sh Tests
```

### Using Project-Specific Build Scripts

Each project has its own build script. Navigate to the project directory and run:

```bash
# For Domain
cd src/ClinicManagement.Domain
./build.sh

# For Application
cd src/ClinicManagement.Application
./build.sh

# For Infrastructure
cd src/ClinicManagement.Infrastructure
./build.sh

# For Web
cd src/ClinicManagement.Web
./build.sh

# For Tests
cd tests/ClinicManagement.Tests
./build.sh
```

### Using dotnet CLI Directly

From any project directory:

```bash
dotnet build <ProjectName>.csproj
```

Example:
```bash
cd src/ClinicManagement.Application
dotnet build ClinicManagement.Application.csproj
```

## Build Configurations

### Debug Build (Default)

```bash
dotnet build
# or
./build.sh
```

### Release Build

```bash
dotnet build -c Release
# or
./build.sh -c Release
```

## Common Build Issues and Solutions

### Issue: MSB1003 - No project or solution file found

**Cause**: Running `dotnet build` without specifying a project/solution file in a directory that doesn't contain one.

**Solution**: 
- Use the project-specific build scripts: `./build.sh`
- Or specify the project file: `dotnet build <ProjectName>.csproj`
- Or navigate to the solution root and build from there

### Issue: Project reference not found

**Cause**: Project references are not properly restored.

**Solution**:
```bash
dotnet restore
dotnet build
```

### Issue: Package restore failed

**Cause**: NuGet packages are not restored or network issues.

**Solution**:
```bash
dotnet restore --force
dotnet build
```

## Cleaning Build Artifacts

### Clean all projects

```bash
dotnet clean ClinicManagement.sln
```

### Clean and rebuild

```bash
dotnet clean
dotnet build
```

## Dependency Order

The projects have the following dependency hierarchy:

1. **ClinicManagement.Domain** (no dependencies)
2. **ClinicManagement.Application** (depends on Domain)
3. **ClinicManagement.Infrastructure** (depends on Domain)
4. **ClinicManagement.Web** (depends on Application and Infrastructure)
5. **ClinicManagement.Tests** (depends on all projects)

When building manually, follow this order to avoid build errors.

## Additional Build Options

### Verbose Output

```bash
dotnet build -v detailed
```

### Build with specific framework

```bash
dotnet build -f net8.0
```

### Build without restoring packages

```bash
dotnet build --no-restore
```

## Continuous Integration

For CI/CD pipelines, use:

```bash
dotnet restore
dotnet build --no-restore -c Release
dotnet test --no-build -c Release
```

## Support

For build issues or questions, please refer to:
- Project README files in each project directory
- .NET documentation: https://docs.microsoft.com/dotnet/
- Project issue tracker
