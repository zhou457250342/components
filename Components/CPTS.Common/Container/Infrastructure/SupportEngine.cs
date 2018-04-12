using Autofac;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPTS.Common.Container.Infrastructure
{
    public abstract class SupportEngine : IEngine
    {
        #region field
        protected IObjectContainer _objectContainer;
        #endregion

        public T Resolve<T>() where T : class
        {
            try
            {
                return ContainerManager.Resolve<T>();
            }
            catch (Exception)
            {
                return default(T);
            }
        }
        public object Resolve(Type type)
        {
            return ContainerManager.Resolve(type);
        }
        public T[] ResolveAll<T>()
        {
            return ContainerManager.ResolveAll<T>();
        }
        public IObjectContainer ContainerManager
        {
            get { return this._objectContainer; }
        }
        public abstract void Initialize(IConfigurationSupport config);
        public abstract void ShutDownEngine();
        public virtual void RegisterDependencies(IConfigurationSupport supportConfig)
        {
            var typeFinder = new TypeFinder();
            var builder = new ContainerBuilder();
            var container = builder.Build();
            _objectContainer = new ObjectContainer(container);

            builder = new ContainerBuilder();
            var drTypes = typeFinder.FindClassesOfType<IDependencyRegistrar>();
            var drInstances = new List<IDependencyRegistrar>();
            foreach (var drType in drTypes)
                drInstances.Add((IDependencyRegistrar)Activator.CreateInstance(drType));
            //排序注册
            drInstances = drInstances.AsQueryable().OrderBy(t => t.Order).ToList();
            foreach (var dependencyRegistrar in drInstances)
                dependencyRegistrar.Register(builder, typeFinder, supportConfig);

            builder.Update(container);

            EngineCommon.UseContainer(() => { return _objectContainer; });
        }
    }
}
