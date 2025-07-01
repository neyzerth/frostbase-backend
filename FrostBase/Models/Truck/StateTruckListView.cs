namespace FrostBase.Models.Truck;

public class StateTruckListView : JsonResponse
{
    public List<StateTruck> StateTrucks { get; set; }

    public static StateTruckListView GetResponse(List<StateTruck> stateTrucks, int status)
    {
        return new StateTruckListView
        {
            Status = status,
            StateTrucks = stateTrucks
        };
    }
}