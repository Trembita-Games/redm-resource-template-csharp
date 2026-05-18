using System;

namespace RedmResourceTemplateCSharp.Shared.Logging
{
    /// <summary>
    /// Shared log formatting helper.
    /// </summary>
    /// <remarks>
    /// Client and server runtimes expose their own logging APIs.
    /// This helper keeps formatting centralized without coupling shared code
    /// to a specific runtime implementation.
    /// </remarks>
    public static class LogHelper
    {
        public static void Debug(Action<string> writeLine, string message)
        {
            /*if (!ResourceConfiguration.Debug)
            {
                return;
            }*/

            Write(writeLine, "DEBUG", message);
        }

        public static void Info(Action<string> writeLine, string message)
        {
            Write(writeLine, "INFO", message);
        }

        public static void Warning(Action<string> writeLine, string message)
        {
            Write(writeLine, "WARNING", message);
        }

        public static void Error(Action<string> writeLine, string message)
        {
            Write(writeLine, "ERROR", message);
        }

        private static void Write(Action<string> writeLine, string level, string message)
        {
            if (writeLine == null)
            {
                throw new ArgumentNullException(nameof(writeLine));
            }

            if (level == null)
            {
                throw new ArgumentNullException(nameof(level));
            }

            if (message == null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            writeLine(Format(level, message));
        }

        private static string Format(string resourceName, string message)
        {
            if (resourceName == null)
            {
                throw new ArgumentNullException(nameof(resourceName));
            }

            if (message == null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            return "[" + ResourceConfiguration.ResourceName + "] [" + resourceName + "] " + message;
        }
    }
}
