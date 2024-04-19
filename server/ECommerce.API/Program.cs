using ECommerce.API.Extensions;
using ECommerce.Services;
using ECommerce.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddApiServices();

var app = builder.Build();

app.UseApiServices();

app.Run();