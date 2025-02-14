namespace LogNet.Interfaces
{
    internal interface ILogger
    {
        Task LogInfo(string message);
        Task LogWarning(string message);
        Task LogError(string message, Exception? ex = null);
    }
}
