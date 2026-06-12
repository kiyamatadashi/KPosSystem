namespace drinkOrderSystem.Config;

public static class ApiEndpoints
{
    private static string Base =>
        $"{AppSettings.ApiBaseUrl}";

    // ApiKey末尾の "==" 等がクエリ文字列パースで後続パラメータと混ざるのを防ぐため、
    // URLエンコードしてから "code=" パラメータに設定する。
    private static string Code =>
        $"code={Uri.EscapeDataString(AppSettings.ApiKey)}";

    public static string Orders =>
        $"{Base}/orders?{Code}";

    public static string SignalRNegotiate =>
        $"{Base}/signalr/negotiate?{Code}";

    public static string UpdateOrderStatus =>
        $"{Base}/orders/status?{Code}";

    public static string Groups =>
        $"{Base}/groups?{Code}";
}
