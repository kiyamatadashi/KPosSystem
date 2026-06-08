using Microsoft.AspNetCore.SignalR.Client;

using shared.Models.SignalR;

using registerSystem.Config;

using System.Net.Http;
using System.Net.Http.Json;

namespace registerSystem.Services;

public class SignalRService
{
    private HubConnection? _connection;

    private const int NegotiateTimeoutSeconds = 90;
    private const int MaxRetryCount           = 3;
    private const int RetryIntervalSeconds    = 5; // コールドスタート後のウォームアップを考慮して5秒

    public event Action<List<OrderCreatedMessage>>? OrderCreated;

    public async Task StartAsync()
    {
        Exception? lastException = null;

        for (int attempt = 1; attempt <= MaxRetryCount; attempt++)
        {
            try
            {
                await ConnectAsync();
                return; // 接続成功
            }
            catch (Exception ex)
            {
                // StartAsync・ConnectAsync どちらの例外も全てリトライ対象
                lastException = ex;
            }

            if (attempt < MaxRetryCount)
                await Task.Delay(TimeSpan.FromSeconds(RetryIntervalSeconds));
        }

        throw new Exception(
            $"SignalR接続に失敗しました（{MaxRetryCount}回試行）。\n原因: {lastException?.Message}",
            lastException);
    }

    private async Task ConnectAsync()
    {
        using var http = new HttpClient
        {
            Timeout = TimeSpan.FromSeconds(NegotiateTimeoutSeconds)
        };

        var response = await http.PostAsync(ApiEndpoints.SignalRNegotiate, null);
        response.EnsureSuccessStatusCode();

        var result = await response.Content
            .ReadFromJsonAsync<SignalRConnectionResponse>();

        if (result is null)
            throw new Exception("SignalRネゴシエーションのレスポンスが空でした。");

        // 前回の接続が残っている場合は破棄
        if (_connection is not null)
        {
            await _connection.DisposeAsync();
            _connection = null;
        }

        _connection = new HubConnectionBuilder()
            .WithUrl(result.Url, options =>
            {
                options.AccessTokenProvider =
                    () => Task.FromResult(result.AccessToken)!;
            })
            .WithAutomaticReconnect(new[]
            {
                TimeSpan.FromSeconds(2),
                TimeSpan.FromSeconds(5),
                TimeSpan.FromSeconds(10),
                TimeSpan.FromSeconds(30),
            })
            .Build();

        _connection.On<List<OrderCreatedMessage>>(
            "OrderCreated",
            messages => OrderCreated?.Invoke(messages));

        // StartAsync もタイムアウトが起きうるためキャンセルトークンで制御
        using var cts = new CancellationTokenSource(
            TimeSpan.FromSeconds(NegotiateTimeoutSeconds));
        await _connection.StartAsync(cts.Token);
    }
}
