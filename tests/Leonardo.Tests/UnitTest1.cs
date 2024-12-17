using Leonardo;
using Microsoft.EntityFrameworkCore;

namespace tests;

public class UnitTest1
{
    [Fact]
    public async void Test1()
    {
        var builder = new DbContextOptionsBuilder<FibonacciDataContext>();
        var dataBaseName = Guid.NewGuid().ToString();
        builder.UseInMemoryDatabase(dataBaseName);
        var options = builder.Options;
        var fibonacciDataContext = new FibonacciDataContext(options);
        await fibonacciDataContext.Database.EnsureCreatedAsync();

        var strings = new[] { "1", "2", "3", "4", "5" };
        var results = new Fibonacci(fibonacciDataContext).RunAsync(strings).Result;
        Assert.Equal(1, results[0].Result);
        Assert.Equal(1, results[1].Result);
        Assert.Equal(2, results[2].Result);
        Assert.Equal(3, results[3].Result);
        Assert.Equal(5, results[4].Result);
    }
}