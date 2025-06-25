public class MessageResponse : JsonResponse
{
    public object Message { get; set; }
    public string Type { get; set; }  

    public static MessageResponse GetResponse(int status, object message, MessageType type)
    {
        MessageResponse r = new MessageResponse();
        r.Status = status;
        r.Message = message;
        r.Type = type.ToString();
        return r;
    }
}