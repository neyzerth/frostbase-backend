public class MessageResponse : JsonResponse
{
    public object Data { get; set; }
    public string Type { get; set; }  

    public static MessageResponse GetResponse(int status, object message, MessageType type)
    {
        MessageResponse r = new MessageResponse();
        r.Status = status;
        r.Data = message;
        r.Type = type.ToString();
        return r;
    }
}