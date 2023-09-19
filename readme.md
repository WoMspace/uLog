# μLog
*microLog*

### A small logging utility for projects that need something simple and easy.

Basic Usage:

```csharp
// Initialise a logger with default settings
var logger = new Logger();

// Change loglevel to Warnings and worse
logger.LogLevel = Severity.Warning;

// Add a file output
logger.AddOutput(File.OpenWrite("mylog.log"));

// Finally log something
logger.Log(Severity.Info, "Logging ready");
```