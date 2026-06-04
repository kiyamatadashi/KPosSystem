namespace azureDatabase.Models.Entities.Masters;

public class BonusMasterEntity
{
    public string ShopID { get; set; } = string.Empty;
    public string BonusName { get; set; } = string.Empty;
    public int Threshold { get; set; }
    public int FirstPlace { get; set; }
    public int SecondPlace { get; set; }
    public int ThirdPlace { get; set; }
    public bool? Deleted { get; set; }
}
