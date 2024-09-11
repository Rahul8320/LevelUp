using System.Text;
using Microsoft.Extensions.Logging;

namespace AdapterPattern;

internal class FileLogger<T>(string path)
    : FileStream(path, FileMode.Append), ILogger<T>
{
    public IDisposable? BeginScope<TState>(TState state) where TState : notnull
    {
        throw new NotImplementedException();
    }

    public bool IsEnabled(LogLevel logLevel)
    {
        return true;
    }

    public void Log<TState>(
        LogLevel logLevel,
        EventId eventId,
        TState state,
        Exception? exception,
        Func<TState, Exception?, string> formatter)
    {
        byte[] messageArray = new UTF8Encoding(true).GetBytes(state?.ToString() + "\n");

        Write(messageArray, 0, messageArray.Length);
        Flush();
    }
}
