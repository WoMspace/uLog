using System.Text;

namespace uLog;
public class Logger {

	private StreamWriter stdout;
	private StringBuilder builder = new();

	public Logger(Stream output) { stdout = new StreamWriter(output); }
	public Logger() { stdout = new StreamWriter(Console.OpenStandardOutput()); }
	
	public void Log(Severity severity, string message, string sender) {
		builder.Clear();
		builder.AppendLine();
		builder.Append(DateTime.Now.ToShortTimeString());
		builder.Append(" [");
		builder.Append(GetSeverityName(severity));
		builder.Append("] ");
		builder.Append(sender);
		builder.Append(": ");
		builder.Append(message);
		
		stdout.WriteLine(builder.ToString());
	}

	public void Log(Severity severity, string message) {
		builder.Clear();
		builder.AppendLine();
		builder.Append(DateTime.Now.ToShortTimeString());
		builder.Append(" [");
		builder.Append(GetSeverityName(severity));
		builder.Append("] ");
		builder.Append(message);
		
		stdout.WriteLine(builder.ToString());
	}

	public void Log(string message, string sender) {
		Log(Severity.Info, message, sender);
	}

	public void Log(string message) {
		Log(Severity.Info, message);
	}

	private string GetSeverityName(Severity severity) =>
		severity switch {
			Severity.Debug    => "Debug",
			Severity.Info     => "Info",
			Severity.Warning  => "Warn",
			Severity.Error    => "Error",
			Severity.Critical => "Critical",
			_                 => throw new ArgumentOutOfRangeException(nameof(severity), severity, null)
		};
}
