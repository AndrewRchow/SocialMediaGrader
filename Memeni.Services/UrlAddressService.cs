using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Memeni.Data.Providers;
using System.Data.SqlClient;
using Memeni.Models.Requests;
using Memeni.Models.Domain;
using System.Data;
using Memeni.Data;
using Memeni.Services.Interfaces;

namespace Memeni.Services
{
    public class UrlAddressService : BaseService, IUrlAddressService
    {
        public UrlAddressService(IDataProvider dataProvider) : base(dataProvider)
        {
        }

        public List<UrlAddress> GetAll()
        {
            List<UrlAddress> list = new List<UrlAddress>();
            DataProvider.ExecuteCmd("dbo.UrlAddress_SelectAll"
               , inputParamMapper: null
               , singleRecordMapper: delegate (IDataReader reader, short set)
               {
                   UrlAddress singleItem = Mapper(reader);
                   list.Add(singleItem);
               });
            return list;
        }       

        public UrlAddress Get(int id)
        {
            UrlAddress singleItem = null;
            DataProvider.ExecuteCmd("dbo.UrlAddress_SelectById"
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

        private UrlAddress Mapper(IDataReader reader)
        {
            UrlAddress singleItem = new UrlAddress();
            int startingIndex = 0; //startingOrdinal
            singleItem.Id = reader.GetSafeInt32(startingIndex++);
            singleItem.Url = reader.GetSafeString(startingIndex++);
            singleItem.CreatedDate = reader.GetSafeDateTime(startingIndex++);
            singleItem.ModifiedDate = reader.GetSafeDateTime(startingIndex++);
            singleItem.ModifiedBy = reader.GetSafeString(startingIndex++);
            return singleItem;
        }

        public int Insert(UrlAddressAddRequest model)
        {
            int Id = 0;
            DataProvider.ExecuteNonQuery( "dbo.UrlAddress_Insert"
               , inputParamMapper: delegate (SqlParameterCollection paramCollection)
               {
                   paramCollection.AddWithValue("@Url", model.Url);
                   paramCollection.AddWithValue("@ModifiedBy", model.ModifiedBy);

                   SqlParameter idParameter = new SqlParameter("@Id", System.Data.SqlDbType.Int);
                   idParameter.Direction = System.Data.ParameterDirection.Output;

                   paramCollection.Add(idParameter);
               }, 
               returnParameters: delegate (SqlParameterCollection param)
               {
                   Int32.TryParse(param["@Id"].Value.ToString(), out Id);
               });
            return Id;
        }

        public void Update(UrlAddressUpdateRequest model)
        {        
            DataProvider.ExecuteNonQuery("dbo.UrlAddress_Update"
               , inputParamMapper: delegate (SqlParameterCollection paramCollection)
               {
                   paramCollection.AddWithValue("@Id", model.Id);
                   paramCollection.AddWithValue("@Url", model.Url);
                   paramCollection.AddWithValue("@ModifiedBy", model.ModifiedBy);                  
               });
        }

        public void Delete(int id)
        {
            DataProvider.ExecuteNonQuery("dbo.UrlAddress_Delete"
               , inputParamMapper: delegate (SqlParameterCollection paramCollection)
               {                
                   paramCollection.AddWithValue("@Id", id);                  
               });       
        }       
    }
}





