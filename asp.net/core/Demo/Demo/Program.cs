var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// middleware chain
// middleware 1 (e.g. this could enable HTTPS redirection)
app.Use(async (HttpContext context, RequestDelegate next) => {
    Console.WriteLine("Running middleware 1");
    // await context.Response.WriteAsync("Running middleware 1\n");
    await next(context);
});
// middleware 2 (e.g. this could enable auth)
app.Use(async (context, next) => {
    Console.WriteLine("Running middleware 2");
    // await context.Response.WriteAsync("Running middleware 2\n");
    await next(context);
});

// middleware 3 (e.g. another component added to the pipeline)
app.Use(async (context, next) => {
    Console.WriteLine("Running middleware 2");
    // await context.Response.WriteAsync("Running middleware 2\n");
    // not a terminal middleware, so we need to invoke the next one
    await next(); 
});

//app.MapGet("/", () => "Hello World!");
app.MapGet("/", async(context) => {
    await context.Response.WriteAsync("Hello, World!");
});

app.Map("/employees/{id:int?}", async (context) => 
{
    if (context.Request.RouteValues.ContainsKey("id"))
    {
        int empId = Convert.ToInt32(context.Request.RouteValues["id"]);
        await context.Response.WriteAsync($"Employee with id {empId} goes here");
    } else
    {
        await context.Response.WriteAsync("Employee id not supplied");
    }
});

app.Map("/files/{filename:minlength(3)}.{extension = txt}", async (context) =>
{
    string? fileName    = Convert.ToString(context.Request.RouteValues["filename"]);
    string? extension   = Convert.ToString(context.Request.RouteValues["extension"]);
    await context.Response.WriteAsync($"Requesting file: {fileName}.{extension}");
});

app.MapFallback(async (context) => {
    await context.Response.WriteAsync($"Page Not Found at { context.Request.Path}");
});

app.Run();
