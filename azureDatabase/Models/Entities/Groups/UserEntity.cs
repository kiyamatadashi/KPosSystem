namespace azureDatabase.Models.Entities.Groups;

public class UserEntity
{
    public string ShopID { get; set; } = string.Empty;
    public string GroupID { get; set; } = string.Empty;
    public int UserID { get; set; }
    public string SetNumber { get; set; } = string.Empty;
    public string ChargeType { get; set; } = string.Empty;
    public string DrinkType { get; set; } = string.Empty;
    public string SeatName { get; set; } = string.Empty;
    public int ServiceCharge { get; set; }
    public int SetUnitTime { get; set; }
}
