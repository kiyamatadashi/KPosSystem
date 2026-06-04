namespace azureDatabase.Models.Entities.Masters;

public class TimeMasterEntity
{
    public string ShopID { get; set; } = string.Empty;
    public string TimeType { get; set; } = string.Empty;
    public int TimeValue { get; set; }
    public bool? Deleted { get; set; }
}
