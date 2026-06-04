using System.Net;

using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;

using azureDatabase.Services.Masters;

namespace azureDatabase.Functions.Masters;

public class GetCastMasterFunction
{
    private readonly MasterService _masterService;

    public GetCastMasterFunction(MasterService masterService)
    {
        _masterService = masterService;
    }

    [Function("GetCastMaster")]
    public async Task<HttpResponseData> Run(
        [HttpTrigger(
            AuthorizationLevel.Function,
            "get",
            Route = "masters/cast")]
        HttpRequestData req,
        CancellationToken cancellationToken)
    {
        try
        {
            // クエリパラメータからShopIDを取得
            var query = System.Web.HttpUtility.ParseQueryString(req.Url.Query);
            var shopId = query["shopId"];

            if (string.IsNullOrEmpty(shopId))
            {
                var badRequest = req.CreateResponse(HttpStatusCode.BadRequest);
                await badRequest.WriteStringAsync("shopId is required.");
                return badRequest;
            }

            var casts = await _masterService.GetCastMastersAsync(shopId, cancellationToken);

            var response = req.CreateResponse(HttpStatusCode.OK);
            await response.WriteAsJsonAsync(casts);
            return response;
        }
        catch (Exception ex)
        {
            var errorResponse = req.CreateResponse(HttpStatusCode.InternalServerError);
            await errorResponse.WriteAsJsonAsync(new { Message = ex.Message });
            return errorResponse;
        }
    }
}
