#!/bin/bash
cd /modernize-data/studio-data/TNT1001/APP837891/transformed-code/57/studio-workspace/FullComp
dotnet restore src/ClinicManagement.Application/ClinicManagement.Application.csproj --force --no-cache > /tmp/restore.log 2>&1
