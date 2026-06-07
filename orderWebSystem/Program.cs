using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

using orderWebSystem;
using orderWebSystem.Config;
using orderWebSystem.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// HttpClient 登録
builder.Services.AddScoped(sp =>
    new HttpClient
    {
        BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
    });

// ApiEndpoints（wwwroot/appsettings.json から読み込み）
builder.Services.AddScoped<ApiEndpoints>();

// OrderApiService 登録
builder.Services.AddScoped<OrderApiService>();

await builder.Build().RunAsync();
