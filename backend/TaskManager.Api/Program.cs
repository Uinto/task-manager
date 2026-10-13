var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "TaskManager API is running");
app.MapGet("/api/info", () => new { name = "TaskManager", version = "0.1.0" });

app.Run();
