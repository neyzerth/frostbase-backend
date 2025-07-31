public class FrostbaseException : Exception
{
    public string _message;
    public string Type;
    public int Status;
    public int HttpStatus { get; set; }
    public override string Message => _message;

    public FrostbaseException(
        string message = "Error", 
        int status = 1, 
        int httpStatus = 500,
        MessageType type = MessageType.Error
        )
    {
        _message = message;
        Status = status;
        Type = type.ToString();
        HttpStatus = httpStatus;
    }
}