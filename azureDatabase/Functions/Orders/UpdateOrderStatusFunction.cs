using System.Net;
using System.Text.Json;

using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;

using azureDatabase.Services.Orders;

using shared.Models.Requests.Orders;

namespace azureDatabase.Functions.Orders;

public class UpdateOrderStatusFunction
{
    private readonly OrderService
        _orderService;

    public UpdateOrderStatusFunction(
        OrderService orderService)
    {
        _orderService = orderService;
    }

    [Function("UpdateOrderStatus")]
    public async Task<HttpResponseData>
        Run(
            [HttpTrigger(
                AuthorizationLevel.Function,
                "put",
                Route = "orders/status")]
            HttpRequestData req)
    {
        try
        {
            var request = await JsonSerializer.DeserializeAsync<UpdateOrderStatusRequest>(req.Body, new JsonSerializerOptions {PropertyNameCaseInsensitive = true});
            if (request is null)
            {
                var badRequest =
                    req.CreateResponse(
                        HttpStatusCode.BadRequest);

                await badRequest
                    .WriteStringAsync(
                        "Request body is null.");

                return badRequest;
            }

            await _orderService
                .UpdateOrderStatusAsync(
                    request.ShopID,
                    request.OrderID,
                    request.Status);

            var response =
                req.CreateResponse(
                    HttpStatusCode.OK);

            await response
                .WriteStringAsync("OK");

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