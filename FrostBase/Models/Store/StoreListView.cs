public class StoreListView : JsonResponse
{
    public List<Store> Stores { get; set; }

    public static StoreListView GetResponse(List<Store> stores, int status)
    {
        return new StoreListView
        {
            Status = status,
            Stores = stores
        };
    }
}
