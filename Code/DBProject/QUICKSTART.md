# Quick Build Reference

## TL;DR - Just Build It!

```bash
# From solution root
./build.sh

# Or with dotnet CLI
dotnet build ClinicManagement.sln
```

## Build Individual Projects

```bash
# From solution root
./build.sh Application    # Build Application layer
./build.sh Domain         # Build Domain layer
./build.sh Infrastructure # Build Infrastructure layer
./build.sh Web            # Build Web API
./build.sh Tests          # Build Tests

# Or navigate to project directory
cd src/ClinicManagement.Application
./build.sh
```

## Common Commands

```bash
# Clean and rebuild
dotnet clean && dotnet build

# Release build
./build.sh -c Release

# Restore packages
dotnet restore

# Run tests
dotnet test
```

## Troubleshooting

**Error: MSB1003 - No project file found**
- Solution: Use `./build.sh` or specify the .csproj file explicitly
- See [TROUBLESHOOTING.md](TROUBLESHOOTING.md) for detailed solutions

**Error: Project reference not found**
- Solution: Run `dotnet restore` first

**Error: SDK not found**
- Solution: Install .NET 8.0 SDK or later

For detailed instructions, see:
- [BUILD.md](BUILD.md) - Comprehensive build guide
- [TROUBLESHOOTING.md](TROUBLESHOOTING.md) - Detailed error solutions
