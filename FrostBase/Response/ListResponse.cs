public class ListResponse<T> : MessageResponse
{
    public List<T> Data { get; set; }
    
    public static ListResponse<T> GetResponse(List<T> data, int status = 0)
    {
        return new ListResponse<T>()
        {
            Status = status,
            Data = data
        };
    }
}