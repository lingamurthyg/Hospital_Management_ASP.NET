.PHONY: build clean restore test

# Default target
all: build

# Build the project
build:
	dotnet build ClinicManagement.Domain.csproj

# Clean build artifacts
clean:
	dotnet clean ClinicManagement.Domain.csproj

# Restore NuGet packages
restore:
	dotnet restore ClinicManagement.Domain.csproj

# Run tests (if any)
test:
	@echo "No tests defined for Domain project"

# Build in release mode
release:
	dotnet build ClinicManagement.Domain.csproj -c Release
