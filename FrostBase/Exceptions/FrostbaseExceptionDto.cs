public class FrostbaseExceptionDto
{
    public string Message { get; set; }
    public string Type { get; set; }
    public int Status { get; set; }

    public FrostbaseExceptionDto(FrostbaseException ex)
    {
        Message = ex.Message;
        Type = ex.Type;
        Status = ex.Status;
    }

    public FrostbaseExceptionDto(Exception ex)
    {
        Message = "Error ocurred: "+ex.Message;
        Type = MessageType.Error.ToString();
        Status = 1;
    }
}