using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using azureDatabase.Infrastructure.Database;
using azureDatabase.Repositories.Interfaces;
using azureDatabase.Repositories.Groups;
using azureDatabase.Repositories.Masters;
using azureDatabase.Repositories.Orders;
using azureDatabase.Services.Groups;
using azureDatabase.Services.Masters;
using azureDatabase.Services.Orders;

var builder =
    FunctionsApplication.CreateBuilder(args);

builder.ConfigureFunctionsWebApplication();

builder.Services.AddSingleton<
    DapperContext>();

// Orders
builder.Services.AddScoped<
    IOrderRepository,
    OrderRepository>();

builder.Services.AddScoped<
    OrderService>();

// Groups
builder.Services.AddScoped<
    IGroupRepository,
    GroupRepository>();

builder.Services.AddScoped<
    GroupService>();

// Masters
builder.Services.AddScoped<
    IMasterRepository,
    MasterRepository>();

builder.Services.AddScoped<
    MasterService>();

builder.Build().Run();
