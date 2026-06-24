# Build Troubleshooting Guide

## Error: MSB1003 - Specify a project or solution file

### Problem
```
MSBUILD : error MSB1003: Specify a project or solution file. The current working directory does not contain a project or solution file.
```

### Root Cause
This error occurs when:
1. Running `dotnet build` or `msbuild` without specifying a project/solution file
2. The current working directory doesn't contain a .csproj or .sln file
3. The build command is executed from the wrong directory

### Solutions

#### Solution 1: Use the build script (RECOMMENDED)
```bash
cd src/ClinicManagement.Domain
./build.sh
```

The build script automatically:
- Locates the correct project file
- Sets the correct working directory
- Passes all arguments to dotnet build

#### Solution 2: Specify the project file explicitly
```bash
cd src/ClinicManagement.Domain
dotnet build ClinicManagement.Domain.csproj
```

#### Solution 3: Use Make
```bash
cd src/ClinicManagement.Domain
make build
```

#### Solution 4: Build from solution root
```bash
# From the solution root directory
dotnet build ClinicManagement.sln

# Or use the master build script
./build.sh Domain
```

#### Solution 5: Use dotnet build with auto-discovery
If you're in a directory with exactly one .csproj file:
```bash
cd src/ClinicManagement.Domain
dotnet build
```

### Verification Steps

1. **Verify you're in the correct directory:**
```bash
pwd
ls -la *.csproj
```

Expected output should show `ClinicManagement.Domain.csproj`

2. **Verify the project file exists:**
```bash
cat ClinicManagement.Domain.csproj
```

3. **Verify .NET SDK is installed:**
```bash
dotnet --version
```

Should show version 8.0.0 or later

4. **Test the build script:**
```bash
./build.sh --help
```

### Common Mistakes

❌ **Wrong:** Running `dotnet build` from parent directory without specifying project
```bash
cd src
dotnet build  # ERROR: Multiple projects in subdirectories
```

✅ **Correct:** Specify the project or use the build script
```bash
cd src/ClinicManagement.Domain
./build.sh
```

❌ **Wrong:** Running msbuild without project file
```bash
msbuild  # ERROR: No project file specified
```

✅ **Correct:** Use dotnet build with project file
```bash
dotnet build ClinicManagement.Domain.csproj
```

### Build System Integration

If you're integrating with an automated build system:

#### For CI/CD pipelines:
```yaml
# Example for GitHub Actions, Azure DevOps, etc.
- name: Build Domain Project
  run: |
    cd src/ClinicManagement.Domain
    dotnet build ClinicManagement.Domain.csproj -c Release
```

#### For Docker builds:
```dockerfile
WORKDIR /app/src/ClinicManagement.Domain
RUN dotnet build ClinicManagement.Domain.csproj -c Release
```

#### For custom build scripts:
```bash
#!/bin/bash
cd "$(dirname "$0")/src/ClinicManagement.Domain"
dotnet build ClinicManagement.Domain.csproj "$@"
```

### Additional Resources

- **Build Documentation:** See BUILD.md in the solution root
- **Quick Start:** See QUICKSTART.md in the solution root
- **Project README:** See README.md in each project directory

### Still Having Issues?

1. Clean the build artifacts:
```bash
dotnet clean
rm -rf bin obj
```

2. Restore packages:
```bash
dotnet restore
```

3. Try building the entire solution:
```bash
cd ../../..  # Navigate to solution root
dotnet build ClinicManagement.sln
```

4. Check for file system issues:
```bash
# Verify file permissions
ls -la ClinicManagement.Domain.csproj

# Verify file encoding
file ClinicManagement.Domain.csproj
```

5. Enable verbose logging:
```bash
dotnet build -v detailed
```

### Contact

If none of these solutions work, please provide:
- The exact command you're running
- The directory you're running it from (`pwd` output)
- The full error message
- Output of `dotnet --info`
