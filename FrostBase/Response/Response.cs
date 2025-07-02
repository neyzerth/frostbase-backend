public class Response<T> : MessageResponse
{
    public T Data { get; set; }
    
    public static Response<T> GetResponse(T data, int status = 0)
    {
        return new Response<T>()
        {
            Status = status,
            Data = data
        };
    }
}