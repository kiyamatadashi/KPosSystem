using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

using shared.Models.Requests.Orders;
using shared.Models.Responses.Orders;

using orderWebSystem.Config;

namespace orderWebSystem.Services;

public class OrderApiService
{
    private readonly HttpClient    _client;
    private readonly ApiEndpoints  _endpoints;

    private const int MaxRetryCount        = 3;
    private const int RetryIntervalSeconds = 2;

    public OrderApiService(HttpClient client, ApiEndpoints endpoints)
    {
        _client            = client;
        _client.Timeout    = TimeSpan.FromSeconds(90);
        _endpoints         = endpoints;
    }

    public async Task<List<OrderResponse>> GetOrdersAsync()
    {
        return await ExecuteWithRetryAsync(async () =>
            await _client.GetFromJsonAsync<List<OrderResponse>>(_endpoints.Orders)
            ?? new List<OrderResponse>()
        );
    }

    public async Task CreateOrdersAsync(List<CreateOrderRequest> orders)
    {
        await ExecuteWithRetryAsync(async () =>
        {
            var content  = new StringContent(
                JsonSerializer.Serialize(orders), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(_endpoints.Orders, content);
            response.EnsureSuccessStatusCode();
            return response;
        });
    }

    public async Task UpdateOrderStatusAsync(UpdateOrderStatusRequest request)
    {
        await ExecuteWithRetryAsync(async () =>
        {
            var content  = new StringContent(
                JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");
            var response = await _client.PutAsync(_endpoints.UpdateOrderStatus, content);
            response.EnsureSuccessStatusCode();
            return response;
        });
    }

    // ─── リトライ共通処理 ─────────────────────────────────────

    private async Task<T> ExecuteWithRetryAsync<T>(
        Func<Task<T>> action,
        CancellationToken cancellationToken = default)
    {
        Exception? lastException = null;

        for (int attempt = 1; attempt <= MaxRetryCount; attempt++)
        {
            try { return await action(); }
            catch (TaskCanceledException ex) { lastException = ex; }
            catch (HttpRequestException ex)  { lastException = ex; }

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
