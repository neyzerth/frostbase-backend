public class DriverDto
{
    public string Id { get; set; }
    public Name Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public DateOnly? BirthDate { get; set; }
    public TruckDto TruckDefault { get; set; }

    public static DriverDto FromModel(UserApp u)
    {
        TruckDto truck = null;
        if(!string.IsNullOrEmpty(u.IDTruckDefault))
            truck = TruckDto.FromModel(Truck.Get(u.IDTruckDefault));
        
        return new DriverDto
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

    public static List<DriverDto> FromModel(List<UserApp> drivers)
    {
        List<DriverDto> driversDto = new List<DriverDto>();
        foreach (UserApp u in drivers)
        {
            driversDto.Add(FromModel(u));
        }
        return driversDto;   
    }
}