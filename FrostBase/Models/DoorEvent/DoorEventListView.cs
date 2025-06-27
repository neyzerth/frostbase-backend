public class DoorEventListView : JsonResponse
{
    public List<DoorEvent> DoorEvents { get; set; }

    public static DoorEventListView GetResponse(List<DoorEvent> doorEvents, int status)
    {
        return new DoorEventListView
        {
            Status = status,
            DoorEvents = doorEvents
        };
    }
}
