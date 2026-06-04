using System.Net;
using System.Text.Json;

using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;

using azureDatabase.Services.Groups;

namespace azureDatabase.Functions.Groups;

public class CloseGroupFunction
{
    private readonly GroupService _groupService;

    public CloseGroupFunction(GroupService groupService)
    {
        _groupService = groupService;
    }

    [Function("CloseGroup")]
    public async Task<HttpResponseData> Run(
        [HttpTrigger(
            AuthorizationLevel.Function,
            "put",
            Route = "groups/close")]
        HttpRequestData req,
        CancellationToken cancellationToken)
    {
        try
        {
            var body = await JsonSerializer.DeserializeAsync<CloseGroupRequest>(
                req.Body,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            if (body is null)
            {
                var badRequest = req.CreateResponse(HttpStatusCode.BadRequest);
                await badRequest.WriteStringAsync("Request body is null.");
                return badRequest;
            }

            await _groupService.DeleteGroupAsync(body.ShopID, body.GroupID, cancellationToken);

            var response = req.CreateResponse(HttpStatusCode.OK);
            await response.WriteStringAsync("OK");
            return response;
        }
        catch (Exception ex)
        {
            var errorResponse = req.CreateResponse(HttpStatusCode.InternalServerError);
            await errorResponse.WriteAsJsonAsync(new { Message = ex.Message });
            return errorResponse;
        }
    }

    // CloseGroup専用のリクエスト型（このFunctionのみで使用）
    private record CloseGroupRequest(string ShopID, string GroupID);
}
