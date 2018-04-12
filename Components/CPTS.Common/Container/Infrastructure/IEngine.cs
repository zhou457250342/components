using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPTS.Common.Container.Infrastructure
{
    public interface IEngine
    {
        T Resolve<T>() where T : class;
        object Resolve(Type type);
        void Initialize(IConfigurationSupport config);
        void ShutDownEngine();
        IObjectContainer ContainerManager { get; }
    }
}
