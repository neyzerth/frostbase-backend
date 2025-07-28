public class MessageResponse : JsonResponse
{
    public object Data { get; set; }
    public MessageType Type { get; set; }  

    public static MessageResponse GetResponse(object data, int status = 0, MessageType type = MessageType.Success)
    {
        MessageResponse r = new MessageResponse();
        r.Status = status;
        r.Data = data;
        r.Type = type;
        return r;
    }
}