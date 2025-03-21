using Presentation.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var services = builder.Services;

services.AddPersistence();
services.AddDatabase();

var app = builder.Build();

app.UseExternalServices();

app.Run();
