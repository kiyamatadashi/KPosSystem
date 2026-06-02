namespace shared.Models.Requests.Orders;

public class UpdateOrderStatusRequest
{
    public string ShopID { get; set; } = string.Empty;
    public long OrderID { get; set; }
    public bool Status { get; set; }
}