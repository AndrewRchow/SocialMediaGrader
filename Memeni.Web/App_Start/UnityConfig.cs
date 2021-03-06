using Microsoft.Practices.Unity;
using Memeni.Data;
using Memeni.Data.Providers;
using Memeni.Services;
using Memeni.Services.Cryptography;
using Memeni.Web.Core.Services;
using System.Configuration;
using System.Security.Principal;
using System.Threading;
using System.Web.Http;
using System.Web.Mvc;
using Unity.WebApi;
using Memeni.Services.Interfaces;
using System.Web;
using Memeni.Services.Security;

namespace Memeni.Web
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();
            container.RegisterType<ISMEService, SMEService>();
            container.RegisterType<IPeopleService, PeopleService>();
            container.RegisterType<ITncService, TncService>();
            container.RegisterType<IPrivacyService, PrivacyService>();
            container.RegisterType<IUrlAddressService, UrlAddressService>();
            container.RegisterType<IAdminUserService, AdminUserService>();
            container.RegisterType<IProfileService, ProfileService>();
            container.RegisterType<IProfilePersonService, ProfilePersonService>();
            container.RegisterType<IProfileCompanyService, ProfileCompanyService>();
            container.RegisterType<IProfilePhoneService, ProfilePhoneService>();
            container.RegisterType<IHelpCategoriesService, HelpCategoriesService>();
            container.RegisterType<IHelpService, HelpService>();
            container.RegisterType<IUrlAddressService, UrlAddressService>();           
            container.RegisterType<IErrorLogService, ErrorLogService>();
            container.RegisterType<ICompanyService, CompanyService>();
            container.RegisterType<IPhoneService, PhoneService>();
            container.RegisterType<IConfigService, ConfigService>();
            container.RegisterType<IConfigCategoryService, ConfigCategoryService>();
            container.RegisterType<IConfigDataService, ConfigDataService>();
            container.RegisterType<IEmailService, EmailService>();
            container.RegisterType<IPageMetaTagsService, PageMetaTagsService>();
            container.RegisterType<IMetaTagsService, MetaTagsService>();
            container.RegisterType<ILogoService, LogoService>();
            container.RegisterType<IMetaUrlService, MetaUrlService>();
            container.RegisterType<IFacebookService, FacebookService>();
            container.RegisterType<IWidgetService, WidgetService>();
            container.RegisterType<IFbDbService, FacebookDbService>();
            container.RegisterType<ISalesforceService, SalesforceService>();
            container.RegisterType<ITwitterService, TwitterService>();
            container.RegisterType<IFaqService, FaqService>();
            container.RegisterType<IFaqCategoryService, FaqCategoryService>();
            container.RegisterType<IAnonTrackingService, AnonTrackingService>();

            //this should be per request
            container.RegisterType<IAuthenticationService, OwinAuthenticationService>();

            container.RegisterType<ICryptographyService, Base64StringCryptographyService>(new ContainerControlledLifetimeManager());


            container.RegisterType<IDataProvider, SqlDataProvider>(
                new InjectionConstructor(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString));

            container.RegisterType<IPrincipal>(new TransientLifetimeManager(),
                    new InjectionFactory(con => HttpContext.Current.User));

            container.RegisterType<IUserService, UserService>(new ContainerControlledLifetimeManager());
     
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);

            DependencyResolver.SetResolver(new Unity.Mvc5.UnityDependencyResolver(container));
        }
    }
}
