public class ParameterView : JsonResponse
{
    public Parameter Parameter { get; set; }
    
    public static ParameterView GetResponse(Parameter parameter, int status = 0)
    {
        return new ParameterView
        {
            Status = status,
            Parameter = parameter
        };
    }
}
