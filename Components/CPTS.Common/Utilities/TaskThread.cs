using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CPTS.Common.Utilities
{
    public partial class TaskThread : IDisposable
    {
        private Thread _thread;
        private bool _disposed;
        public event EventHandler<EventArgs> CycleExecutionEvent;
        public event EventHandler<ExceptionEventArgs> CycleExecutionExceptionEvent;

        public TaskThread()
        {
            this.CycleExecutionInterval = 1000;
            this.IsEnabled = true;
            this.FixedTime = new List<IntervalTime>();
            this.initThread();
        }
        public void Dispose()
        {
            if ((this._thread != null) && !this._disposed)
            {
                lock (this)
                {
                    this._disposed = true;
                    this._thread.Join();
                    this._thread = null;
                }
            }
        }
        public void Start()
        {
            if (_thread != null && _thread.ThreadState == ThreadState.Unstarted)
                _thread.Start();
        }

        bool isFixTime()
        {
            var flag = false;
            if (FixedTime != null && FixedTime.Count >= 0)
            {
                var now = DateTime.Now;
                foreach (var item in FixedTime)
                {
                    if (item.RunTime != null)
                    {
                        if (item.Day != null && item.RunTime.Value.Day != now.Day)
                            item.RunTime = null;
                        if (item.Hour != null && item.RunTime.Value.Hour != now.Day)
                            item.RunTime = null;
                        if (item.Minute != null && item.RunTime.Value.Minute != now.Minute)
                            item.RunTime = null;
                        continue;
                    }
                    if (item.Day != null && item.Day != now.Day)
                        continue;
                    if (item.Hour != null && item.Hour != now.Hour)
                        continue;
                    if (item.Minute != null && item.Minute != now.Minute)
                        continue;
                    flag = true;
                    item.RunTime = DateTime.Now;
                    break;
                }
            }
            return flag;
        }
        void initThread()
        {
            _thread = new Thread(op =>
            {
                while (!_disposed)
                {
                    if (!IsEnabled)
                        continue;
                    LastOnTime = DateTime.Now;
                    try
                    {
                        if (CycleExecutionEvent != null)
                        {
                            if (isFixTime())
                                CycleExecutionEvent(this, null);
                            else if (this.FixedTime.Count <= 0)
                                CycleExecutionEvent(this, null);
                        }
                    }
                    catch (Exception ex)
                    {
                        if (CycleExecutionExceptionEvent != null)
                            CycleExecutionExceptionEvent(this, new ExceptionEventArgs(ex));
                        if (this.StopOnErr)
                            this.Dispose();
                    }
                    if (this.Interval > 0)
                        Thread.Sleep(this.Interval);
                }
            });
        }

        #region Property
        /// <summary>
        /// 任务预估时间
        /// </summary>
        public int CycleExecutionInterval
        {
            get;
            set;
        }
        public int Interval
        {
            get;
            set;
        }
        public bool IsEnabled
        {
            get;
            set;
        }
        public bool StopOnErr
        {
            get;
            set;
        }
        public IList<IntervalTime> FixedTime { get; set; }
        public DateTime LastOnTime { get; protected set; }
        public bool RunStatus
        {
            get
            {
                return (DateTime.Now - this.LastOnTime).TotalMilliseconds <= this.Interval + this.CycleExecutionInterval + 2000;
            }
        }
        #endregion
    }
    public class IntervalTime
    {
        public int? Day { get; set; }
        public int? Hour { get; set; }
        public int? Minute { get; set; }
        public DateTime? RunTime { get; set; }
    }
    public class ExceptionEventArgs
    {
        public Exception InnerException { get; private set; }
        public ExceptionEventArgs(Exception innerException)
        {
            InnerException = innerException;
        }
    }
}
