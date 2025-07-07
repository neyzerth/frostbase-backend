public class OrderDto
{
    public string Id { get; set; }
    public DateOnly Date { get; set; }
    public DateTime? DeliveryTime { get; set; }
    public UserDto CreatedBy { get; set; }
    public StoreDto Store { get; set; }

    public static OrderDto FromModel(Order o)
    {
        return new OrderDto
        {
            Id = o.Id,
            Date = DateOnly.FromDateTime(o.Date),
            CreatedBy = UserDto.FromModel(UserApp.Get(o.IDUser)),
            Store = StoreDto.FromModel(global::Store.Get(o.IDStore))
        };
    }

    public static List<OrderDto> FromModel(List<Order> orders)
    {
        List<OrderDto> ordersDto = new List<OrderDto>();
        foreach (Order o in orders)
        {
            ordersDto.Add(FromModel(o));
        }
        return ordersDto;
    }
}