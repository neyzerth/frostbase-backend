public class TruckListView : JsonResponse
{
    public List<Truck> Trucks { get; set; }

    public static TruckListView GetResponse(List<Truck> trucks, int status)
    {
        return new TruckListView
        {
            Status = status,
            Trucks = trucks
        };
    }
}