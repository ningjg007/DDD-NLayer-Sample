using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace NLayer.Presentation.WebHost
{
    public static class ExceptionExtensions
    {
        public static string GetIndentedExceptionLog(this Exception ex)
        {
            if (ex == null)
            {
                return string.Empty;
            }

            var sb = new StringBuilder(64);
            var innerExceptionLog = GetIndentedExceptionLog(ex.InnerException).Replace("\n", "\n\t");
            sb.AppendFormat(
                "Message: {1}{0}Type: {2}{0}StackTrace: {3}{0}InnerException: {4}",
                Environment.NewLine,
                ex.Message,
                ex.GetType().FullName,
                ex.StackTrace,
                innerExceptionLog);

            return sb.ToString();
        }

        public static string GetIndentedExceptionMessage(this Exception ex)
        {
            var sb = new StringBuilder();
            var index = 1;
            sb.AppendLine(ex.Message);
            var e = ex;
            while (e.InnerException != null)
            {
                sb.AppendFormat("{0}{1}", Enumerable.Repeat("\t", index),e.InnerException.Message);
                sb.AppendLine();
                e = e.InnerException;
                index++;
            }
            return sb.ToString();
        }
    }
}