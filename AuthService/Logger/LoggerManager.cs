using NLog;

namespace DataBaseService.Logger
{
    public class LoggerManager : ILoggerManager
    {
        private static readonly NLog.Logger logger = LogManager.GetCurrentClassLogger();
        public LoggerManager(string pathToConfigNLog)
        {
            LogManager.LoadConfiguration(pathToConfigNLog);
        }

        public LoggerManager()
            : this(string.Concat(Directory.GetCurrentDirectory(), "\\Logger\\nlog.config"))
        {
        }

        public void Write(NLog.LogLevel level, string message)
        {
            logger.Log(level, message);
        }

        public void WriteError(string message)
        {
            logger.Log(NLog.LogLevel.Error, message);
        }

        public void WriteFatal(string message)
        {
            logger.Log(NLog.LogLevel.Fatal, message);
        }
    }
}
