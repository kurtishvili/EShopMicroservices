var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var services = builder.Services;

services.AddCarter();
services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);
});

var connectionString = builder.Configuration.GetConnectionString("Database");

services.AddMarten(options =>
{
    options.Connection(connectionString!);
}).UseLightweightSessions();

var app = builder.Build();

// Configure the Http request pipeline

app.MapCarter();

app.Run();
