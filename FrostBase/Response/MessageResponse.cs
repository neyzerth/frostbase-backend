public class MessageResponse : JsonResponse
{
    public object Data { get; set; }
    public string Type { get; set; }  

    public static MessageResponse GetResponse(object data, MessageType type, int status = 0)
    {
        MessageResponse r = new MessageResponse();
        r.Status = status;
        r.Data = data;
        r.Type = type.ToString();
        return r;
    }
}