using System;

namespace Newegg.EC.Core.Log
{
    /// <summary>
    /// Logger mananger.
    /// </summary>
    public interface ILogManager
    {
        /// <summary>
        /// Add variable to log file.
        /// </summary>
        /// <param name="variableName">Variable name.</param>
        /// <param name="variable">Variable content.</param>
        void AddVariable(string variableName, object variable);

        /// <summary>
        /// Write log with trace level.
        /// </summary>
        /// <param name="message">Log message.</param>
        /// <param name="category">Log category.</param>
        /// <param name="exception">Exception message.</param>
        void Trace(string message, string category = "TraceCategory", Exception exception = null);

        /// <summary>
        /// Write log with debug level.
        /// </summary>
        /// <param name="message">Log message.</param>
        /// <param name="category">Log category.</param>
        /// <param name="exception">Exception message.</param>
        void Debug(string message, string category = "DebugCategory", Exception exception = null);

        /// <summary>
        /// Write log with info level.
        /// </summary>
        /// <param name="message">Log message.</param>
        /// <param name="category">Log category.</param>
        /// <param name="exception">Exception message.</param>
        void Info(string message, string category = "InfoCategory", Exception exception = null);

        /// <summary>
        /// Write log with warn level.
        /// </summary>
        /// <param name="message">Log message.</param>
        /// <param name="category">Log category.</param>
        /// <param name="exception">Exception message.</param>
        void Warn(string message, string category = "WarnCategory", Exception exception = null);

        /// <summary>
        /// Write log with error level.
        /// </summary>
        /// <param name="message">Log message.</param>
        /// <param name="category">Log category.</param>
        /// <param name="exception">Exception message.</param>
        void Error(string message, string category = "ErrorCategory", Exception exception = null);

        /// <summary>
        /// Write log with fatal level.
        /// </summary>
        /// <param name="message">Log message.</param>
        /// <param name="category">Log category.</param>
        /// <param name="exception">Exception message.</param>
        void Fatal(string message, string category = "FatalCategory", Exception exception = null);
    }
}
