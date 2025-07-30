using MongoDB.Bson.Serialization.Attributes;

public class CreateUserDto
{
    public string Name { get; set; }
    public string LastName { get; set; }
    public string MiddleName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public DateTime BirthDate { get; set; }
    public string Password { get; set; }
    public string IDTruckDefault { get; set; }
    public bool IsAdmin { get; set; }
}