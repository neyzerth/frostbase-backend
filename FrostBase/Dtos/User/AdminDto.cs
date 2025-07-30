public class AdminDto
{
    public string Id { get; set; }
    public Name Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public DateOnly? BirthDate { get; set; }

    public static AdminDto FromModel(UserApp u)
    {
        if (u == null) return new AdminDto();
        return new AdminDto
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
            BirthDate = DateOnly.FromDateTime(u.BirthDate)
        };
    }

    public static List<AdminDto> FromModel(List<UserApp> admins)
    {
        List<AdminDto> adminsDto = new List<AdminDto>();
        foreach (UserApp u in admins)
        {
            adminsDto.Add(FromModel(u));
        }
        return adminsDto;   
    }
}