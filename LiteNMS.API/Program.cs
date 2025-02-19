using Microsoft.EntityFrameworkCore;
using FluentValidation;
using LIteNMS.Application.Contracts;
using LiteNMS.DTOS;
using LiteNMS.Infrastructure;
using LiteNMS.Infrastructure.Cache;
using LiteNMS.Infrastructure.DBContexts;
using LiteNMS.Repositories;
using LiteNMS.Services;
using LiteNMS.Validators;
using Quartz;
using Quartz.Spi;
using Quartz.Impl;
using Quartz.Simpl;

var builder = WebApplication.CreateBuilder(args);

// Load Configuration
var configuration = builder.Configuration;


// ✅ Register Quartz Hosted Service
builder.Services.AddQuartz(q =>
{
    q.UseMicrosoftDependencyInjectionJobFactory(); // Registers IJobFactory
});

// ✅ Register Quartz Scheduler Factory
builder.Services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();

// ✅ Register Custom Job Factory
builder.Services.AddSingleton<IJobFactory, MicrosoftDependencyInjectionJobFactory>();

// Add DB Context
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));


// Add Repositories
builder.Services.AddScoped<ICredentialProfileRepository, CredentialProfileRepository>();
builder.Services.AddScoped<IDiscoveryProfileRepository, DiscoveryProfileRepository>();
builder.Services.AddScoped<IDeviceDiscoveryRepository, DeviceDiscoveryRepository>();
builder.Services.AddScoped<IDeviceProvisionRepository, DeviceProvisionRepository>();
builder.Services.AddScoped<IDeviceProvisionCacheRepository, DeviceProvisionCacheRepository>();


// Add Services
builder.Services.AddScoped<ICredentialProfileService, CredentialProfileService>();
builder.Services.AddScoped<IDiscoveryProfileService, DiscoveryProfileService>();
builder.Services.AddScoped<IDiscoveryService, DiscoveryService>();
builder.Services.AddScoped<IGoPluginService, GoPluginService>();
builder.Services.AddSingleton<IQuartzSchedulerService, QuartzSchedulerService>();
builder.Services.AddScoped<IDeviceProvisionService, DeviceProvisionService>();

// Add Validators
builder.Services.AddScoped<IValidator<CredentialProfileRequest>, CredentialProfileValidator>();
builder.Services.AddScoped<IValidator<DiscoveryProfileRequestDto>, DiscoveryProfileDtoValidator>();

// Add Controllers
builder.Services.AddControllers();

// Add Swagger for API Documentation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Enable CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
        builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});

var app = builder.Build();

// Configure Middleware
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAll");

app.UseAuthorization();
app.MapControllers();
await app.Services.GetRequiredService<IQuartzSchedulerService>().StartSchedulerAsync();
app.Run();