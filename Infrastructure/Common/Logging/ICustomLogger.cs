using Microsoft.Extensions.Logging;

namespace Infrastructure.Common.Logging;

// TODO подключить нормальное логирование
public interface ICustomLogger
{
    public void Log(LogLevel logLevel, string message);

    public void Log(LogLevel logLevel, Exception e);
}