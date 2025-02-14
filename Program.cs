using LogNet.Services;

var conLog = new ConsoleLogger();
var fileLog = new FileLogger();

await conLog.LogInfo("Application started.");
await conLog.LogWarning("This is a warning.");
await conLog.LogError("Something went wrong!", new Exception("Sample exception."));

await fileLog.LogInfo("Application started.");
await fileLog.LogWarning("This is a warning.");
await fileLog.LogError("Something went wrong!", new Exception("Sample exception."));
