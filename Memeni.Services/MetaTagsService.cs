using Memeni.Data;
using Memeni.Data.Providers;
using Memeni.Models.Domain;
using Memeni.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memeni.Services
{
    public class MetaTagsService : BaseService, IMetaTagsService
    {
        public MetaTagsService(IDataProvider dataProvider) : base(dataProvider)
        {
        }

        public List<MetaTags> Get()
        {
            List<MetaTags> list = new List<MetaTags>();
            DataProvider.ExecuteCmd("dbo.MetaMT_SelectAll"
                , inputParamMapper: null
                , singleRecordMapper: delegate (IDataReader reader, short set)
                {
                    MetaTags singleItem = Mapper(reader);
                    list.Add(singleItem);
                });
            return list;
        }

        private MetaTags Mapper(IDataReader reader)
        {
            MetaTags singleItem = new MetaTags();
            int startingIndex = 0;
            singleItem.Id = reader.GetSafeInt32(startingIndex++);
            singleItem.Name = reader.GetSafeString(startingIndex++);
            singleItem.Description = reader.GetSafeString(startingIndex++);
            singleItem.Example = reader.GetSafeString(startingIndex++);
            singleItem.Template = reader.GetSafeString(startingIndex++);
            return singleItem;
        }

    }
}
