using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Extensions.SignalRService;
using Microsoft.Azure.Functions.Worker.Http;
using azureDatabase.Services.Orders;
using shared.Models.Requests.Orders;
using shared.Models.SignalR;

namespace azureDatabase.Functions.Orders;

public class CreateOrderFunction
{
    private readonly OrderService _orderService;
    public CreateOrderFunction(OrderService orderService) {
        _orderService = orderService;
    }
    [Function("CreateOrder")]
    [SignalROutput(HubName = "poshub", ConnectionStringSetting = "AzureSignalRConnectionString")]
    public async Task<SignalRMessageAction> Run([HttpTrigger(AuthorizationLevel.Function, "post", Route = "orders")]HttpRequestData req, CancellationToken cancellationToken) {
        var requests = await req.ReadFromJsonAsync<List<CreateOrderRequest>>(cancellationToken);
        if (requests is null) {
            throw new Exception("Request is null.");
        }
        var signalRMessages = new List<OrderCreatedMessage>();
        foreach (var request in requests) {
            var orderId = await _orderService.CreateOrderAsync(request, cancellationToken);
            signalRMessages.Add(new OrderCreatedMessage {
                ShopID = request.ShopID,
                OrderID = orderId,
                GroupID = request.GroupID,
                SetNumber = request.SetNumber,
                OrderDateTime = DateTime.UtcNow,
                Category = request.Category,
                SideMenu = request.SideMenu,
                ProductName = request.ProductName,
                Amount = request.Amount,
                Quantity = request.Quantity,
                BackAmount = request.BackAmount,
                BackUnit = request.BackUnit,
                MixerSelectable = request.MixerSelectable,
                CastSelectable = request.CastSelectable,
                Status = false
            });
        }
        return new SignalRMessageAction("OrderCreated") {
            Arguments = new object[] {signalRMessages}
        };
    }
}