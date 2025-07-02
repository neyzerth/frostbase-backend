public class TruckView : JsonResponse
{
    public Truck Truck { get; set; }
    
    public static TruckView GetResponse(Truck truck, int status = 0)
    {
        return new TruckView
        {
            Status = status,
            Truck = truck
        };
    }
}