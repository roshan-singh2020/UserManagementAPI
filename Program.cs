var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
var app = builder.Build();

// 🔻 Add this block to handle unhandled exceptions globally
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/error");
}

// Minimal endpoint for error handling
app.Map("/error", (HttpContext context) =>
{
    // Optional: log the error, inspect context.Features, etc.
    return Results.Problem("An unexpected error occurred. Please try again later hai.");
});

// 🔹 1st: Error Handling Middleware (Catches all unhandled errors)
app.UseMiddleware<ErrorHandlingMiddleware>();

// 🔹 2nd: Authentication Middleware (Blocks unauthorized requests)
app.UseMiddleware<AuthenticationMiddleware>();

// 🔹 3rd: Logging Middleware (Logs all requests and responses)
app.UseMiddleware<LoggingMiddleware>();

app.UseRouting();
app.UseAuthorization();
app.MapControllers();
app.Run();