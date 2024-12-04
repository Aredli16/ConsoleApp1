using Leonardo;

namespace tests;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        var strings = new string[] { "1", "2", "3", "4", "5" };
        var results = Fibonacci.RunAsync(strings).Result;
        Assert.Equal(1, results[0].Result);
        Assert.Equal(1, results[1].Result);
        Assert.Equal(2, results[2].Result);
        Assert.Equal(3, results[3].Result);
        Assert.Equal(5, results[4].Result);
    }
}