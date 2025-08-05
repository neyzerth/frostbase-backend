public class StoresWithNoOrdersNotFoundException : FrostbaseException
{
    public StoresWithNoOrdersNotFoundException() : base()
    {
        _message = "Stores with no orders not founds";
        Type = nameof(MessageType.Warning);
        HttpStatus = StatusCodes.Status404NotFound;
    }
    public StoresWithNoOrdersNotFoundException(DateTime date) : base()
    {
        _message = "Stores with no orders not founds for " + date.Date;
        Type = nameof(MessageType.Warning);
        HttpStatus = StatusCodes.Status404NotFound;
    }
}