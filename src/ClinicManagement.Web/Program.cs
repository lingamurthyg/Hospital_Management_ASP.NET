using ClinicManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Configure Serilog
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.File("logs/clinic-management-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Host.UseSerilog();

// Add services to the container
builder.Services.AddRazorPages();

// Configure DbContext for PostgreSQL
builder.Services.AddDbContext<ClinicDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        npgsqlOptions => npgsqlOptions
            .MigrationsHistoryTable("__efmigrations_history", "public")
            .EnableRetryOnFailure(
                maxRetryCount: 5,
                maxRetryDelay: TimeSpan.FromSeconds(30),
                errorCodesToAdd: null))
    .UseSnakeCaseNamingConvention());

// Register repositories
builder.Services.AddScoped<ClinicManagement.Domain.Interfaces.Repositories.IPatientRepository, 
    ClinicManagement.Infrastructure.Repositories.PatientRepository>();
builder.Services.AddScoped<ClinicManagement.Domain.Interfaces.Repositories.IDoctorRepository, 
    ClinicManagement.Infrastructure.Repositories.DoctorRepository>();
builder.Services.AddScoped<ClinicManagement.Domain.Interfaces.Repositories.IAppointmentRepository, 
    ClinicManagement.Infrastructure.Repositories.AppointmentRepository>();
builder.Services.AddScoped<ClinicManagement.Domain.Interfaces.Repositories.IDepartmentRepository, 
    ClinicManagement.Infrastructure.Repositories.DepartmentRepository>();
builder.Services.AddScoped<ClinicManagement.Domain.Interfaces.Repositories.IStaffRepository, 
    ClinicManagement.Infrastructure.Repositories.StaffRepository>();
builder.Services.AddScoped<ClinicManagement.Domain.Interfaces.Repositories.IFeedbackRepository, 
    ClinicManagement.Infrastructure.Repositories.FeedbackRepository>();

// Configure session
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Configure HTTP context accessor
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseSession();

app.MapRazorPages();

try
{
    Log.Information("Starting Clinic Management System");
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}
