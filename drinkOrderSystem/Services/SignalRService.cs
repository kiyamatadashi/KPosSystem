using Microsoft.AspNetCore.SignalR.Client;

using shared.Models.SignalR;

using drinkOrderSystem.Config;

using System.Net.Http;
using System.Net.Http.Json;

namespace drinkOrderSystem.Services;

public class SignalRService
{
    private HubConnection? _connection;

    private const int NegotiateTimeoutSeconds = 90;
    private const int MaxRetryCount           = 3;
    private const int RetryIntervalSeconds    = 2;

    public event Action<List<OrderCreatedMessage>>? OrderCreated;

    public async Task StartAsync()
    {
        Exception? lastException = null;

        for (int attempt = 1; attempt <= MaxRetryCount; attempt++)
        {
            try
            {
                await ConnectAsync();
                return;
            }
            catch (TaskCanceledException ex) { lastException = ex; }
            catch (HttpRequestException ex)  { lastException = ex; }
            catch (Exception ex)
            {
                throw new Exception("SignalR接続中に予期しないエラーが発生しました。", ex);
            }

            if (attempt < MaxRetryCount)
                await Task.Delay(TimeSpan.FromSeconds(RetryIntervalSeconds));
        }

        throw new Exception(
            $"SignalR接続に失敗しました。{MaxRetryCount}回リトライしましたが応答がありませんでした。",
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

        _connection = new HubConnectionBuilder()
            .WithUrl(result.Url, options =>
            {
                options.AccessTokenProvider =
                    () => Task.FromResult(result.AccessToken)!;
            })
            .WithAutomaticReconnect()
            .Build();

        _connection.On<List<OrderCreatedMessage>>(
            "OrderCreated",
            messages => OrderCreated?.Invoke(messages));

        await _connection.StartAsync();
    }
}
