using System.Net;

using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Extensions.SignalRService;
using Microsoft.Azure.Functions.Worker.Http;

namespace azureDatabase.Functions.SignalR;

public class SignalRNegotiateFunction
{
    [Function("Negotiate")]
    public async Task<HttpResponseData> Run(
        [HttpTrigger(
            AuthorizationLevel.Anonymous,
            "post",
            Route = "signalr/negotiate")]
        HttpRequestData req,

        [SignalRConnectionInfoInput(
            HubName = "poshub",
            ConnectionStringSetting =
                "AzureSignalRConnectionString")]
        SignalRConnectionInfo connectionInfo)
    {
        var response =
            req.CreateResponse(
                HttpStatusCode.OK);

        await response.WriteAsJsonAsync(new
        {
            url = connectionInfo.Url,
            accessToken =
                connectionInfo.AccessToken
        });

        return response;
    }
}