using System.Net;

using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;

using azureDatabase.Services.Groups;

namespace azureDatabase.Functions.Groups;

public class GetGroupsFunction
{
    private readonly GroupService _groupService;

    public GetGroupsFunction(GroupService groupService)
    {
        _groupService = groupService;
    }

    [Function("GetGroups")]
    public async Task<HttpResponseData> Run(
        [HttpTrigger(
            AuthorizationLevel.Function,
            "get",
            Route = "groups")]
        HttpRequestData req,
        CancellationToken cancellationToken)
    {
        try
        {
            var groups = await _groupService.GetGroupsAsync(cancellationToken);

            var response = req.CreateResponse(HttpStatusCode.OK);
            await response.WriteAsJsonAsync(groups);
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
