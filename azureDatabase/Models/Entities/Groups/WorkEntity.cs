namespace azureDatabase.Models.Entities.Groups;

public class WorkEntity
{
    public string ShopID { get; set; } = string.Empty;
    public DateTime BusinessDate { get; set; }
    public string CastName { get; set; } = string.Empty;
    public TimeSpan? StartDateTime { get; set; }
    public TimeSpan? EndDateTime { get; set; }
    public int? HourlyWageAddition { get; set; }
    public int? AdvancePayment { get; set; }
    public bool? Deleted { get; set; }
}
