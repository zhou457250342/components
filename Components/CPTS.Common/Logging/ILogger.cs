using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPTS.Common.Logging
{
    public interface ILogger
    {
        void Log(string msg, Exception ex = null, string type = null);
        void Debug(string msg, Exception ex = null, string type = null);
        void Debug(string msg, params object[] args);
        void InfoFormat(string formart, params object[] args);
        void Info(string message, Exception exception);
        void Error(string msg, Exception ex);
        void ErrorFormat(string format, params object[] args);
    }

}
