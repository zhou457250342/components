using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPTS.Common.Tasks
{
    public class TaskThreadManager : IDisposable
    {
        public readonly ConcurrentDictionary<string, TaskScheduleThread> Tasks = new ConcurrentDictionary<string, TaskScheduleThread>();
        public TaskThreadManager()
        {
        }
        public void AddTask(ITaskSchedule taskSchedule)
        {
            if (taskSchedule == null || taskSchedule.TaskOperation == null) return;
            if (!Tasks.ContainsKey(taskSchedule.TaskOperation.Name))
                Tasks.TryAdd(taskSchedule.TaskOperation.Name, new TaskScheduleThread(taskSchedule));
        }
        public void Start()
        {
            foreach (var item in Tasks.Keys)
            {
                Tasks[item].Start();
            }
        }
        public void Dispose()
        {
            foreach (var item in Tasks.Keys)
            {
                Tasks[item].Dispose();
            }
        }
    }
}
