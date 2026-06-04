namespace azureDatabase.Models.Entities.Groups;

public class TrainingRemainingTimeEntity
{
    public string ShopID { get; set; } = string.Empty;
    public string ClosingYearMonth { get; set; } = string.Empty;
    public string CastName { get; set; } = string.Empty;
    public int RemainingTrainingHours { get; set; }
    public string WorkType { get; set; } = string.Empty;
    public int SalaryType { get; set; }
    public int Amount { get; set; }
    public bool? Deleted { get; set; }
}
