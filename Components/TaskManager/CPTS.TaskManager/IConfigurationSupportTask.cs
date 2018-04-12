using CPTS.Common.Container;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPTS.TaskManager
{
    public interface IConfigurationSupportTask : IConfigurationSupport
    {
        RunTaskScheduleConfig RunTaskSchedule { get; set; }
    }
}
