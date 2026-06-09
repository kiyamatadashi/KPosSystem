namespace shared.Models.Requests.Masters;

/// <summary>
/// ProductMaster 登録・更新リクエスト（1件分）。
/// 複数件をまとめて POST する場合は List&lt;UpsertProductMasterRequest&gt; として送信する。
/// </summary>
public class UpsertProductMasterRequest
{
    public string ShopID        { get; set; } = string.Empty;
    public string Category      { get; set; } = string.Empty;
    public string SideMenu      { get; set; } = string.Empty;
    public string ProductName   { get; set; } = string.Empty;
    public int    Amount        { get; set; }
    public int    BackAmount    { get; set; }
    public string BackUnit      { get; set; } = string.Empty;
    public bool   MixSelectable  { get; set; }
    public bool   CastSelectable { get; set; }
    public bool?  Deleted       { get; set; }
}
