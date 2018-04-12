using CPTS.Common.Container;
using CPTS.Common.Container.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CPTS.TaskManager
{
    public sealed class EngineContext
    {
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static IConfigurationSupport Initialize(bool forceRecreate = true)
        {
            IConfigurationSupport config = null;
            if (Singleton<IEngine>.Instance == null || forceRecreate)
            {
                Singleton<IEngine>.Instance = new TaskManagerEngine();
                config = new TaskManagerConfig()
                {
                };
                Singleton<IEngine>.Instance.Initialize(config);
            }
            return config;
        }
        public static IEngine Current
        {
            get
            {
                if (Singleton<IEngine>.Instance == null)
                {
                    Initialize(false);
                }
                return Singleton<IEngine>.Instance;
            }
        }
        public static T CurrentEngine<T>()
        {
            if (Current is T)
                return (T)Current;
            return default(T);
        }
    }
}
