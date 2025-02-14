using LogNet.Interfaces;

namespace LogNet.Services
{
    internal class FileLogger : ILogger
    {
        private readonly string _logFolderPath = "Logs";

        public async Task LogInfo(string message)
        {
            await WriteLogToFile($"INFO: {message}");
        }

        public async Task LogWarning(string message)
        {
            await WriteLogToFile($"WARNING: {message}");
        }

        public async Task LogError(string message, Exception? ex = null)
        {
            await WriteLogToFile($"ERROR: {message}");
            if (ex != null)
            {
                await WriteLogToFile($"Exception: {ex.Message}");
            }
        }

        public async Task<string?> WriteLogToFile(string logMessage)
        {
            string filePath = GetLogFilePath();

            try
            {
                using StreamWriter writer = new(filePath, true);
                await writer.WriteLineAsync($"{DateTime.Now}: {logMessage}");
                return null;
            }
            catch (IOException ioEx)
            {
                // Handle file access or disk issues
                string ioErrorMessage = $"File I/O error: {ioEx.Message}\n{ioEx.StackTrace}";
                HandleCriticalError(ioErrorMessage);
                return ioErrorMessage;
            }
            catch (UnauthorizedAccessException uaEx)
            {
                // Handle permission-related errors
                string accessErrorMessage = $"Access error: {uaEx.Message}\n{uaEx.StackTrace}";
                HandleCriticalError(accessErrorMessage);
                return accessErrorMessage;
            }
            catch (Exception ex)
            {
                // Handle general errors
                string generalErrorMessage = $"An unexpected error occurred: {ex.Message}\n{ex.StackTrace}";
                HandleCriticalError(generalErrorMessage);
                return generalErrorMessage;
            }
        }

        private string GetLogFilePath()
        {
            // Get the appropriate directory for storing logs based on the platform
            string logDirectory;

            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
            {
                // Windows - Local Application Data: C:\Users\<Username>\AppData\Local\LogNet\Logs\logs_MM_DD.log
                logDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "LogNet", _logFolderPath);
            }
            else
            {
                // macOS/Linux - User's home directory: ~/.lognet/logs/logs_MM_DD.log
                logDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".lognet", _logFolderPath);
            }

            // Ensure the Logs directory exists
            Directory.CreateDirectory(logDirectory);

            string currentFile = $"logs_{DateTime.Now:MM_dd}.log";
            return Path.Combine(logDirectory, currentFile);
        }

        private static void HandleCriticalError(string errorMessage)
        {
            // Log write errors to the console
            Console.WriteLine($"ERROR (critical): {errorMessage}");
        }
    }
}
