using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CPTS.Common.Utilities;
using System.Configuration;

namespace CPTS.Common.Logging
{
    public class Logger : ILogger
    {
        public void Log(string msg, Exception ex = null, string type = null)
        {
            try
            {
                if (string.IsNullOrEmpty(type)) type = "SystemLog";
                log4net.ILog log = log4net.LogManager.GetLogger(type);
                log.Info(string.Format("msg:{0},errmsg:{1}", msg, ex != null ? ex.GetFullMessage() : string.Empty));
            }
            catch (Exception)
            {
            }
        }
        public void Debug(string msg, Exception ex = null, string type = null)
        {
            var isdebug = CPTS.Common.Utilities.UtiltyHelper.ParseType<bool>(ConfigurationManager.AppSettings["IsDebug"]);
            if (isdebug)
                this.Log(msg, ex, string.IsNullOrEmpty(type) ? "DebugLog" : type);
        }
        public void InfoFormat(string formart, params object[] args)
        {
            this.Log(string.Format(formart, args));
        }
        public void Error(string msg, Exception ex)
        {
            this.Log(msg, ex);
        }
        public void ErrorFormat(string format, params object[] args)
        {
            this.Log(string.Format(format, args));
        }
        public void Info(string message, Exception exception)
        {
            this.Log(message, exception);
        }
        public void Debug(string msg, params object[] args)
        {
            this.Debug(string.Format(msg, args));
        }
    }
}
