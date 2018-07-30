using Memeni.Models.Requests;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Memeni.Data.Providers;
using Memeni.Models.Domain;
using System.Data;
using Memeni.Data;
using Memeni.Services.Interfaces;

namespace Memeni.Services
{
    public class CompanyService : BaseService, ICompanyService
    {
        public CompanyService(IDataProvider dataProvider) : base(dataProvider)
        {
        }

        public int Insert(CompanyAddRequest model)
        {
            int id = 0;
            DataProvider.ExecuteNonQuery("dbo.Company_Insert"
               , inputParamMapper: delegate (SqlParameterCollection paramCollection)
               {
                   paramCollection.AddWithValue("@CompanyName", model.CompanyName);
                   paramCollection.AddWithValue("@CompanyUrl", model.CompanyUrl);
                   paramCollection.AddWithValue("@CompanyLogoUrl", model.CompanyLogoUrl);
                   paramCollection.AddWithValue("@ModifiedBy", model.ModifiedBy);

                   SqlParameter idParameter = new SqlParameter("@UserId", System.Data.SqlDbType.Int);
                   idParameter.Direction = System.Data.ParameterDirection.Output;

                   paramCollection.Add(idParameter);
               }
               , returnParameters: delegate (SqlParameterCollection param)
               {
                   Int32.TryParse(param["@UserId"].Value.ToString(), out id);
               });
            return id;
        }

        public List<Company> Get()
        {
            List<Company> list = new List<Company>();
            DataProvider.ExecuteCmd("dbo.Company_SelectAll"
               , inputParamMapper: null
               , singleRecordMapper: delegate (IDataReader reader, short set)
               {
                   Company singleItem = Mapper(reader);                   
                   list.Add(singleItem);
               });
            return list;
        }

        private Company Mapper(IDataReader reader)
        {
            Company singleItem = new Company();
            int startingIndex = 0; //startingOrdinal

            singleItem.UserId = reader.GetSafeInt32(startingIndex++);
            singleItem.CompanyName = reader.GetSafeString(startingIndex++);
            singleItem.CompanyUrl = reader.GetSafeString(startingIndex++);
            singleItem.CompanyLogoUrl = reader.GetSafeString(startingIndex++);
            singleItem.CreatedDate = reader.GetSafeDateTime(startingIndex++);
            singleItem.ModifiedDate = reader.GetSafeDateTime(startingIndex++);
            singleItem.ModifiedBy = reader.GetSafeString(startingIndex++);
            return singleItem;
        }

        public Company GetById(int id)
        {
            Company singleItem = null;
            DataProvider.ExecuteCmd("dbo.Company_SelectById"
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

        public void Update(CompanyUpdateRequest model)
        {
            DataProvider.ExecuteNonQuery("dbo.Company_Update"
               , inputParamMapper: delegate (SqlParameterCollection paramCollection)
               {
                   paramCollection.AddWithValue("@UserId", model.UserId);
                   paramCollection.AddWithValue("@CompanyName", model.CompanyName);
                   paramCollection.AddWithValue("@CompanyUrl", model.CompanyUrl);
                   paramCollection.AddWithValue("@CompanyLogoUrl", model.CompanyLogoUrl);
                   paramCollection.AddWithValue("@ModifiedBy", model.ModifiedBy);
               });
        }

        public void Delete(int id)
        {
            DataProvider.ExecuteNonQuery("dbo.Company_Delete"
               , inputParamMapper: delegate (SqlParameterCollection paramCollection)
               {
                   paramCollection.AddWithValue("@UserId", id);
               });                             
        }
    }
}
