public class ReadingView : JsonResponse
{
    public Reading Reading { get; set; }
    
    public static ReadingView GetResponse(Reading reading, int status = 0)
    {
        return new ReadingView
        {
            Status = status,
            Reading = reading
        };
    }
}
