public class UpdateStoreDto
{
    public string Id { get; set; }
    
    public string Name { get; set; }

    public string Phone { get; set; }

    public StoreLocation Location { get; set; }
    
    public bool Active { get; set; }
}