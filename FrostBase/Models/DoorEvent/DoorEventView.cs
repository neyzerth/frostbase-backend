public class DoorEventView : JsonResponse
{
    public DoorEvent DoorEvent { get; set; }
    
    public static DoorEventView GetResponse(DoorEvent doorEvent, int status = 0)
    {
        return new DoorEventView
        {
            Status = status,
            DoorEvent = doorEvent
        };
    }
}
