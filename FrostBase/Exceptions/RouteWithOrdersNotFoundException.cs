public class RouteWithOrdersNotFoundException : FrostbaseException
{
    public RouteWithOrdersNotFoundException(DateTime date) : base() 
    {
        _message = $"No routes with orders for {date.Date}";
        Type = nameof(MessageType.Warning);
        HttpStatus = StatusCodes.Status404NotFound;
    }
}