namespace MKExpress.API.Logger
{
    public interface ILoggerManager
    {
        void LogInfo(string message);
        void LogWarn(string message);
        void LogWarn(string message,string className,string methodName);
        void LogDebug(string message);
        void LogError(Exception ex, string message);
    }
}
