namespace azureDatabase.Models.Entities.Groups;

public class RequestEntity
{
    public string ShopID { get; set; } = string.Empty;
    public string GroupID { get; set; } = string.Empty;
    public string SetNumber { get; set; } = string.Empty;
    public string CastName { get; set; } = string.Empty;
    public int RequestCount { get; set; }
}
