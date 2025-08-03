public class NoOrdersForRouteException : FrostbaseException
{
    public NoOrdersForRouteException(string routeId) : base() 
    {
        _message = $"No orders for route {routeId}";
        Type = nameof(MessageType.Warning);
        HttpStatus = StatusCodes.Status404NotFound;
    }
}