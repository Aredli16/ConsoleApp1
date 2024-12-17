using System.Text.Json.Serialization;
using Leonardo;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<FibonacciDataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddTransient<Fibonacci>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/", () => "Hello World 2!");

app.MapGet("/fibonacci", async (Fibonacci fibonacci) =>
{
    var result = await fibonacci.RunAsync(["42"]);
    return Results.Json(result, FibonacciContext.Default.ListFibonacciResult);
});

app.Run();

[JsonSerializable(typeof(List<FibonacciResult>))]
internal partial class FibonacciContext : JsonSerializerContext
{
}