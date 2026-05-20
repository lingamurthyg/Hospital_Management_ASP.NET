#!/bin/bash
cd /modernize-data/studio-data/TNT1001/APP837891/transformed-code/57/studio-workspace/FullComp
echo "=== Final Build Attempt ===" > /tmp/final_build.log
echo "Starting at $(date)" >> /tmp/final_build.log
dotnet build src/ClinicManagement.Application/ClinicManagement.Application.csproj >> /tmp/final_build.log 2>&1
echo "Application build done" >> /tmp/final_build.log
dotnet build src/ClinicManagement.Infrastructure/ClinicManagement.Infrastructure.csproj >> /tmp/final_build.log 2>&1
echo "Infrastructure build done" >> /tmp/final_build.log
dotnet build src/ClinicManagement.Web/ClinicManagement.Web.csproj >> /tmp/final_build.log 2>&1
echo "Web build done" >> /tmp/final_build.log
echo "Completed at $(date)" >> /tmp/final_build.log
