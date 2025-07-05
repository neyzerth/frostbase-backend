using MongoDB.Bson.Serialization.Attributes;
public class CreateRouteDto
{   
    [BsonId]
    public int Id { get; set; }
    [BsonElement("name")]
    public string Name { get; set; }
    [BsonElement("IDUser")]
    public int IDUser { get; set; }
}