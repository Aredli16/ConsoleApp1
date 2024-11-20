// See https://aka.ms/new-console-template for more information

using System.Diagnostics;
using Leonardo;

var stopwatch = new Stopwatch();
stopwatch.Start();
var results = await Fibonnaci.RunAsync(args);
stopwatch.Stop();
Console.WriteLine($"Elapsed time: {stopwatch.ElapsedMilliseconds}ms");

foreach (var result in results) 
{
    Console.WriteLine(result.Result);
}
