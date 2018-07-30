using Memeni.Models.Domain;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Memeni.Data.Providers;
using System.Data;
using Memeni.Data;
using Memeni.Services.Interfaces;
using Memeni.Models.Requests;

namespace Memeni.Services
{
    public class PhoneService : BaseService, IPhoneService
    {
        public PhoneService(IDataProvider dataProvider) : base(dataProvider)
        {
        }

        public int Insert(PhoneAddRequest model)
        {
            int id = 0;
            DataProvider.ExecuteNonQuery("dbo.Phone_Insert"
               , inputParamMapper: delegate (SqlParameterCollection paramCollection)
               {
                   paramCollection.AddWithValue("@CountryCode", model.CountryCode);
                   paramCollection.AddWithValue("@PhoneNumber", model.PhoneNumber);
                   paramCollection.AddWithValue("@Extension", model.Extension);
                   paramCollection.AddWithValue("@ModifiedBy", model.ModifiedBy);

                   SqlParameter idParameter = new SqlParameter("@UserId", System.Data.SqlDbType.Int);
                   idParameter.Direction = System.Data.ParameterDirection.Output;

                   paramCollection.Add(idParameter);

                   }, returnParameters: delegate (SqlParameterCollection param)
                   {
                       Int32.TryParse(param["@UserId"].Value.ToString(), out id);
                   });

                   return id;
               }
               
        public List<Phone> GetAll()
        {
            List<Phone> list = new List<Phone>();
            DataProvider.ExecuteCmd("dbo.Phone_SelectAll"
               , inputParamMapper: null
               , singleRecordMapper: delegate (IDataReader reader, short set)
               {
                   Phone singleItem = Mapper(reader);
                   list.Add(singleItem);
               });
            return list;
        }

        private Phone Mapper(IDataReader reader)
        {
            Phone singleItem = new Phone();
            int startingIndex = 0; //startingOrdinal

            singleItem.UserId = reader.GetSafeInt32(startingIndex++);
            singleItem.CountryCode = reader.GetSafeString(startingIndex++);
            singleItem.PhoneNumber = reader.GetSafeString(startingIndex++);
            singleItem.Extension = reader.GetSafeString(startingIndex++);
            singleItem.CreatedDate = reader.GetSafeDateTime(startingIndex++);
            singleItem.ModifiedDate = reader.GetSafeDateTime(startingIndex++);
            singleItem.ModifiedBy = reader.GetSafeString(startingIndex++);
            return singleItem;
        }

        public Phone Get(int id)
        {
            Phone singleItem = null;
            DataProvider.ExecuteCmd("dbo.Phone_SelectById"
               , inputParamMapper: delegate (SqlParameterCollection paramCollection)
               {
                   paramCollection.AddWithValue("@UserId", id);
               }
               , singleRecordMapper: delegate (IDataReader reader, short set)
               {
                   singleItem = Mapper(reader);
               });
            return singleItem;
        }

        public void Update(PhoneUpdateRequest model)
        {
            DataProvider.ExecuteNonQuery("dbo.Phone_Update"
               , inputParamMapper: delegate (SqlParameterCollection paramCollection)
               {
                   paramCollection.AddWithValue("@UserId", model.UserId);
                   paramCollection.AddWithValue("@CountryCode", model.CountryCode);
                   paramCollection.AddWithValue("@PhoneNumber", model.PhoneNumber);
                   paramCollection.AddWithValue("@Extension", (Object)model.Extension ?? DBNull.Value);
                   paramCollection.AddWithValue("@ModifiedBy", model.ModifiedBy);
               });
        }

        public void Delete(int id)
        {
            DataProvider.ExecuteNonQuery("dbo.Phone_Delete"
               , inputParamMapper: delegate (SqlParameterCollection paramCollection)
               {
                   paramCollection.AddWithValue("@UserId", id);
               });
        }
    }
}
