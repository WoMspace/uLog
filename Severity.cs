// Copyright (C) 2023 Sasha Beam, All Rights Reserved

namespace uLog;

public enum Severity {
	/// <summary>
	/// Verbose information that is useful for diagnosing unexpected behaviour.
	/// </summary>
	Debug,
	/// <summary>
	/// Fairly verbose information that shows running behaviour.
	/// </summary>
	Info,
	/// <summary>
	/// Information that may indicate a problem.
	/// </summary>
	Warning,
	/// <summary>
	/// Information that strongly indicates a problem.
	/// </summary>
	Error,
	/// <summary>
	/// Equivalent to an aircraft's black box post-crash.
	/// </summary>
	Critical
}