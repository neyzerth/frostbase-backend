public class AlertListView : JsonResponse
{
    public List<Alert> Alerts { get; set; }

    public static AlertListView GetResponse(List<Alert> alerts, int status)
    {
        return new AlertListView
        {
            Status = status,
            Alerts = alerts
        };
    }
}
