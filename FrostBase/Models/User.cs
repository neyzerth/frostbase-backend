using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class User
{
    public string Id { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
}

