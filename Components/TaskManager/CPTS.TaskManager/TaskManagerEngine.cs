using CPTS.Common.Container;
using CPTS.Common.Container.Infrastructure;
using CPTS.Common.Plugin;
using CPTS.Common.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPTS.TaskManager
{
    public sealed class TaskManagerEngine : SupportEngine
    {
        private TaskThreadManager _taskThreadManager;
        public override void Initialize(IConfigurationSupport config)
        {
            _taskThreadManager = new TaskThreadManager();
            RegisterDependencies(config);
        }
        public void RunPluginInstall(IConfigurationSupport config)
        {
            PluginManager.MarkAllPluginsAsUninstalled();
            var pluginFinder = Resolve<IPluginFinder>();
            var plugins = pluginFinder.GetPlugins<IPlugin>(LoadPluginsMode.All)
                      .ToList()
                      .OrderBy(x => x.PluginDescriptor.Group)
                      .ThenBy(x => x.PluginDescriptor.DisplayOrder)
                      .ToList();
            foreach (var plugin in plugins) plugin.Install();
        }
        public void RunStartupTasks(IConfigurationSupport supportConfig)
        {
            if (_taskThreadManager != null)
                _taskThreadManager.Dispose();
            var configurationSupportTask = supportConfig as IConfigurationSupportTask;
            if (configurationSupportTask.RunTaskSchedule == null || configurationSupportTask.RunTaskSchedule.TaskOperations == null || configurationSupportTask.RunTaskSchedule.TaskOperations.Count <= 0) return;
            foreach (var item in configurationSupportTask.RunTaskSchedule.TaskOperations)
            {
                var instance = this.ResolveAll<ITaskSchedule>();
                if (instance == null || instance.Length <= 0) return;
                var task = instance.FirstOrDefault(op => op.GetType().FullName == item.Type);
                if (task != null)
                {
                    task.TaskOperation = item;
                    _taskThreadManager.AddTask(task);
                }
            }
            _taskThreadManager.Start();
        }
        public override void ShutDownEngine()
        {
            if (_taskThreadManager != null)
                _taskThreadManager.Dispose();
            PluginManager.MarkAllPluginsAsUninstalled();
        }
    }
}
