using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPTS.Common.Tasks
{
    public class TaskOperation
    {
        /// <summary>
        /// 固定运行时间
        /// </summary>
        public IList<DateTime> FixedTime { get; set; }
        public string Name { get; set; }
        public bool IsEnable { get; set; }
        public bool StopOnError { get; set; }
        public int Interval { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string PluginsName { get; set; }
        public TaskOperationHandler TaskOperationHandlerTag { get; set; }
    }
    public class TaskOperationHandler
    {
        public int TakeEachCount { get; set; }
        public int FrequencyTime { get; set; }
        public bool HandlerAsync { get; set; }
        public string QueueName { get; set; }
        public string RoutingName { get; set; }
        public string TopicName { get; set; }
        public string OutRoutingName { get; set; }
        public string OutTopicName { get; set; }
    }
}
