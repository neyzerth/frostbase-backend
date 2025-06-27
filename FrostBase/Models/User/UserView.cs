public class UserView : JsonResponse
{
    public UserApp UserApp { get; set; }
    
    public static UserView GetResponse(UserApp userApp, int status = 0)
    {
        return new UserView
        {
            Status = status,
            UserApp = userApp
        };
    }
}