using System;
using System.Collections.Generic;
using System.IO;

namespace uTools.uLog {

/// <summary>
/// A very small and easy-to-use class for simple logging.
/// </summary>
public class Logger {

	/// <summary>
	/// A list of streams that will be written to.
	/// </summary>
	/// <para>
	/// For example, writing to both the console and a log file.
	/// </para>
	private List<StreamWriter> outputs = new List<StreamWriter>();

	/// <summary>
	/// Severities less important than this will not be logged.
	/// </summary>
	public Severity LogLevel { get; set; } = Severity.Info;

	/// <summary>
	/// Whether to include the date with the log timestamp.
	/// </summary>
	public bool IncludeDate { get; set; } = false;

	/// <summary>
	/// Initialise a new instance of the <c>Logger</c> class that will only output to the console.
	/// </summary>
	/// <para>
	/// Additional stream outputs can still be added later.
	/// </para>
	public Logger() { outputs.Add(new StreamWriter(Console.OpenStandardOutput())); }

	/// <summary>
	/// Initialise a new instance of the <c>Logger</c> class that will only output to the given stream.
	/// </summary>
	/// <para>
	/// This effectively changes the default stream logging occurs to from the console to the given stream.
	/// </para>
	/// <param name="output">The stream to initialise logging to.</param>
	public Logger(Stream output) { outputs.Add(new StreamWriter(output)); }

	/// <summary>
	/// Initialise a new instance of the <c>Logger</c> class that will output to every stream in the given array.
	/// </summary>
	/// <param name="outputs">An array of <c>Stream</c> objects that events should be logged to.</param>
	public Logger(Stream[] outputs) {
		foreach(var stream in outputs) { this.outputs.Add(new StreamWriter(stream)); }
	}


	/// <summary>
	/// Add an additional output to log events to.
	/// </summary>
	/// <param name="output">The <c>Stream</c> object to add.</param>
	public void AddOutput(Stream output) => outputs.Add(new StreamWriter(output));


	/// <summary>
	/// Log an event.
	/// </summary>
	/// <param name="severity">The severity of the event.</param>
	/// <param name="message">The log message.</param>
	/// <param name="sender">The internal category of the event.</param>
	public void Log(Severity severity, string message, string sender) {
		if(severity >= LogLevel) {
			string line = $"{FormatDate(DateTime.Now)} [{GetSeverityName(severity)}] {sender}: {message}";
			WriteOutput(line);
		}
	}

	/// <summary>
	/// Log an event.
	/// </summary>
	/// <param name="severity">The severity of the event.</param>
	/// <param name="message">The log message.</param>
	public void Log(Severity severity, string message) {
		if(severity >= LogLevel) {
			string line = $"{FormatDate(DateTime.Now)} [{GetSeverityName(severity)}] {message}";
			WriteOutput(line);
		}
	}



	/// <summary>
	/// Write the given string to all attached outputs.
	/// </summary>
	/// <param name="line">The message to write.</param>
	private void WriteOutput(string line) {
		foreach(var writer in outputs) { writer.WriteLine(line); }
	}

	/// <summary>
	/// Format the given <c>DateTime</c> to a human-readable string, respecting <c>IncludeDate</c>.
	/// </summary>
	/// <param name="time">The time to format.</param>
	/// <returns>A string containing the formatted time and optionally date.</returns>
	private string FormatDate(DateTime time) {
		if(IncludeDate) {
			return $"{time.Year:0000}-{time.Month:00}-{time.Day:00} {time.Hour:00}:{time.Minute:00}:{time.Second:00}";
		} else { return $"{time.Hour:00}:{time.Minute:00}:{time.Second:00}"; }
	}

	

	/// <summary>
	/// Return the human-readable name of the given <c>Severity</c>.
	/// </summary>
	/// <param name="severity">The <c>Severity</c> to determine the name of.</param>
	/// <returns>A string containing the human-readable name of the <c>Severity</c>.</returns>
	/// <exception cref="ArgumentOutOfRangeException">Thrown if the severity does not exist.</exception>
	private string GetSeverityName(Severity severity) {
		switch(severity) {
			case Severity.Debug:    return "Debug";
			case Severity.Info:     return "Info";
			case Severity.Warning:  return "Warn";
			case Severity.Error:    return "Error";
			case Severity.Critical: return "Critical";
			default:                throw new ArgumentOutOfRangeException(nameof(severity), severity, null);
		}
	}
}

}