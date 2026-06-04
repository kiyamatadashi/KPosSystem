namespace azureDatabase.Models.Entities.Masters;

public class SystemMasterEntity
{
    public string ShopID { get; set; } = string.Empty;
    public string SystemType { get; set; } = string.Empty;
    public string SystemTypeName { get; set; } = string.Empty;
    public int Amount { get; set; }
    public bool? Deleted { get; set; }
}
