using CPTS.Common.Container;
using CPTS.Common.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPTS.TaskManager
{
    public static class EngineExtension
    {
        public static IConfigurationSupport RunTaskSchedule(this IConfigurationSupport configurationSupport)
        {
            EngineContext.CurrentEngine<TaskManagerEngine>().RunStartupTasks(configurationSupport);
            return configurationSupport;
        }
        public static IConfigurationSupport Log4Net(this IConfigurationSupport configurationSupport)
        {
            var config = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Configuration/Log4net.config");
            log4net.Config.XmlConfigurator.Configure(new FileInfo(config));
            return configurationSupport;
        }
        public static IConfigurationSupport RegisterUnExceptionHandler(this IConfigurationSupport configurationSupport)
        {
            AppDomain.CurrentDomain.UnhandledException += (sender, e) =>
            {
                EngineContext.Current.Resolve<ILogger>().Log("全局错误", e.ExceptionObject is Exception ? e.ExceptionObject as Exception : null);
            };
            return configurationSupport;
        }
        /// <summary>
        /// 检查系统必要条件(比较隐形的)
        /// </summary>
        /// <param name="configurationSupport"></param>
        /// <returns></returns>
        public static IConfigurationSupport CheckSystemNecessaryConditionRabbit(this IConfigurationSupport configurationSupport)
        {
            return configurationSupport;
        }
        public static IConfigurationSupport CheckSystemNecessaryCondition(this IConfigurationSupport configurationSupport)
        {
            return configurationSupport;
        }
    }
}
