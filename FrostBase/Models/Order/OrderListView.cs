public class OrderListView : JsonResponse
{
    public List<Order> Orders { get; set; }

    public static OrderListView GetResponse(List<Order> orders, int status)
    {
        return new OrderListView
        {
            Status = status,
            Orders = orders
        };
    }
}
