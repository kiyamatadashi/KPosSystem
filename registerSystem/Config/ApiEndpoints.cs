namespace registerSystem.Config;

public static class ApiEndpoints
{
    private static string Base =>
        $"{AppSettings.ApiBaseUrl}";

    private static string Code =>
        $"code={AppSettings.ApiKey}";

    public static string Orders =>
        $"{Base}/orders?{Code}";

    public static string SignalRNegotiate =>
        $"{Base}/signalr/negotiate?{Code}";

    public static string UpdateOrderStatus =>
        $"{Base}/orders/status?{Code}";

    public static string Groups =>
        $"{Base}/groups?{Code}";

    public static string Masters =>
        $"{Base}/masters/product?{Code}";

    public static string CastMaster =>
        $"{Base}/masters/cast?{Code}";

    public static string SeatMaster =>
        $"{Base}/masters/seat?{Code}";
}
