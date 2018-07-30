using Memeni.Data;
using Memeni.Data.Providers;
using Memeni.Services;
using Memeni.Services.Interfaces;
using Memeni.Test.Core;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memeni.Test.Dependency
{
    public class UnityRegistrationModule : IContainerRegistrationModule<IUnityContainer>
    {
        // Register dependencies in unity container
        public void Register(IUnityContainer container)
        {
            // register service locator
            container.RegisterType<IServiceLocator, CustomUnityServiceLocator>();

            // register services
            container.RegisterType<IConfigService, ConfigService>();
            container.RegisterType<ITncService, TncService>();
            container.RegisterType<IFaqService, FaqService>();
            container.RegisterType<IFaqCategoryService, FaqCategoryService>();

            container.RegisterType<IDataProvider, SqlDataProvider>(
                new InjectionConstructor(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString));

        }
    }
}
