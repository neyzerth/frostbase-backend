public class ReadingListView : JsonResponse
{
    public List<Reading> Readings { get; set; }

    public static ReadingListView GetResponse(List<Reading> readings, int status)
    {
        return new ReadingListView
        {
            Status = status,
            Readings = readings
        };
    }
}
