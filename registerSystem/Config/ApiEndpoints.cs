namespace registerSystem.Config;

public static class ApiEndpoints
{
    public const string BaseUrl = "https://k-pos-system-api-func-abcydpc4b9g5a7hh.japanwest-01.azurewebsites.net/api";
    public const string Orders = $"{BaseUrl}/orders?code=C6GJNDi4_TD0AtuK8bvITIOu5JXrupHFCiAZeri2nvw2AzFuGvcB5Q==";
    public const string SignalRNegotiate = $"{BaseUrl}/signalr/negotiate?code=C6GJNDi4_TD0AtuK8bvITIOu5JXrupHFCiAZeri2nvw2AzFuGvcB5Q==";
}