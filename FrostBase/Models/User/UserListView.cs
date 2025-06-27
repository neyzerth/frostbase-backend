public class UserListView : JsonResponse
{
    public List<UserApp> Users { get; set; }

    public static UserListView GetResponse(List<UserApp> users, int status)
    {
        return new UserListView
        {
            Status = status,
            Users = users
        };
    }
}