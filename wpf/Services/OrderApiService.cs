using shared.Models.Requests.Orders;
using shared.Models.Responses.Orders;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using wpf.Config;

namespace wpf.Services;

public class OrderApiService
{
    // コールドスタートは30〜60秒かかるため90秒に設定
    private readonly HttpClient _client = new()
    {
        Timeout = TimeSpan.FromSeconds(90)
    };

    // リトライ設定
    private const int MaxRetryCount = 3;
    private const int RetryIntervalSeconds = 2;

    public async Task<List<OrderResponse>?> GetOrdersAsync()
    {
        return await ExecuteWithRetryAsync(async () =>
            await _client.GetFromJsonAsync<List<OrderResponse>>(ApiEndpoints.Orders)
        );
    }

    public async Task CreateOrderAsync(List<CreateOrderRequest> orders)
    {
        await ExecuteWithRetryAsync(async () =>
        {
            var json = JsonSerializer.Serialize(orders);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(ApiEndpoints.Orders, content);
            response.EnsureSuccessStatusCode();
            return response;
        });
    }

    /// <summary>
    /// コールドスタート対策のリトライ処理。
    /// タイムアウト・接続失敗の場合は RetryIntervalSeconds 待機後にリトライする。
    /// </summary>
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
            catch (TaskCanceledException ex)
            {
                // タイムアウト（コールドスタート）
                lastException = ex;
            }
            catch (HttpRequestException ex)
            {
                // 接続失敗
                lastException = ex;
            }

            if (attempt < MaxRetryCount)
            {
                await Task.Delay(
                    TimeSpan.FromSeconds(RetryIntervalSeconds),
                    cancellationToken);
            }
        }

        throw new Exception(
            $"API接続に失敗しました。{MaxRetryCount}回リトライしましたが応答がありませんでした。",
            lastException);
    }

    /// <summary>
    /// 戻り値なしのアクション用オーバーロード。
    /// </summary>
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
