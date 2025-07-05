using MongoDB.Bson.Serialization.Attributes;

public class CreateUserDto
{
    [BsonId]
    public int Id { get; set; }
    [BsonElement("name")]
    public string Name { get; set; }
    [BsonElement("last_name")]
    public string LastName { get; set; }
    [BsonElement("middle_name")]
    public string MiddleName { get; set; }
    [BsonElement("email")]
    public string Email { get; set; }
    [BsonElement("phone")]
    public string Phone { get; set; }
    [BsonElement("birth_date")]
    public DateOnly BirthDate { get; set; }
    [BsonElement("password")]
    public string Password { get; set; }
    [BsonElement("IDTruck")]
    public string IDTruck { get; set; }
}