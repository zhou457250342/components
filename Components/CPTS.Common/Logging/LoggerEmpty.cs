using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPTS.Common.Logging
{
    public class LoggerEmpty : ILogger
    {
        public void Log(string msg, Exception ex = null, string type = null)
        {
        }
        public void Debug(string msg, Exception ex = null, string type = null)
        {
        }
        public void InfoFormat(string formart, params object[] args)
        {
        }
        public void Error(string msg, Exception ex)
        {
        }
        public void ErrorFormat(string format, params object[] args)
        {
        }
        public void Info(string message, Exception exception)
        {
        }
        public void Debug(string msg, params object[] args)
        {
        }
    }
}
