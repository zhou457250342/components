using CPTS.Common.Container;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPTS.TaskManager
{
    public class TaskManagerConfig : IConfigurationSupportTask
    {
        public RunTaskScheduleConfig RunTaskSchedule
        {
            get;
            set;
        }
    }
}
