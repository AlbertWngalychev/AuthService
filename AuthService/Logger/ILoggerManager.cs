namespace DataBaseService.Logger
{
    public interface ILoggerManager
    {
        void Write(NLog.LogLevel level, string message);
        void WriteError(string message);
        void WriteFatal(string message);
    }
}
