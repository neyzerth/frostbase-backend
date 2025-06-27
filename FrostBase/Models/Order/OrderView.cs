public class OrderView : JsonResponse
{
    public Order Order { get; set; }
    
    public static OrderView GetResponse(Order order, int status = 0)
    {
        return new OrderView
        {
            Status = status,
            Order = order
        };
    }
}
