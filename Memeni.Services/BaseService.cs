using Memeni.Data.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Memeni.Data;
using System.Data;

namespace Memeni.Services
{
    public abstract class BaseService
    {
        public IDataProvider DataProvider { get; set; }

        public BaseService(IDataProvider dataProvider)
        {
            this.DataProvider = dataProvider;
        }

        public BaseService()
        {
            this.DataProvider = new SqlDataProvider("Server=sabiodb05.cdzsexgreyji.us-west-2.rds.amazonaws.com;Database=Memeni;User Id=C37_User;Password=Sabiopass1!;");
        }



        
    }
}
