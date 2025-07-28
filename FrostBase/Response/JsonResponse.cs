public class JsonResponse
{
    public int Status { get; set; }

    public JsonResponse(int status = 0)
    {
        Status = status;
    }
}
