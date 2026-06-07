using shared.Models.Requests.Orders;
using shared.Models.Responses.Orders;

using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

using drinkOrderSystem.Config;

namespace drinkOrderSystem.Services;

public class OrderApiService
{
    // コールドスタートは30〜60秒かかるため90秒に設定
    private readonly HttpClient _client = new()
    {
        Timeout = TimeSpan.FromSeconds(90)
    };

    private const int MaxRetryCount     = 3;
    private const int RetryIntervalSeconds = 2;

    public async Task<List<OrderResponse>?> GetOrdersAsync()
    {
        return await ExecuteWithRetryAsync(async () =>
            await _client.GetFromJsonAsync<List<OrderResponse>>(ApiEndpoints.Orders)
        );
    }

    public async Task UpdateOrderStatusAsync(UpdateOrderStatusRequest request)
    {
        await ExecuteWithRetryAsync(async () =>
        {
            var json    = JsonSerializer.Serialize(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PutAsync(ApiEndpoints.UpdateOrderStatus, content);
            response.EnsureSuccessStatusCode();
            return response;
        });
    }

    private async Task<T?> ExecuteWithRetryAsync<T>(
        Func<Task<T?>> action,
        CancellationToken cancellationToken = default)
    {
        Exception? lastException = null;

        for (int attempt = 1; attempt <= MaxRetryCount; attempt++)
        {
            try
            {
                return await action();
            }
            catch (TaskCanceledException ex)  { lastException = ex; }
            catch (HttpRequestException ex)   { lastException = ex; }

            if (attempt < MaxRetryCount)
                await Task.Delay(TimeSpan.FromSeconds(RetryIntervalSeconds), cancellationToken);
        }

        throw new Exception(
            $"API接続に失敗しました。{MaxRetryCount}回リトライしましたが応答がありませんでした。",
            lastException);
    }

    private async Task ExecuteWithRetryAsync(
        Func<Task> action,
        CancellationToken cancellationToken = default)
    {
        await ExecuteWithRetryAsync<object?>(async () =>
        {
            await action();
            return null;
        }, cancellationToken);
    }
}
