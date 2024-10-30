using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;
using System.IO;
using System.Linq; // Include this for Linq

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddCors(options =>
{
    var allowedOrigins = new[]
    {
        "http://localhost:5173",
        "http://localhost:5173/",
        "http://localhost:5174",
        "http://localhost:5174/"
    };

    options.AddPolicy("CorsPolicy", builder => builder
        .WithOrigins(allowedOrigins)
        .AllowCredentials()
        .AllowAnyHeader()
        .AllowAnyMethod());
});

builder.Services.AddCookiePolicy(options =>
{
    options.MinimumSameSitePolicy = SameSiteMode.None;
});

// Build app
var app = builder.Build();

// Ensure directories exist
EnsureDirectoriesExist(new[]
{
    "public/Courses_Pictures",
    "public/Courses_Videos",
    "public/Payment",
    "public/ProfilePics",
    "public/Summaries",
    "public/Summaries_Pictures"
});

void EnsureDirectoriesExist(string[] directories)
{
    foreach (var dir in directories)
    {
        var path = Path.Combine(Directory.GetCurrentDirectory(), dir);
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
    }
}

// Serve static files
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "public")),
    RequestPath = "/public"
});

// Middleware: example of cookie handling and CORS
app.UseCors("CorsPolicy");
app.UseRouting();
app.UseCookiePolicy();

// Example middleware
app.Use(async (context, next) =>
{
    // Custom logic, e.g., authentication check
    var origin = context.Request.Headers["Origin"].ToString();
    var allowedOriginsList = new[]
    {
        "http://localhost:5173",
        "http://localhost:5173/",
        "http://localhost:5174",
        "http://localhost:5174/"
    };

    if (!string.IsNullOrEmpty(origin) && !allowedOriginsList.Contains(origin))
    {
        context.Response.StatusCode = 403; // Forbidden
        await context.Response.WriteAsync("Origin not allowed");
        return;
    }
    await next.Invoke();
});

// Endpoints (in place of routes/controllers)
app.MapGet("/", async context =>
{
    await context.Response.WriteAsync("Hello from FlexEducation");
});

app.MapGet("/teacher/:userId/CoursesWithStudentCount", async context =>
{
    var userId = context.Request.RouteValues["userId"];
    await context.Response.WriteAsync($"Get courses with student count for teacher {userId}");
});

app.MapGet("/teacher/:userId/Profile", async context =>
{
    var userId = context.Request.RouteValues["userId"];
    await context.Response.WriteAsync($"Get profile for teacher {userId}");
});

// Example of handling a POST request
app.MapPost("/teacher/:userId/CCP", async context =>
{
    var userId = context.Request.RouteValues["userId"];
    using (var reader = new StreamReader(context.Request.Body))
    {
        var body = await reader.ReadToEndAsync();
        await context.Response.WriteAsync($"Change CCP for teacher {userId} with data: {body}");
    }
});

// Other routes as needed (PUT, DELETE)
app.MapDelete("/teacher/:userId/Courses/:courseId", async context =>
{
    var userId = context.Request.RouteValues["userId"];
    var courseId = context.Request.RouteValues["courseId"];
    await context.Response.WriteAsync($"Delete course {courseId} for teacher {userId}");
});

// Start app
app.Run();
