namespace azureDatabase.Models.Entities.Groups;

public class PayrollAdjustmentEntity
{
    public string ShopID { get; set; } = string.Empty;
    public string CastName { get; set; } = string.Empty;
    public string ClosingYearMonth { get; set; } = string.Empty;
    public int Amount { get; set; }
    public bool? Deleted { get; set; }
}
