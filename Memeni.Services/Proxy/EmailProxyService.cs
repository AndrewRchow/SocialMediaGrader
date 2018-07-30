using Memeni.Data;
using Memeni.Data.Providers;
using Memeni.Models.Requests;
using Memeni.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memeni.Services.Proxy
{
    // Proxy service for hangfire 
    public class EmailProxyService
    {
        private IEmailService _emailService;
        
        private void CreateEmailService()
        {
            string connString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            IDataProvider dataProvider = new SqlDataProvider(connString);
            IConfigService configService = new ConfigService(dataProvider);
            IErrorLogService errorLogService = new ErrorLogService(dataProvider);
            IFacebookService facebookService = new FacebookService();
            ITwitterService twitterService = new TwitterService(configService);
            _emailService = new EmailService(dataProvider, configService, errorLogService, facebookService, twitterService);
        }

        public EmailProxyService()
        {
            CreateEmailService();
        }

        public void ScheduleErrorEmail()
        {
            _emailService.ScheduleErrorEmail();
        }

        public void ScheduleReportEmail(ReportAddRequest model)
        {
            _emailService.ScheduleReportEmail(model);
        }
    }
}
