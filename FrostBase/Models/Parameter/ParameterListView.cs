public class ParameterListView : JsonResponse
{
    public List<Parameter> Parameters { get; set; }

    public static ParameterListView GetResponse(List<Parameter> parameters, int status)
    {
        return new ParameterListView
        {
            Status = status,
            Parameters = parameters
        };
    }
}
