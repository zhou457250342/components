using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPTS.Common.Container
{
    public interface IObjectContainer
    {
        IContainer Container { get; }

        /// <summary>
        /// Resolve
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="key">key</param>
        /// <param name="scope">Scope; pass null to automatically resolve the current scope</param>
        /// <returns>Resolved service</returns>
        T Resolve<T>(string key = "", ILifetimeScope scope = null) where T : class;
        /// <summary>
        /// Resolve
        /// </summary>
        /// <param name="type">Type</param>
        /// <param name="scope">Scope; pass null to automatically resolve the current scope</param>
        /// <returns>Resolved service</returns>
        object Resolve(Type type, ILifetimeScope scope = null);
        /// <summary>
        /// Resolve all
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="key">key</param>
        /// <param name="scope">Scope; pass null to automatically resolve the current scope</param>
        /// <returns>Resolved services</returns>
        T[] ResolveAll<T>(string key = "", ILifetimeScope scope = null);
        /// <summary>
        /// Resolve unregistered service
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="scope">Scope; pass null to automatically resolve the current scope</param>
        /// <returns>Resolved service</returns>
        T ResolveUnregistered<T>(ILifetimeScope scope = null) where T : class;
        bool TryResolve(Type serviceType, ILifetimeScope scope, out object instance);
        object ResolveUnregistered(Type type, ILifetimeScope scope = null);
    }
}
