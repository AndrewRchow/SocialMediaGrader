using Memeni.Models.Requests;
using Memeni.Models.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using Memeni.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Memeni.Data.Providers;
using Memeni.Services.Interfaces;

namespace Memeni.Services
{
    public class HelpCategoriesService : BaseService, IHelpCategoriesService
    {
        public int Insert(HelpCategoriesAddRequest model)
        {
            int Id = 0;
            DataProvider.ExecuteNonQuery("dbo.HelpCategories_Insert"
                , inputParamMapper: delegate (SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@Name", model.Name);
                    paramCollection.AddWithValue("@UrlPath", model.UrlPath);
                    paramCollection.AddWithValue("@ModifiedBy", model.ModifiedBy);

                    SqlParameter idParameter = new SqlParameter("@Id", System.Data.SqlDbType.Int);
                    idParameter.Direction = System.Data.ParameterDirection.Output;

                    paramCollection.Add(idParameter);
                }
                , returnParameters: delegate (SqlParameterCollection param)
                {
                    Int32.TryParse(param["@Id"].Value.ToString(), out Id);
                });
            return Id;
        }
        public List<HelpCategories> Get()
        {
            List<HelpCategories> list = new List<HelpCategories>();
            DataProvider.ExecuteCmd("dbo.HelpCategories_SelectAll"
                , inputParamMapper: null
                , singleRecordMapper: delegate (IDataReader reader, short set)
                {
                    HelpCategories singleItem = Mapper(reader);
                    list.Add(singleItem);
                });
            return list;
        }
        public HelpCategories Get(int id)
        {
            HelpCategories singleItem = null;
            DataProvider.ExecuteCmd("dbo.HelpCategories_SelectById"
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
        public void Update(HelpCategoriesUpdateRequest model)
        {
            DataProvider.ExecuteNonQuery("dbo.HelpCategories_Update"
                , inputParamMapper: delegate (SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@Id", model.Id);
                    paramCollection.AddWithValue("@Name", model.Name);
                    paramCollection.AddWithValue("@UrlPath", model.UrlPath);
                    paramCollection.AddWithValue("@ModifiedBy", model.ModifiedBy);
                });
        }
        public void Delete(int id)
        {
            DataProvider.ExecuteNonQuery("dbo.HelpCategories_Delete"
            , inputParamMapper: delegate (SqlParameterCollection paramCollection)
            {
                paramCollection.AddWithValue("@Id", id);
            });
        }

        private HelpCategories Mapper(IDataReader reader)
        {
            HelpCategories singleItem = new HelpCategories();
            int index = 0;

            singleItem.Id = reader.GetSafeInt32(index++);
            singleItem.Name = reader.GetSafeString(index++);
            singleItem.UrlPath = reader.GetSafeString(index++);
            singleItem.CreatedDate = reader.GetSafeDateTime(index++);
            singleItem.ModifiedDate = reader.GetSafeDateTime(index++);
            singleItem.ModifiedBy = reader.GetSafeString(index++);

            return singleItem;
        }
    }
}
