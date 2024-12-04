using Leonardo;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/Fibonacci", async () => await Fibonacci.RunAsync(["44", "43"]));

app.Run();
