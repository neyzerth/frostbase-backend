public class OrderDto
{
    public string Id { get; set; }
    public DateTime Date { get; set; }
    public DateOnly? DeliverDate { get; set; }
    public AdminDto CreatedBy { get; set; }
    public StoreDto Store { get; set; }
    public StateDto State { get; set; }
    
    public static OrderDto FromModel(Order o)
    {
        return new OrderDto
        {
            Id = o.Id,
            Date = o.Date,
            DeliverDate = DateOnly.FromDateTime(o.DeliverDate),
            CreatedBy = AdminDto.FromModel(UserApp.Get(o.IDUser)),
            Store = StoreDto.FromModel(global::Store.Get(o.IDStore)),
            State = StateDto.FromModel(StateOrder.Get(o.IDStateOrder))
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