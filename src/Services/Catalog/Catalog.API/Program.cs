using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var services = builder.Services;

var assemly = typeof(Program).Assembly;

services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(assemly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
    //config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});

services.AddValidatorsFromAssembly(assemly);

services.AddCarter();

var connectionString = builder.Configuration.GetConnectionString("Database");

services.AddMarten(options =>
{
    options.Connection(connectionString!);
}).UseLightweightSessions();

if (builder.Environment.IsDevelopment())
    services.InitializeMartenWith<CatalogInitialData>();

services.AddExceptionHandler<CustomExceptionHandler>();

services.AddHealthChecks()
    .AddNpgSql(connectionString!);

var app = builder.Build();

// Configure the Http request pipeline
app.MapCarter();

app.UseExceptionHandler(options => { });

app.UseHealthChecks("/health", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.Run();
