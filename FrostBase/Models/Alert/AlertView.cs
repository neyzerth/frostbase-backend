public class AlertView : JsonResponse
{
    public Alert Alert { get; set; }
    
    public static AlertView GetResponse(Alert alert, int status = 0)
    {
        return new AlertView
        {
            Status = status,
            Alert = alert
        };
    }
}
