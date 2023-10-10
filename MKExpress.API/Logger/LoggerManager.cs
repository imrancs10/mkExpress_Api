using NLog;
using ILogger = NLog.ILogger;

namespace MKExpress.API.Logger
{
    public class LoggerManager : ILoggerManager
    {
        private static ILogger logger;
        private static IConfiguration _configuration;
        public LoggerManager(IConfiguration configuration)
        {
            _configuration = configuration;
            var loggingtype = _configuration["AppLogging:Loggingtype"];
            var loggingPath = _configuration["AppLogging:LogFilePath"];
            var logFileName = _configuration["AppLogging:LogFileName"];

            LogManager.Configuration.Variables["basedir"] = loggingPath;
            LogManager.Configuration.Variables["LogFileName"] = logFileName;

            if (loggingtype?.ToLower() == "file")
            {
                logger = LogManager.GetCurrentClassLogger();
            }

            GlobalDiagnosticsContext.Set("basedir", loggingPath);
            GlobalDiagnosticsContext.Set("LogFileName", logFileName);
           
        }
       
        public void LogDebug(string message) => logger.Debug(message);
        public void LogError(Exception ex, string message) => logger.Error(ex,message);
        public void LogInfo(string message) => logger.Info(message);
        public void LogWarn(string message) => logger.Warn(message);
    }
}
