namespace shared.Models.Responses.Orders;

public class OrderResponse
{
    public string ShopID { get; set; } = string.Empty;
    public long OrderID { get; set; }
    public string GroupID { get; set; } = string.Empty;
    public string SetNumber { get; set; } = string.Empty;
    public DateTime OrderDateTime { get; set; }
    public string Category { get; set; } = string.Empty;
    public string SideMenu { get; set; } = string.Empty;
    public string ProductName { get; set; } = string.Empty;
    public int Amount { get; set; }
    public int Quantity { get; set; }
    public int BackAmount { get; set; }
    public string BackUnit { get; set; } = string.Empty;
    public bool MixerSelectable { get; set; }
    public bool CastSelectable { get; set; }
    public bool? Status { get; set; }
}