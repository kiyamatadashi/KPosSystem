using System.Net;
using System.Text.Json;

using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;

using azureDatabase.Models.Entities.Masters;
using azureDatabase.Services.Masters;

namespace azureDatabase.Functions.Masters;

/// <summary>
/// POST /masters/product
/// 商品マスタを一括Upsertする。
/// リクエストボディ: JSON配列（各要素は ProductMasterEntity と同一構造）
/// </summary>
public class UpsertProductMasterFunction
{
    private readonly MasterService _masterService;

    public UpsertProductMasterFunction(MasterService masterService)
    {
        _masterService = masterService;
    }

    [Function("UpsertProductMaster")]
    public async Task<HttpResponseData> Run(
        [HttpTrigger(
            AuthorizationLevel.Function,
            "post",
            Route = "masters/product")]
        HttpRequestData req,
        CancellationToken cancellationToken)
    {
        try
        {
            var body = await req.ReadAsStringAsync();
            if (string.IsNullOrWhiteSpace(body))
            {
                var bad = req.CreateResponse(HttpStatusCode.BadRequest);
                await bad.WriteStringAsync("リクエストボディが空です。");
                return bad;
            }

            var entities = JsonSerializer.Deserialize<List<ProductMasterEntity>>(
                body,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            if (entities is null || entities.Count == 0)
            {
                var bad = req.CreateResponse(HttpStatusCode.BadRequest);
                await bad.WriteStringAsync("登録対象データがありません。");
                return bad;
            }

            foreach (var entity in entities)
                await _masterService.UpsertProductMasterAsync(entity, cancellationToken);

            var response = req.CreateResponse(HttpStatusCode.OK);
            await response.WriteStringAsync("OK");
            return response;
        }
        catch (Exception ex)
        {
            var error = req.CreateResponse(HttpStatusCode.InternalServerError);
            await error.WriteAsJsonAsync(new { Message = ex.Message });
            return error;
        }
    }
}
