namespace Logger
{
    public interface ILoggerMethod
    {
        void LogInfo(string message);
        void LogWarning(string message);
        void LogError(string message);
    }
}