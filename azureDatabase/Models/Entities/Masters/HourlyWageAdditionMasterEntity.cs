namespace azureDatabase.Models.Entities.Masters;

public class HourlyWageAdditionMasterEntity
{
    public string ShopID { get; set; } = string.Empty;
    public string ItemName { get; set; } = string.Empty;
    public int Amount { get; set; }
    public bool? Deleted { get; set; }
}
