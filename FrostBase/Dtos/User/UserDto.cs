
public class UserDto
{
    public string Id { get; set; }
    public Name Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Phone { get; set; }
    public DateOnly? BirthDate { get; set; }
    public string IDTruck { get; set; }


    public static UserDto FromModel(UserApp u)
    {
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
            Password = u.Password,
            Phone = u.Phone,
            BirthDate = DateOnly.FromDateTime(u.BirthDate),
            IDTruck = u.IDTruck
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
}