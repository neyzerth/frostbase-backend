public class StoreView : JsonResponse
{
    public Store Store { get; set; }
    
    public static StoreView GetResponse(Store store, int status = 0)
    {
        return new StoreView
        {
            Status = status,
            Store = store
        };
    }
}
