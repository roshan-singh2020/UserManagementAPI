public class AuthenticationMiddleware
{
    private readonly RequestDelegate _next;

    public AuthenticationMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
        
        if (string.IsNullOrEmpty(token) || !ValidateToken(token))
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsync("{ \"error\": \"Unauthorized access.\" }");
            return;
        }

        await _next(context);
    }

    private bool ValidateToken(string token)
    {
        // Mock token validation - Replace with real validation logic
        return token == "valid_token";
    }
}