public class ListResponse<T> : MessageResponse
{
    public List<T> Data { get; set; }
    
    public static ListResponse<T> GetResponse(List<T> data, int status = 0, MessageType type = MessageType.Success)
    {
        return new ListResponse<T>()
        {
            Status = status,
            Data = data,
            Type = type.ToString()
        };
    }
}