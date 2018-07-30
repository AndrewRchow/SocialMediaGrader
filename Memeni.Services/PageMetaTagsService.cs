using Memeni.Data;
using Memeni.Data.Providers;
using Memeni.Models.Domain;
using Memeni.Models.Requests;
using Memeni.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memeni.Services
{
    public class PageMetaTagsService : BaseService, IPageMetaTagsService
    {
        public PageMetaTagsService(IDataProvider dataProvider) : base(dataProvider)
        {
        }

        public List<PageMetaTags> Get()
        {
            List<PageMetaTags> list = new List<PageMetaTags>();
            DataProvider.ExecuteCmd("dbo.PageMetaTags_SelectAll"
                , inputParamMapper: null
                , singleRecordMapper: delegate (IDataReader reader, short set)
                {
                    PageMetaTags singleItem = Mapper(reader);
                    list.Add(singleItem);
                });
            return list;
        }

        public List<PageMetaTags> GetByUrl(string pageUrl)
        {
            List<PageMetaTags> list = new List<PageMetaTags>();
            DataProvider.ExecuteCmd("dbo.PageMetaTags_SelectByUrl"
                , inputParamMapper: delegate (SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@PageUrl", pageUrl);
                }
                , singleRecordMapper: delegate (IDataReader reader, short set)
                {
                    PageMetaTags singleItem = Mapper(reader);
                    list.Add(singleItem);
                });
            return list;
        }

        public List<PageMetaTags> GetByUrlId(int id)
        {
            List<PageMetaTags> list = new List<PageMetaTags>();
            DataProvider.ExecuteCmd("dbo.PageMetaTags_SelectByUrlId"
                , inputParamMapper: delegate (SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@Id", id);
                }
                , singleRecordMapper: delegate (IDataReader reader, short set)
                {
                    PageMetaTags singleItem = Mapper(reader);
                    list.Add(singleItem);
                });
            return list;
        }

        public PageMetaTags Get(int id)
        {
            PageMetaTags singleItem = null;
            DataProvider.ExecuteCmd("dbo.PageMetaTags_SelectById"
                , inputParamMapper: delegate (SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@Id", id);
                }
                , singleRecordMapper: delegate (IDataReader reader, short set)
                {
                    singleItem = Mapper(reader);
                });
            return singleItem;
        }

        private PageMetaTags Mapper (IDataReader reader)
        {
            PageMetaTags singleItem = new PageMetaTags();
            int startingIndex = 0;
            singleItem.Id = reader.GetSafeInt32(startingIndex++);
            singleItem.PageUrl = reader.GetSafeString(startingIndex++);
            singleItem.PageName = reader.GetSafeString(startingIndex++);
            singleItem.PageOwner = reader.GetSafeString(startingIndex++);
            singleItem.MetaName = reader.GetSafeString(startingIndex++);
            singleItem.MetaDescription = reader.GetSafeString(startingIndex++);
            singleItem.MetaExample = reader.GetSafeString(startingIndex++);
            singleItem.MetaTemplate = reader.GetSafeString(startingIndex++);
            singleItem.Value = reader.GetSafeString(startingIndex++);
            return singleItem;
        }

        public void Insert(PageMetaTagsAddRequest model)
        {
            DataProvider.ExecuteNonQuery("dbo.PageMetaTags_Insert"
                , inputParamMapper: delegate (SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@OwnerTypeId", model.OwnerTypeId);
                });
        }

        public void UpdateValue(PageMetaTagsUpdateValueRequest model)
        {
            DataProvider.ExecuteNonQuery("dbo.PageMetaTags_UpdateValue"
                , inputParamMapper: delegate (SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@Id", model.Id);
                    paramCollection.AddWithValue("@Value", model.Value);
                });
        }

        public void Delete(int id)
        {
            DataProvider.ExecuteNonQuery("dbo.PageMetaTags_Delete"
                , inputParamMapper: delegate (SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@OwnerTypeId", id);
                });
        }
    }
}
