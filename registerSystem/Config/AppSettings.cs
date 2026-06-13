using Microsoft.Extensions.Configuration;

namespace registerSystem.Config;

public static class AppSettings
{
    private static readonly IConfigurationRoot Configuration =
        new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false)
            .Build();

    public static string ShopId =>
        Configuration.GetValue<string>("ShopId") ?? string.Empty;

    public static string ApiBaseUrl =>
        Configuration["ApiBaseUrl"] ?? string.Empty;

    public static string ApiKey =>
        Configuration["ApiKey"] ?? string.Empty;

    /// <summary>
    /// orderWebSystem（オーダーシステム）のベースURL。
    /// QRコード発行時に "?shop={ShopId}" を付与してエンコードする。
    /// </summary>
    public static string OrderWebBaseUrl =>
        Configuration["OrderWebBaseUrl"] ?? string.Empty;
}
