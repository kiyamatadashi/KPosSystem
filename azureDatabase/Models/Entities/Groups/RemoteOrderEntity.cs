namespace azureDatabase.Models.Entities.Groups;

public class RemoteOrderEntity
{
    public string ShopID { get; set; } = string.Empty;
    public DateTime ReceptionDate { get; set; }
    public string Category { get; set; } = string.Empty;
    public string SideMenu { get; set; } = string.Empty;
    public string ProductName { get; set; } = string.Empty;
    public string YearMonth { get; set; } = string.Empty;
    public int Amount { get; set; }
    public int Quantity { get; set; }
    public int BackAmount { get; set; }
    public string BackUnit { get; set; } = string.Empty;
    public string CastName { get; set; } = string.Empty;
    public bool? Deleted { get; set; }
}
