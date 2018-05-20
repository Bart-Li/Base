using System;

namespace log4net
{
    //为去掉log4net的依赖，这里完成对LogManager的mock.
    internal static class LogManager
    {
        private static MockLogger _mockLogger = new MockLogger();

        public static ILog GetLogger(Type type)
        {
            return _mockLogger;
        }
    }

    public interface ILog
    {
        void Trace(object message);

        void Warn(object message);

        void Error(object message);

        void Debug(object message);

        void Info(object message);

        void Trace(string message, params object[] exception);

        void Warn(string message, params object[] exception);

        void Error(string message, params object[] exception);

        void Debug(string message, params object[] exception);

        void Info(string message, params object[] exception);

        void TraceFormat(string format, params object[] args);

        void WarnFormat(string format, params object[] args);

        void ErrorFormat(string format, params object[] args);

        void DebugFormat(string format, params object[] args);

        void InfoFormat(string format, params object[] args);

        bool IsDebugEnabled { get; }
    }

    public class MockLogger : ILog
    {
        public void Trace(object message)
        {
        }

        public void Warn(object message)
        {
        }

        public void Error(object message)
        {
        }

        public void Debug(object message)
        {
        }

        public void Info(object message)
        {
        }

        public void Trace(string message, params object[] exception)
        {
        }

        public void Warn(string message, params object[] exception)
        {
        }

        public void Error(string message, params object[] exception)
        {
        }

        public void Debug(string message, params object[] exception)
        {
        }

        public void Info(string message, params object[] exception)
        {
        }

        public void TraceFormat(string format, params object[] args)
        {
        }

        public void WarnFormat(string format, params object[] args)
        {
        }

        public void ErrorFormat(string format, params object[] args)
        {
        }

        public void DebugFormat(string format, params object[] args)
        {
        }

        public void InfoFormat(string format, params object[] args)
        {
        }

        public bool IsDebugEnabled
        {
            get { return false; }
        }
    }
}