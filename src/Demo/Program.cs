// See https://aka.ms/new-console-template for more information

using System.Diagnostics;
using Demo;
using Leonardo;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
IConfiguration configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddEnvironmentVariables()
    .AddJsonFile("appsettings.json", false, true)
    .AddJsonFile($"appsettings.{environmentName}.json", true, true)
    .Build();

var applicationName = configuration.GetValue<string>("Application:Name");
var applicationMessage = configuration.GetValue<string>("Application:Message");
Console.WriteLine($"Application Name : {applicationName}");
Console.WriteLine($"Application Message : {applicationMessage}");

var applicationSection = configuration.GetSection("Application");
var applicationConfig = applicationSection.Get<ApplicationConfig>();

var services = new ServiceCollection();
services.AddTransient<Fibonacci>();
services.AddLogging(configure => configure.AddConsole());
services.AddDbContext<FibonacciDataContext>(options =>
    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

await using var serviceProvider = services.BuildServiceProvider();
var logger = serviceProvider.GetService<ILogger<Program>>();
logger.LogInformation("Application Name : {ApplicationConfigName}", applicationConfig.Name);
logger.LogInformation("Application Message : {ApplicationConfigMessage}", applicationConfig.Message);

var fibonacci = serviceProvider.GetService<Fibonacci>();
var stopwatch = new Stopwatch();
stopwatch.Start();
var results = await fibonacci.RunAsync(args);
stopwatch.Stop();
Console.WriteLine($"Elapsed time: {stopwatch.ElapsedMilliseconds}ms");
foreach (var result in results) Console.WriteLine($"Fibonacci of {result.Result} is {result.Result}");