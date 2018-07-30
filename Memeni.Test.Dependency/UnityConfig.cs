using Memeni.Test.Core;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memeni.Test.Dependency
{
    public class UnityConfig
    {
        private static readonly Lazy<IUnityContainer> Container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            return container;
        });

        public static IUnityContainer GetUnityContainer()
        {
            return Container.Value;
        }

        public static void RegisterTypes(IUnityContainer container)
        {
            var registrationModuleAssemblyName = System.Configuration.ConfigurationManager.AppSettings["UnityRegistrationModule"];
            var type = Type.GetType(registrationModuleAssemblyName);
            var module = (IContainerRegistrationModule<IUnityContainer>)Activator.CreateInstance(type);
            module.Register(container);
        }
    }
}
