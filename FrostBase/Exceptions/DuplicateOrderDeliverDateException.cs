public class DuplicateOrderDeliverDateException : FrostbaseException
{
    public DuplicateOrderDeliverDateException(Order order) : base() 
    {
        _message = $"Store {order.IDStore} has already an order with the same deliver date {order.DeliverDate}";
        Type = nameof(MessageType.Warning);
        HttpStatus = StatusCodes.Status404NotFound;
    }
}