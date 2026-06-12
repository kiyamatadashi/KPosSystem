using Microsoft.AspNetCore.Components;

namespace orderWebSystem.Config;

/// <summary>
/// APIエンドポイント。
/// APIキー・URLは wwwroot/appsettings.json から読み込む（.gitignore対象）。
/// GitHub Actions Secret で CI/CD 時に自動生成される。
///
/// ShopId は appsettings.json では管理せず、URLクエリパラメータ "shop" から取得する。
/// 例: https://xxxx.azurestaticapps.net/?shop=0001
/// 複数店舗で同一デプロイを共有し、店舗ごとにQRコードのURL（shopパラメータ）を
/// 変えることで対応する（registerSystem側でQRコード発行時にshopパラメータを付与）。
/// </summary>
public class ApiEndpoints
{
    private readonly string _base;
    private readonly string _code;

    public ApiEndpoints(IConfiguration configuration, NavigationManager navigationManager)
    {
        _base  = configuration["ApiBaseUrl"] ?? string.Empty;

        // ApiKey末尾の "==" 等がクエリ文字列パースで後続パラメータと混ざるのを防ぐため、
        // URLエンコードしてから "code=" パラメータに設定する。
        _code  = $"code={Uri.EscapeDataString(configuration["ApiKey"] ?? string.Empty)}";

        ShopId = ParseShopId(navigationManager.ToAbsoluteUri(navigationManager.Uri).Query);
    }

    /// <summary>
    /// 店舗ID。アクセスURLのクエリパラメータ "shop" から取得する。
    /// 例: ?shop=0001 → "0001"
    /// パラメータが無い場合は空文字。
    /// </summary>
    public string ShopId { get; }

    public string Orders             => $"{_base}/orders?{_code}";
    public string UpdateOrderStatus  => $"{_base}/orders/status?{_code}";
    public string Groups             => $"{_base}/groups?{_code}";
    public string CastMaster         => $"{_base}/masters/cast?{_code}";
    public string ProductMaster      => $"{_base}/masters/product?{_code}";
    public string SeatMaster         => $"{_base}/masters/seat?{_code}";

    /// <summary>
    /// クエリ文字列（"?shop=0001&amp;..." 形式）から "shop" パラメータの値を取り出す。
    /// 外部パッケージに依存せず簡易パースする。
    /// </summary>
    private static string ParseShopId(string query)
    {
        if (string.IsNullOrEmpty(query))
            return string.Empty;

        var trimmed = query.TrimStart('?');
        foreach (var pair in trimmed.Split('&'))
        {
            var kv = pair.Split('=', 2);
            if (kv.Length == 2 && kv[0] == "shop")
                return Uri.UnescapeDataString(kv[1]);
        }

        return string.Empty;
    }
}
