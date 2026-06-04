namespace azureDatabase.Models.Entities.Groups;

public class CashInOutEntity
{
    public string ShopID { get; set; } = string.Empty;
    public DateTime TransactionDate { get; set; }
    public string Category { get; set; } = string.Empty;
    public string ItemName { get; set; } = string.Empty;
    public string CashInOutType { get; set; } = string.Empty;
    public int Amount { get; set; }
    public string? Memo { get; set; }
    public bool? Deleted { get; set; }
}
