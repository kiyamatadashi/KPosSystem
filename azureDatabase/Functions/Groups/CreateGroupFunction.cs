using System.Net;

using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;

using azureDatabase.Models.Entities.Groups;
using azureDatabase.Services.Groups;

namespace azureDatabase.Functions.Groups;

public class CreateGroupFunction
{
    private readonly GroupService _groupService;

    public CreateGroupFunction(GroupService groupService)
    {
        _groupService = groupService;
    }

    [Function("CreateGroup")]
    public async Task<HttpResponseData> Run(
        [HttpTrigger(
            AuthorizationLevel.Function,
            "post",
            Route = "groups")]
        HttpRequestData req,
        CancellationToken cancellationToken)
    {
        try
        {
            var group = await req.ReadFromJsonAsync<GroupEntity>(cancellationToken);

            if (group is null)
            {
                var badRequest = req.CreateResponse(HttpStatusCode.BadRequest);
                await badRequest.WriteStringAsync("Request body is null.");
                return badRequest;
            }

            await _groupService.CreateGroupAsync(group, cancellationToken);

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
}
