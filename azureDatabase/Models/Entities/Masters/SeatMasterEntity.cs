namespace azureDatabase.Models.Entities.Masters;

public class SeatMasterEntity
{
    public string ShopID { get; set; } = string.Empty;
    public string SeatName { get; set; } = string.Empty;
    public int ServiceCharge { get; set; }
    public int? SeatCharge { get; set; }
    public bool? Deleted { get; set; }
}
