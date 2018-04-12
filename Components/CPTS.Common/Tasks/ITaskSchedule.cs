using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPTS.Common.Tasks
{
    public interface ITaskSchedule : IDisposable
    {
        TaskOperation TaskOperation { get; set; }
        void Excute();
        void LisenceException(Exception ex);
    }
}
