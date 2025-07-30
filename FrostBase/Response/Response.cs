public class Response<T> : MessageResponse
{
    public T Data { get; set; }
    
    public static Response<T> GetResponse(T data, int status = 0, MessageType type = MessageType.Success)
    {
        return new Response<T>()
        {
            Status = status,
            Data = data,
            Type = type.ToString()
        };
    }
}