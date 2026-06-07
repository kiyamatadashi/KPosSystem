namespace azureDatabase.Models.Entities.Orders;

public class OrderEntity
{
    public string ShopID { get; set; } = string.Empty;
    public string GroupID { get; set; } = string.Empty;
    public string SetNumber { get; set; } = string.Empty;
    public DateTime OrderDateTime { get; set; }
    public long OrderID { get; set; }
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
    /// キャスト名JSON文字列。例: ["さくら","あおい"]
    /// DB定義: CastName NVARCHAR(128) NULL
    /// </summary>
    public string? CastName { get; set; }

    public bool? Status { get; set; }
    public DateTime CreatedAt { get; set; }
}
