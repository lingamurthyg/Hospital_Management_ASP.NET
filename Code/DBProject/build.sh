#!/bin/bash
# Master build script for ClinicManagement solution
# Usage: ./build.sh [project-name] [additional-args]
# If no project name is provided, builds the entire solution

set -e

SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
cd "$SCRIPT_DIR"

if [ -z "$1" ]; then
    echo "Building entire solution..."
    dotnet build ClinicManagement.sln "${@:2}"
else
    PROJECT_NAME="$1"
    shift
    
    case "$PROJECT_NAME" in
        "Domain"|"domain")
            echo "Building ClinicManagement.Domain..."
            cd src/ClinicManagement.Domain
            dotnet build ClinicManagement.Domain.csproj "$@"
            ;;
        "Application"|"application")
            echo "Building ClinicManagement.Application..."
            cd src/ClinicManagement.Application
            dotnet build ClinicManagement.Application.csproj "$@"
            ;;
        "Infrastructure"|"infrastructure")
            echo "Building ClinicManagement.Infrastructure..."
            cd src/ClinicManagement.Infrastructure
            dotnet build ClinicManagement.Infrastructure.csproj "$@"
            ;;
        "Web"|"web")
            echo "Building ClinicManagement.Web..."
            cd src/ClinicManagement.Web
            dotnet build ClinicManagement.Web.csproj "$@"
            ;;
        "Tests"|"tests")
            echo "Building ClinicManagement.Tests..."
            cd tests/ClinicManagement.Tests
            dotnet build ClinicManagement.Tests.csproj "$@"
            ;;
        *)
            echo "Unknown project: $PROJECT_NAME"
            echo "Available projects: Domain, Application, Infrastructure, Web, Tests"
            echo "Or run without arguments to build the entire solution"
            exit 1
            ;;
    esac
fi

echo "Build completed successfully!"
