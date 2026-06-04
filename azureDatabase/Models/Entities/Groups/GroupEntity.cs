namespace azureDatabase.Models.Entities.Groups;

public class GroupEntity
{
    public string ShopID { get; set; } = string.Empty;
    public string GroupID { get; set; } = string.Empty;
    public int GuestCount { get; set; }
    public string? Companion { get; set; }
    public string? Nomination { get; set; }
    public string? CatchStaff { get; set; }
    public bool? Deleted { get; set; }
}
