namespace azureDatabase.Models.Entities.Masters;

public class CashInOutMasterEntity
{
    public string ShopID { get; set; } = string.Empty;
    public string ItemName { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public string CashInOutType { get; set; } = string.Empty;
    public bool? Deleted { get; set; }
}
