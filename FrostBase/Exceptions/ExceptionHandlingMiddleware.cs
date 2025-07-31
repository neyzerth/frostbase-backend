public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    public ExceptionHandlingMiddleware(RequestDelegate next) => _next = next;

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (FrostbaseException ex)
        {
            Console.WriteLine("Frostbase error: "+ex);
            context.Response.StatusCode = ex.HttpStatus;
            await context.Response.WriteAsJsonAsync(new FrostbaseExceptionDto(ex));
        }
        catch (Exception ex)
        {
            Console.WriteLine("ERROR: "+ex);
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await context.Response.WriteAsJsonAsync(new FrostbaseExceptionDto(ex));
        }
    }
}