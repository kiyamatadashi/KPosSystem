namespace shared.Models.Responses.Masters;

/// <summary>
/// ProductMaster 取得レスポンス。
/// azureDatabase の ProductMasterEntity と同一構造。
/// </summary>
public class ProductMasterResponse
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
