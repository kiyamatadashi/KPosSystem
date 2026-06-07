using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

using orderWebSystem;
using orderWebSystem.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// HttpClient 登録（コールドスタート対策: タイムアウトはService側で90秒に設定）
builder.Services.AddScoped(sp =>
    new HttpClient
    {
        BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
    });

// OrderApiService 登録
builder.Services.AddScoped<OrderApiService>();

await builder.Build().RunAsync();
