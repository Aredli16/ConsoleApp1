namespace Leonardo;

public record FibonacciResult(int Input, int Result);

public static class Fibonnaci
{
    private static int Run(int i)
    {
        if (i <= 2)
        {
            return 1;
        }

        return Run(i - 1) + Run(i - 2);
    }
    
    public static async Task<List<FibonacciResult>> RunAsync(string[] strings)
    {
        var tasks = new List<Task<FibonacciResult>>();
        foreach (var input in strings)
        {
            var i = int.Parse(input);
            tasks.Add(Task.Run(() => new FibonacciResult(i, Run(i))));
        }

        var results = new List<FibonacciResult>();
        foreach (var task in tasks)
        {
            results.Add(await task);
        }
        
        return results;
    }
}