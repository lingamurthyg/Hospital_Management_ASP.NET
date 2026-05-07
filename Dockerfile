# escape=`

# Build stage - uses .NET Framework SDK
FROM mcr.microsoft.com/dotnet/framework/sdk:4.8 AS builder

SHELL ["powershell", "-Command", "$ErrorActionPreference = 'Stop'; $ProgressPreference = 'SilentlyContinue';"]

# Install NuGet
RUN Invoke-WebRequest -Uri https://dist.nuget.org/win-x86-commandline/latest/nuget.exe -OutFile C:\nuget.exe

# Set working directory
WORKDIR C:\build

# Copy solution and project files first for dependency caching
COPY ["Code/Clinic Management System.sln", "./"]
COPY ["Code/DBProject/Clinic Management System.csproj", "DBProject/"]
COPY ["Code/DBProject/packages.config", "DBProject/"]

# Restore NuGet packages
RUN C:\nuget.exe restore "Clinic Management System.sln"

# Copy all source code
COPY Code/ .

# Build the application
RUN msbuild "DBProject\Clinic Management System.csproj" /p:Configuration=Release /p:OutputPath=C:\app /p:DeployOnBuild=true /p:WebPublishMethod=FileSystem /p:publishUrl=C:\app /p:PrecompileBeforePublish=true /p:EnableUpdateable=false /t:Build

# Runtime stage - uses ASP.NET Framework runtime
FROM mcr.microsoft.com/dotnet/framework/aspnet:4.8

SHELL ["powershell", "-Command", "$ErrorActionPreference = 'Stop'; $ProgressPreference = 'SilentlyContinue';"]

# Set working directory to IIS website root
WORKDIR C:\inetpub\wwwroot

# Remove default IIS website
RUN Remove-Item -Path C:\inetpub\wwwroot\* -Recurse -Force

# Copy published application from builder
COPY --from=builder C:\app .

# Configure IIS Application Pool
RUN Import-Module WebAdministration; `
    Set-ItemProperty 'IIS:\AppPools\DefaultAppPool' -Name processModel.identityType -Value 2; `
    Set-ItemProperty 'IIS:\AppPools\DefaultAppPool' -Name managedRuntimeVersion -Value 'v4.0'; `
    Set-ItemProperty 'IIS:\AppPools\DefaultAppPool' -Name enable32BitAppOnWin64 -Value $false

# Set environment variables
ENV DB_CONNECTION_STRING="Data Source={{DB_HOST}};Initial Catalog=DBProject;User ID={{DB_USER}};Password={{DB_PASSWORD}};" `
    ASPNET_ENVIRONMENT=Production

# Update Web.config connection string at runtime using environment variable
RUN $webConfig = 'C:\inetpub\wwwroot\Web.config'; `
    if (Test-Path $webConfig) { `
        $xml = [xml](Get-Content $webConfig); `
        $connString = $xml.configuration.connectionStrings.add | Where-Object {$_.name -eq 'sqlCon1'}; `
        if ($connString) { `
            $connString.connectionString = $env:DB_CONNECTION_STRING; `
            $xml.Save($webConfig); `
        } `
    }

# Expose port 80
EXPOSE 80

# The base image already has the startup configured for IIS
# No explicit ENTRYPOINT needed - IIS will start automatically