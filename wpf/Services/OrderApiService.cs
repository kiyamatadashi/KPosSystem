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
    private readonly HttpClient
        _client = new();

    public async Task<List<OrderResponse>?>
        GetOrdersAsync()
    {
        return await _client
            .GetFromJsonAsync<
                List<OrderResponse>>(
                ApiEndpoints.Orders);
    }

    public async Task CreateOrderAsync(
        List<CreateOrderRequest> orders)
    {
        var json =
            JsonSerializer.Serialize(orders);

        var content =
            new StringContent(
                json,
                Encoding.UTF8,
                "application/json");

        var response =
            await _client.PostAsync(
                ApiEndpoints.Orders,
                content);

        response.EnsureSuccessStatusCode();
    }
}