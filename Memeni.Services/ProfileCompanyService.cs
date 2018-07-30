using Memeni.Data.Providers;
using Memeni.Models.Requests.Profile;
using System.Data.SqlClient;
using Memeni.Models.Domain.Profile;
using System.Data;
using Memeni.Data;

namespace Memeni.Services
{
    public class ProfileCompanyService : BaseService, IProfileCompanyService
    {
        public ProfileCompanyService(IDataProvider dataProvider) : base(dataProvider)
        {
        }

        public void Insert(ProfileCompanyRequest model)
        {
            DataProvider.ExecuteNonQuery("dbo.Profile_InsertCompany"
               , inputParamMapper: delegate (SqlParameterCollection paramCollection)
               {
                   paramCollection.AddWithValue("@UserId", model.UserId);
                   paramCollection.AddWithValue("@ModifiedBy", model.ModifiedBy);
                   paramCollection.AddWithValue("@CompanyName", model.CompanyName);
                   paramCollection.AddWithValue("@CompanyUrl", model.CompanyUrl);
                   paramCollection.AddWithValue("@CompanyLogoUrl", model.CompanyLogoUrl);
               });
        }

        public void Update(ProfileCompanyRequest model)
        {
            DataProvider.ExecuteNonQuery("dbo.Profile_UpdateCompany"
               , inputParamMapper: delegate (SqlParameterCollection paramCollection)
               {
                   paramCollection.AddWithValue("@UserId", model.UserId);
                   paramCollection.AddWithValue("@ModifiedBy", model.ModifiedBy);
                   paramCollection.AddWithValue("@CompanyName", model.CompanyName);
                   paramCollection.AddWithValue("@CompanyUrl", model.CompanyUrl);
                   paramCollection.AddWithValue("@CompanyLogoUrl", model.CompanyLogoUrl);
               });
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
    }
}
