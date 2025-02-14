# Logger for .NET Apps

This is a simple logging library for .NET Console Applications. It provides logging functionality for both **console** and **file** outputs using an easy-to-use interface. 

## Features
- Log **info**, **warnings**, and **errors**.
- Log to **console** and **file**.
- Cross-platform: works on **Windows**, **macOS**, and **Linux**.
- Supports exception logging.

## Installation
1. Clone the repository or add the files directly to your project.
2. Implement the `ILogger` interface to use either **console logging** or **file logging**.

---

## Usage

### Example:

```csharp
using LogNet.Services;  // Your namespace for services
using LogNet.Interfaces; // Your namespace for interfaces

class Program
{
    static async Task Main(string[] args)
    {
        ILogger logger = new FileLogger(); // Or use ConsoleLogger()

        await logger.LogInfo("Application started.");
        await logger.LogWarning("This is a warning.");
        await logger.LogError("An error occurred.", new Exception("Sample exception."));
    }
}
