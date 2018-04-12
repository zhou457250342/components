using CPTS.Common.Component;
using CPTS.Common.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace CPTS.TaskManager
{
    public class RunTaskScheduleConfig : BaseConfig
    {
        public readonly IList<TaskOperation> TaskOperations = new List<TaskOperation>();
        public override object Create(object parent, object configContext, System.Xml.XmlNode section)
        {
            var config = new RunTaskScheduleConfig();
            var nodes = section.SelectNodes("TaskSchedule");
            if (nodes != null && nodes.Count > 0)
            {
                try
                {
                    foreach (XmlNode item in nodes)
                    {
                        var taskOperation = new TaskOperation
                        {
                            Name = attributeValue<string>(item, "name"),
                            Description = attributeValue<string>(item, "descripttion"),
                            Type = attributeValue<string>(item, "type"),
                            FixedTime = attributeValueArray<DateTime>(item, "fixedTime"),
                            IsEnable = attributeValue<bool>(item, "isEnable"),
                            StopOnError = attributeValue<bool>(item, "stopOnError"),
                            Interval = attributeValue<int>(item, "interval"),
                            PluginsName = attributeValue<string>(item, "pluginsName"),
                        };
                        var handernode = item.SelectSingleNode("TaskScheduleHander");
                        if (handernode != null)
                        {

                        }
                        config.TaskOperations.Add(taskOperation);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("任务计划安排初始化异常~", ex);
                }
            }
            return config;
        }

    }
}
