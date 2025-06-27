public class TripView : JsonResponse
{
    public Trip Trip { get; set; }
    
    public static TripView GetResponse(Trip trip, int status = 0)
    {
        return new TripView
        {
            Status = status,
            Trip = trip
        };
    }
}
