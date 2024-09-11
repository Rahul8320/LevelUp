using AdapterPattern;
using Microsoft.Extensions.Logging;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");

        var logger = new FileLogger<Program>("Log.txt");

        logger.LogDebug("This is a new debug log message from Program.");
    }
}