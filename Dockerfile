# Build stage
FROM mcr.microsoft.com/dotnet/framework/sdk:4.8 AS builder

WORKDIR /app

# Copy solution and project files for dependency caching
COPY ["Code/Clinic Management System.sln", "Code/"]
COPY ["Code/DBProject/Clinic Management System.csproj", "Code/DBProject/"]
COPY ["Code/packages.config", "Code/"]

# Restore NuGet packages
RUN nuget restore "Code/Clinic Management System.sln"

# Copy the rest of the source code
COPY Code/ Code/

# Build the application
WORKDIR /app/Code
RUN msbuild "Clinic Management System.sln" /p:Configuration=Release /p:DeployOnBuild=true /p:PublishProfile=FolderProfile /p:OutputPath=/app/publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/framework/aspnet:4.8

WORKDIR /inetpub/wwwroot

# Copy published application
COPY --from=builder /app/publish .

# Configure IIS
RUN powershell -NoProfile -Command \
    Remove-Website -Name 'Default Web Site'; \
    New-Website -Name 'ClinicApp' -Port 80 -PhysicalPath 'C:\inetpub\wwwroot' -ApplicationPool '.NET v4.5'

# Expose port
EXPOSE 80

# Health check endpoint (optional - Kubernetes will handle this)
# Application startup is handled by IIS automatically