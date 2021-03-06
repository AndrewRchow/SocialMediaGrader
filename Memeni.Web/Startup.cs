using Hangfire;
using Microsoft.Owin;
using Owin;


[assembly: OwinStartupAttribute(typeof(Memeni.Web.Startup))]
namespace Memeni.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888
            GlobalConfiguration.Configuration
                .UseSqlServerStorage("DefaultConnection");

            //Do not remove. Used for Auth
            ConfigureAuth(app);

            //Hangfire Setup
            string hangfireSwitch = System.Configuration.ConfigurationManager.AppSettings["HangfireSwitch"];
            bool isHangFireOn = bool.Parse(hangfireSwitch);
            if (isHangFireOn)
            {
                app.UseHangfireDashboard();
                app.UseHangfireServer();
            } 
        }
    }
}
