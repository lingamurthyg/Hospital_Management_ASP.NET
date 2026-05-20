#!/bin/bash
cd /modernize-data/studio-data/TNT1001/APP837891/transformed-code/57/studio-workspace/FullComp
dotnet build src/ClinicManagement.Infrastructure/ClinicManagement.Infrastructure.csproj > /tmp/build_infra.log 2>&1
echo "Build completed at $(date)" >> /tmp/build_infra.log
