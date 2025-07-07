public class OrderDto
{
    public string Id { get; set; }
    public DateOnly Date { get; set; }
    public DateTime DeliveryTime { get; set; }
    public UserDto CreatedBy { get; set; }
    public StoreDto Store { get; set; }
}