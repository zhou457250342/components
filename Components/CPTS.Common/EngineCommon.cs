using CPTS.Common.Container;
using CPTS.Common.Container.Infrastructure;
using CPTS.Common.Logging;
using CPTS.Common.Scheduling;
using CPTS.Common.Socketing.Framing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPTS.Common
{
    public class EngineCommon
    {
        private static Func<IObjectContainer> _objectContainer;
        public static IObjectContainer ObjectContainer
        {
            get
            {
                return _objectContainer != null ? _objectContainer() : null;
            }
        }
        internal static ILogger Logger
        {
            get
            {
                if (ObjectContainer == null) return new LoggerEmpty();
                var obj = ObjectContainer.Resolve<ILogger>();
                return obj != null ? obj : new LoggerEmpty();
            }
        }
        internal static IMessageFramer MessageFramer
        {
            get
            {
                if (ObjectContainer == null) return new LengthPrefixMessageFramer();
                var obj = ObjectContainer.Resolve<IMessageFramer>();
                return obj != null ? obj : new LengthPrefixMessageFramer();
            }
        }
        internal static IScheduleService ScheduleService
        {
            get
            {
                if (ObjectContainer == null) return new ScheduleService();
                var obj = ObjectContainer.Resolve<IScheduleService>();
                return obj != null ? obj : new ScheduleService();
            }
        }
        public static void UseContainer(Func<IObjectContainer> objectcontainer)
        {
            _objectContainer = objectcontainer;
        }
    }
}
