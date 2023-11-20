using Microsoft.Extensions.Logging;

namespace Infrastructure.Common.Logging;

// TODO логирование
public class ConsoleLogger: ICustomLogger
{
    public void Log(LogLevel logLevel, string message)
    {
        Console.WriteLine($"level: {logLevel}, message: {message}");
    }

    public void Log(LogLevel logLevel, Exception e)
    {
        Console.WriteLine($"level: {logLevel}, exception: {e.Message}");
    }
}