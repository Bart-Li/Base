using System;
using System.Collections.Generic;
using System.Linq;
using Newegg.EC.Core.Serialization;
using NLog;

namespace Newegg.EC.Core.Logger.Impl
{
    [AutoSetupService(typeof(ILogManager), LifeTime = ServiceLifeTime.Transient)]
    public class DefaultLogManager : ILogManager
    {
        private readonly ISerializer _serializer;
        private readonly IDictionary<string, object> _variables;

        /// <summary>
        /// Default logger.
        /// </summary>
        /// <param name="serializer">Serializer format.</param>
        public DefaultLogManager(ISerializer serializer)
        {
            this._serializer = serializer;
            this._variables = new Dictionary<string, object>();
        }

        /// <summary>
        /// Add variable to log file.
        /// </summary>
        /// <param name="variableName">Variable name.</param>
        /// <param name="variable">Variable content.</param>
        public void AddVariable(string variableName, object variable)
        {
            if (this._variables.ContainsKey(variableName))
            {
                this._variables[variableName] = variable;
            }
            else
            {
                this._variables.Add(variableName, variable);
            }
        }

        /// <summary>
        /// Write log with trace level.
        /// </summary>
        /// <param name="message">Log message.</param>
        /// <param name="category">Log category.</param>
        /// <param name="exception">Exception message.</param>
        public void Trace(string message, string category = "TraceLog", Exception exception = null)
        {
            this.WriteLog(LogLevel.Trace, message, category, exception);
        }

        /// <summary>
        /// Write log with debug level.
        /// </summary>
        /// <param name="message">Log message.</param>
        /// <param name="category">Log category.</param>
        /// <param name="exception">Exception message.</param>
        public void Debug(string message, string category = "DebugCategory", Exception exception = null)
        {
            this.WriteLog(LogLevel.Debug, message, category, exception);
        }

        /// <summary>
        /// Write log with info level.
        /// </summary>
        /// <param name="message">Log message.</param>
        /// <param name="category">Log category.</param>
        /// <param name="exception">Exception message.</param>
        public void Info(string message, string category = "InfoCategory", Exception exception = null)
        {
            this.WriteLog(LogLevel.Info, message, category, exception);
        }

        /// <summary>
        /// Write log with warn level.
        /// </summary>
        /// <param name="message">Log message.</param>
        /// <param name="category">Log category.</param>
        /// <param name="exception">Exception message.</param>
        public void Warn(string message, string category = "WarnCategory", Exception exception = null)
        {
            this.WriteLog(LogLevel.Warn, message, category, exception);
        }

        /// <summary>
        /// Write log with error level.
        /// </summary>
        /// <param name="message">Log message.</param>
        /// <param name="category">Log category.</param>
        /// <param name="exception">Exception message.</param>
        public void Error(string message, string category = "ErrorCategory", Exception exception = null)
        {
            this.WriteLog(LogLevel.Error, message, category, exception);
        }

        /// <summary>
        /// Write log with fatal level.
        /// </summary>
        /// <param name="message">Log message.</param>
        /// <param name="category">Log category.</param>
        /// <param name="exception">Exception message.</param>
        public void Fatal(string message, string category = "FatalCategory", Exception exception = null)
        {
            this.WriteLog(LogLevel.Fatal, message, category, exception);
        }

        /// <summary>
        /// Write log.
        /// </summary>
        /// <param name="logLevel">Log level.</param>
        /// <param name="message">Log message.</param>
        /// <param name="category">Log category.</param>
        /// <param name="exception">Exception message.</param>
        private void WriteLog(LogLevel logLevel, string message, string category, Exception exception = null)
        {
            try
            {
                if (this._variables.Any())
                {
                    foreach (var variable in this._variables)
                    {
                        if (variable.GetType() == typeof(string))
                        {
                            LogManager.Configuration.Variables[variable.Key] = variable.ToString();
                        }
                        else
                        {
                            LogManager.Configuration.Variables[variable.Key] = this._serializer.SerializeObject(variable.Value);
                        }
                    }
                }

                var logger = LogManager.GetLogger(category);

                switch (logLevel)
                {
                    case LogLevel.Fatal:
                        logger.Fatal(exception, message);
                        break;
                    case LogLevel.Error:
                        logger.Error(exception, message);
                        break;
                    case LogLevel.Warn:
                        logger.Warn(exception, message);
                        break;
                    case LogLevel.Info:
                        logger.Info(exception, message);
                        break;
                    case LogLevel.Debug:
                        logger.Debug(exception, message);
                        break;
                    case LogLevel.Trace:
                        logger.Trace(exception, message);
                        break;
                    default:
                        logger.Debug(exception, message);
                        break;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
