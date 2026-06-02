using System.Net;

using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;

using azureDatabase.Services.Orders;

namespace azureDatabase.Functions.Orders;

public class GetOrdersFunction
{
    private readonly OrderService _orderService;

    public GetOrdersFunction(
        OrderService orderService)
    {
        _orderService = orderService;
    }

    [Function("GetOrders")]
    public async Task<HttpResponseData> Run(
        [HttpTrigger(
            AuthorizationLevel.Function,
            "get",
            Route = "orders")]
        HttpRequestData req)
    {
        try
        {
            var orders =
                await _orderService.GetOrdersAsync();

            var response =
                req.CreateResponse(
                    HttpStatusCode.OK);

            await response.WriteAsJsonAsync(
                orders);

            return response;
        }
        catch (Exception ex)
        {
            var errorResponse =
                req.CreateResponse(
                    HttpStatusCode
                        .InternalServerError);

            await errorResponse
                .WriteAsJsonAsync(new
                {
                    Message = ex.Message
                });

            return errorResponse;
        }
    }
}