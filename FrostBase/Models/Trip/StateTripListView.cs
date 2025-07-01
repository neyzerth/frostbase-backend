namespace FrostBase.Models.Trip;

public class StateTripListView : JsonResponse
{
    public List<StateTrip> StateTrips { get; set; }

    public static StateTripListView GetResponse(List<StateTrip> stateTrips, int status)
    {
        return new StateTripListView
        {
            Status = status,
            StateTrips = stateTrips
        };
    }
}