namespace FrostBase.Models.User;

public class UserListView : JsonResponse
{
    public List<User> Users { get; set; }

    public static UserListView GetResponse(List<User> users, int status)
    {
        return new UserListView
        {
            Status = status,
            Users = users
        };
    }
}