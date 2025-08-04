public class UpdateUserDto
{
    public string Id { get; set; }
    public Name Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public DateOnly? BirthDate { get; set; }
    public string IDTruckDefault { get; set; }
    public bool Active { get; set; } //agregado
}