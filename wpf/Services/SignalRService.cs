using Microsoft.AspNetCore.SignalR.Client;

using shared.Models.SignalR;

using wpf.Config;
using System.Net.Http;
using System.Net.Http.Json;
namespace wpf.Services;

public class SignalRService
{
    private HubConnection?
        _connection;

    public event Action<
        List<OrderCreatedMessage>>?
        OrderCreated;

    public async Task StartAsync()
    {
        using var http =
            new HttpClient();

        var response =
            await http.PostAsync(
                ApiEndpoints.SignalRNegotiate,
                null);

        var result =
            await response.Content
                .ReadFromJsonAsync<
                    SignalRConnectionResponse>();

        if (result is null)
        {
            return;
        }

        _connection =
            new HubConnectionBuilder()
                .WithUrl(
                    result.Url,
                    options =>
                    {
                        options
                            .AccessTokenProvider =
                            () =>
                                Task.FromResult(
                                    result
                                        .AccessToken)!;
                    })
                .WithAutomaticReconnect()
                .Build();

        _connection.On<
            List<OrderCreatedMessage>>(
            "OrderCreated",
            messages =>
            {
                OrderCreated?.Invoke(
                    messages);
            });

        await _connection.StartAsync();
    }
}