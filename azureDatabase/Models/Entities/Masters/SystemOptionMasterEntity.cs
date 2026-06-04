namespace azureDatabase.Models.Entities.Masters;

public class SystemOptionMasterEntity
{
    public string ShopID { get; set; } = string.Empty;
    public string Option { get; set; } = string.Empty;
    public int? Amount { get; set; }
    public int? BackAmount { get; set; }
    public bool? Deleted { get; set; }
}
