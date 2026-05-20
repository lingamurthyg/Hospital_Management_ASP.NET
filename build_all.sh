#!/bin/bash
cd /modernize-data/studio-data/TNT1001/APP837891/transformed-code/57/studio-workspace/FullComp
echo "Starting build at $(date)" > /tmp/build_all.log
dotnet build ClinicManagement.sln --configuration Release >> /tmp/build_all.log 2>&1
echo "Build completed at $(date)" >> /tmp/build_all.log
