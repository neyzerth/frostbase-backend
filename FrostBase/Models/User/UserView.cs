namespace FrostBase.Models.User;

public class UserView : JsonResponse
{
    public User User { get; set; }
    
    public static UserView GetResponse(User user, int status = 0)
    {
        return new UserView
        {
            Status = status,
            User = user
        };
    }
}