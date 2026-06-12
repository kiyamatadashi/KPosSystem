namespace orderWebSystem.Config;

/// <summary>
/// APIエンドポイント。
/// キーは wwwroot/appsettings.json から読み込む（.gitignore対象）。
/// GitHub Actions Secret で CI/CD 時に自動生成される。
/// </summary>
public class ApiEndpoints
{
    private readonly string _base;
    private readonly string _code;

    public ApiEndpoints(IConfiguration configuration)
    {
        _base  = configuration["ApiBaseUrl"] ?? string.Empty;

        // ApiKey末尾の "==" 等がクエリ文字列パースで後続パラメータと混ざるのを防ぐため、
        // URLエンコードしてから "code=" パラメータに設定する。
        _code  = $"code={Uri.EscapeDataString(configuration["ApiKey"] ?? string.Empty)}";
        ShopId = configuration["ShopId"] ?? string.Empty;
    }

    /// <summary>店舗ID（wwwroot/appsettings.json の ShopId）。</summary>
    public string ShopId { get; }

    public string Orders             => $"{_base}/orders?{_code}";
    public string UpdateOrderStatus  => $"{_base}/orders/status?{_code}";
    public string Groups             => $"{_base}/groups?{_code}";
    public string CastMaster         => $"{_base}/masters/cast?{_code}";
    public string ProductMaster      => $"{_base}/masters/product?{_code}";
    public string SeatMaster         => $"{_base}/masters/seat?{_code}";
}
