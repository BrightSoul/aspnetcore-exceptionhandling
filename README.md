# aspnetcore-exceptionhandling
Simple demo of a custom middleware raising exceptions and global exception handling in ASP.NET Core.

Basically, just skip the custom middleware execution if the `IExceptionHandlerPathFeature` is present. This way, it won't raise the exception again when then `ExceptionHandlingMiddleware` re-executes the pipeline.

```csharp
public class MyMiddleware
{
    private readonly RequestDelegate next;
    public MyMiddleware(RequestDelegate next)
    {
        this.next = next;
    }
    public async Task InvokeAsync(HttpContext context)
    {
        // Call the next delegate/middleware in the pipeline
        if (ShouldAttemptDatabaseConnection(context))
        {
            try
            {
                // Attempt connection to the database
                // It fails...
                throw new Exception("Error");
            }
            catch (Exception exc)
            {
                // Re-throw a custom exception
                throw new MyDatabaseException("Problem communicating witht the database", exc);
            }
        }
        await next(context);
    }
    private bool ShouldAttemptDatabaseConnection(HttpContext context)
    {
        var feature = context.Features.Get<IExceptionHandlerPathFeature>();
        return feature == null;
    }
}
```