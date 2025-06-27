public class TripListView : JsonResponse
{
    public List<Trip> Trips { get; set; }

    public static TripListView GetResponse(List<Trip> trips, int status)
    {
        return new TripListView
        {
            Status = status,
            Trips = trips
        };
    }
}
