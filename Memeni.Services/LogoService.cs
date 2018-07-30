using Memeni.Data;
using Memeni.Data.Providers;
using Memeni.Models.Domain.Profile;
using Memeni.Models.Requests;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Memeni.Services
{
    public class LogoService : BaseService, ILogoService
    {
        public LogoService(IDataProvider dataProvider) : base(dataProvider)
        {
        }

        public ProfileCompany Get(int userId)
        {
            ProfileCompany singleItem = null;
            DataProvider.ExecuteCmd("dbo.Profile_SelectCompanyById"
               , inputParamMapper: delegate (SqlParameterCollection paramCollection)
               {
                   paramCollection.AddWithValue("@UserId", userId);
               }
               , singleRecordMapper: delegate (IDataReader reader, short set)
               {
                   singleItem = new ProfileCompany();
                   int startingIndex = 0; //startingOrdinal
                   singleItem.UserId = reader.GetSafeInt32(startingIndex++);
                   singleItem.ModifiedBy = reader.GetSafeString(startingIndex++);
                   singleItem.CompanyName = reader.GetSafeString(startingIndex++);
                   singleItem.CompanyUrl = reader.GetSafeString(startingIndex++);
                   singleItem.CompanyLogoUrl = reader.GetSafeString(startingIndex++);
               });
            return singleItem;
        }

        public int Insert(LogoAddRequest model)
        {
            int FileId = 0;
            DataProvider.ExecuteNonQuery("dbo.FileStorage_Insert"
               , inputParamMapper: delegate (SqlParameterCollection paramCollection)
               {
                   paramCollection.AddWithValue("@FileName", model.FileName);
                   paramCollection.AddWithValue("@Size", model.Size);                 
                   paramCollection.AddWithValue("@ContentType", model.ContentType);
                   paramCollection.AddWithValue("@SystemFileName", model.ServerFileName);

                   SqlParameter idParameter = new SqlParameter("@FileId", System.Data.SqlDbType.Int);
                   idParameter.Direction = System.Data.ParameterDirection.Output;

                   paramCollection.Add(idParameter);
               }
               , returnParameters: delegate (SqlParameterCollection param)
               {
                   Int32.TryParse(param["@FileId"].Value.ToString(), out FileId);
               });
            return FileId;
        }

        public void InsertIds(CompanyFileIdsRequest model)
        {
            DataProvider.ExecuteNonQuery("dbo.Company_FileStorage_Insert"
               , inputParamMapper: delegate (SqlParameterCollection paramCollection)
               {
                   paramCollection.AddWithValue("@UserId", model.UserId);
                   paramCollection.AddWithValue("@FileId", model.FileId);                   
               });
        }

        public void UpdateLogo(LogoUpdateRequest model)
        {
            DataProvider.ExecuteNonQuery("dbo.FileStorage_Update"
                , inputParamMapper: delegate (SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@FileId", model.FileId);
                    paramCollection.AddWithValue("@FileName", model.FileName);
                    paramCollection.AddWithValue("@Size", model.Size);
                    paramCollection.AddWithValue("@ContentType", model.ContentType);
                    paramCollection.AddWithValue("@SystemFileName", model.ServerFileName);
                });
        }

        public void UpdateIds(CompanyFileIdsRequest model)
        {
            DataProvider.ExecuteNonQuery("dbo.Company_FileStorage_Update"
               , inputParamMapper: delegate (SqlParameterCollection paramCollection)
               {
                   paramCollection.AddWithValue("@UserId", model.UserId);
                   paramCollection.AddWithValue("@FileId", model.FileId);
               });
        }
    }
}
