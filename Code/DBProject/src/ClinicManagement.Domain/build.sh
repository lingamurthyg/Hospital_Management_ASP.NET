#!/bin/bash
# Build script for ClinicManagement.Domain
# This script ensures the project builds correctly regardless of how it's invoked

set -e

# Get the directory where this script is located
SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
cd "$SCRIPT_DIR"

# Check if the project file exists
if [ ! -f "ClinicManagement.Domain.csproj" ]; then
    echo "Error: ClinicManagement.Domain.csproj not found in $SCRIPT_DIR"
    exit 1
fi

# Build the project with explicit project file reference
echo "Building ClinicManagement.Domain..."
dotnet build ClinicManagement.Domain.csproj "$@"

echo "Build completed successfully!"
