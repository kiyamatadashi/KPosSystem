namespace shared.Models.Requests.Orders;

public class CreateOrderRequest
{
    public string ShopID { get; set; } = string.Empty;
    public string GroupID { get; set; } = string.Empty;
    public string SetNumber { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public string SideMenu { get; set; } = string.Empty;
    public string ProductName { get; set; } = string.Empty;
    public int Amount { get; set; }
    public int Quantity { get; set; }
    public int BackAmount { get; set; }
    public string BackUnit { get; set; } = string.Empty;
    public bool MixerSelectable { get; set; }
    public bool CastSelectable { get; set; }

    /// <summary>
    /// キャスト名リスト（JSON文字列で保持）。
    /// 例: ["さくら","あおい"] → DB保存時は JsonSerializer.Serialize で変換する。
    /// 最大5名・1名あたり最大10文字（NVARCHAR(128)に収まる範囲）。
    /// </summary>
    public List<string> CastNames { get; set; } = new();
}
