using Microsoft.Extensions.Logging;

namespace Infrastructure.Common.Logging;

public class ConsoleLogger: ICustomLogger
{
    public void Log(LogLevel logLevel, string message)
    {
        Console.WriteLine($"<Консольный логгер> - level: {logLevel}, message: {message}");
    }

    public void Log(LogLevel logLevel, Exception e)
    {
        Console.WriteLine($"<Консольный логгер> - level: {logLevel}, exception: {e.Message}");
    }
}