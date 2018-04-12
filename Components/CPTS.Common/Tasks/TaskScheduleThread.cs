using CPTS.Common.Logging;
using CPTS.Common.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CPTS.Common.Tasks
{
    public partial class TaskScheduleThread : IDisposable
    {
        private TaskThread _taskThread;
        private ITaskSchedule _taskSchedule;
        private ILogger _logger;
        public TaskScheduleThread(ITaskSchedule taskSchedule)
            : base()
        {
            if (taskSchedule == null || taskSchedule.TaskOperation == null) throw new Exception("初始化失败!");
            _taskSchedule = taskSchedule;
            _logger = EngineCommon.Logger;
            initTaskSchedule();
        }
        public string Code { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public DateTime LastOnTime { get { return this._taskThread.LastOnTime; } }
        public bool RunStatus { get { return this._taskThread.RunStatus; } }
        public int Interval { get { return this._taskThread.Interval; } }
        public bool IsEnabled
        {
            get { return this._taskThread.IsEnabled; }
            set { this._taskThread.IsEnabled = value; }
        }
        public void Start()
        {
            this._taskThread.Start();
        }
        public void Dispose()
        {
            if (_taskThread != null)
                this._taskThread.Dispose();
            if (_taskSchedule != null)
                this._taskSchedule.Dispose();
        }
        void initTaskSchedule()
        {
            this.Code = Guid.NewGuid().ToString();
            this.Name = _taskSchedule.TaskOperation.Name;
            this.Description = _taskSchedule.TaskOperation.Description;

            this._taskThread = new TaskThread();
            this._taskThread.Interval = _taskSchedule.TaskOperation.Interval;
            this._taskThread.IsEnabled = _taskSchedule.TaskOperation.IsEnable;
            if (_taskSchedule.TaskOperation.FixedTime != null)
                this._taskThread.FixedTime = _taskSchedule.TaskOperation.FixedTime.Select(op => new IntervalTime { Day = op.Day, Hour = op.Hour, Minute = op.Minute }).ToList();
            this._taskThread.StopOnErr = _taskSchedule.TaskOperation.StopOnError;

            this._taskThread.CycleExecutionEvent += (sender, e) =>
            {
                _taskSchedule.Excute();
                //this._taskThread.Dispose();
            };
            this._taskThread.CycleExecutionExceptionEvent += (sender, e) =>
            {
                _logger.Log(this.Description + "|监听异常", e.InnerException);
                _taskSchedule.LisenceException(e.InnerException);
            };
        }
    }
}
