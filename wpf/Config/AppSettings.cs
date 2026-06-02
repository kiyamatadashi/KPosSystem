using Microsoft.Extensions.Configuration;

namespace wpf.Config;

public static class AppSettings
{
    private static readonly IConfigurationRoot Configuration =
        new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false)
            .Build();

    public static string ShopId =>
        Configuration.GetValue<string>("ShopId");

    public static string ApiBaseUrl =>
        Configuration["ApiBaseUrl"] ?? string.Empty;
}