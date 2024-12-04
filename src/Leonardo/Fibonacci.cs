using Microsoft.EntityFrameworkCore;

namespace Leonardo;

public record FibonacciResult(int Input, long Result);

public static class Fibonacci
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
        var context = new FibonacciDataContext();
        
        foreach (var input in strings)
        {
            var i = int.Parse(input);
            var result = await context.TFibonaccis.Where(f => f.FibInput == i).FirstOrDefaultAsync();

            tasks.Add(result != null
                ? Task.FromResult(new FibonacciResult(i, result.FibOutput))
                : Task.Run(() => new FibonacciResult(i, Run(i))));
        }

        var results = new List<FibonacciResult>();
        foreach (var task in tasks)
        {
            results.Add(await task);
            
            context.TFibonaccis.Add(new TFibonacci
            {
                FibInput = results.Last().Input,
                FibOutput = results.Last().Result
            });
        }
        
        await context.SaveChangesAsync();
        return results;
    }
}