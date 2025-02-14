using LogNet.Interfaces;

namespace LogNet.Services
{
    internal class ConsoleLogger : ILogger
    {
        public Task LogInfo(string message)
        {
            Console.WriteLine($"INFO: {message}");
            return Task.CompletedTask;
        }

        public Task LogWarning(string message)
        {
            Console.WriteLine($"WARNING: {message}");
            return Task.CompletedTask;
        }

        public Task LogError(string message, Exception? ex = null)
        {
            Console.WriteLine($"ERROR: {message}");
            if (ex != null)
            {
                Console.WriteLine($"EXCEPTION: {ex.Message}");
            }
            return Task.CompletedTask;
        }
    }
}
