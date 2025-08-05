public class AdminNotFoundException : FrostbaseException
{
    public AdminNotFoundException() : base()
    {
        _message = "Admins not found";
        Type = nameof(MessageType.Warning);
        HttpStatus = StatusCodes.Status404NotFound;
    }
    public AdminNotFoundException(string userId) : base()
    {
        _message = "Admins not found with id "+ userId;
        Type = nameof(MessageType.Warning);
        HttpStatus = StatusCodes.Status404NotFound;
    }
}