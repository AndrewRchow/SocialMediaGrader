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
    public class SalesforceProxyService
    {
        private ISalesforceService _SalesforceService;

        private void CreateProxyService()
        {
            string connString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            IDataProvider dataProvider = new SqlDataProvider(connString);
            IConfigService configService = new ConfigService(dataProvider);
            _SalesforceService = new SalesforceService(dataProvider, configService);
        }

        public SalesforceProxyService()
        {
            CreateProxyService();
        }

        public void InsertFB(SalesforceAddRequest model)
        {
            _SalesforceService.InsertFB(model);
        }

        public void InsertTWITT(SalesforceAddRequest model)
        {
            _SalesforceService.InsertTWITT(model);
        }

        public void InsertWidget(SalesforceAddRequest model)
        {
            _SalesforceService.InsertWidget(model);
        }

        public void Register(SalesforceUpdateRequest model)
        {
            _SalesforceService.Register(model);
        }
    }
}
