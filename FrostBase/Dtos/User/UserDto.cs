
public class UserDto
{
    public string Id { get; set; }
    public Name Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public DateOnly? BirthDate { get; set; }
    public TruckDto? TruckDefault { get; set; }

    public static UserDto FromModel(UserApp u)
    {
        TruckDto truck = null;
        if(string.IsNullOrEmpty(u.IDTruckDefault))
            truck = TruckDto.FromModel(Truck.Get(u.IDTruckDefault));
        
        return new UserDto
        {
            Id = u.Id,
            Name = new Name
            {
                FirstName = u.FirstName,
                LastName = u.LastName,
                MiddleName = u.MiddleName
            },
            Email = u.Email,
            Phone = u.Phone,
            TruckDefault = truck,
            BirthDate = DateOnly.FromDateTime(u.BirthDate)
        };
    }

    public static List<UserDto> FromModel(List<UserApp> users)
    {
        List<UserDto> usersDto = new List<UserDto>();
        foreach (UserApp u in users)
        {
            usersDto.Add(FromModel(u));
        }
        return usersDto;   
    }
}

public class Name
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string MiddleName { get; set; }
    public string FullName => $"{FirstName} {LastName} {MiddleName}";
}