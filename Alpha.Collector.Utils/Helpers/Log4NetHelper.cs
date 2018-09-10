using log4net;
using System;

namespace Alpha.Collector.Utils
{
    /// <summary>
    /// log4net帮助类
    /// </summary>
    public static class Log4NetHelper
    {
        /// <summary>
        /// Fatal
        /// </summary>
        /// <param name="type"></param>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        public static void Fatal(Type type, object message, Exception exception = null)
        {
            ILog log = LogManager.GetLogger(type);
            if (exception == null)
            {
                log.Fatal(message);
            }
            else
            {
                log.Fatal(message, exception);
            }
        }

        /// <summary>
        /// Error
        /// </summary>
        /// <param name="type"></param>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        public static void Error(Type type, object message, Exception exception = null)
        {
            ILog log = LogManager.GetLogger(type);
            if (exception == null)
            {
                log.Error(message);
            }
            else
            {
                log.Error(message, exception);
            }
        }

        /// <summary>
        /// Warn
        /// </summary>
        /// <param name="type"></param>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        public static void Warn(Type type, object message, Exception exception = null)
        {
            ILog log = LogManager.GetLogger(type);
            if (exception == null)
            {
                log.Warn(message);
            }
            else
            {
                log.Warn(message, exception);
            }
        }

        /// <summary>
        /// Info
        /// </summary>
        /// <param name="type"></param>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        public static void Info(Type type, object message, Exception exception = null)
        {
            ILog log = LogManager.GetLogger(type);
            if (exception == null)
            {
                log.Info(message);
            }
            else
            {
                log.Info(message, exception);
            }
        }

        /// <summary>
        /// Debug
        /// </summary>
        /// <param name="type"></param>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        public static void Debug(Type type, object message, Exception exception = null)
        {
            ILog log = LogManager.GetLogger(type);
            if (exception == null)
            {
                log.Debug(message);
            }
            else
            {
                log.Debug(message, exception);
            }
        }
    }
}
