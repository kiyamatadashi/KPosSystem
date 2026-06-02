using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using azureDatabase.Infrastructure.Database;
using azureDatabase.Repositories.Interfaces;
using azureDatabase.Repositories.Orders;
using azureDatabase.Services.Orders;

var builder =
    FunctionsApplication.CreateBuilder(args);

builder.ConfigureFunctionsWebApplication();

builder.Services.AddSingleton<
    DapperContext>();

builder.Services.AddScoped<
    IOrderRepository,
    OrderRepository>();

builder.Services.AddScoped<
    OrderService>();

builder.Build().Run();