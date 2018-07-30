using Memeni.Data;
using Memeni.Data.Providers;
using Memeni.Models.Domain.Profile;
using Memeni.Models.Requests.Profile;
using System.Data;
using System.Data.SqlClient;

namespace Memeni.Services
{
    public class ProfilePhoneService : BaseService, IProfilePhoneService
    {
        public ProfilePhoneService(IDataProvider dataProvider) : base(dataProvider)
        {
        }

        public void Insert(ProfilePhoneRequest model)
    {
        DataProvider.ExecuteNonQuery("dbo.Profile_InsertPhone"
           , inputParamMapper: delegate (SqlParameterCollection paramCollection)
           {
               paramCollection.AddWithValue("@UserId", model.UserId);
               paramCollection.AddWithValue("@ModifiedBy", model.ModifiedBy);
               paramCollection.AddWithValue("@CountryCode", model.CountryCode);
               paramCollection.AddWithValue("@PhoneNumber", model.PhoneNumber);
               paramCollection.AddWithValue("@Extension", model.Extension);
           });
    }

        public void Update(ProfilePhoneRequest model)
    {
        DataProvider.ExecuteNonQuery("dbo.Profile_UpdatePhone"
           , inputParamMapper: delegate (SqlParameterCollection paramCollection)
           {
               paramCollection.AddWithValue("@UserId", model.UserId);
               paramCollection.AddWithValue("@ModifiedBy", model.ModifiedBy);
               paramCollection.AddWithValue("@CountryCode", model.CountryCode);
               paramCollection.AddWithValue("@PhoneNumber", model.PhoneNumber);
               paramCollection.AddWithValue("@Extension", model.Extension);
           });
    }

        public ProfilePhone Get(int userId)
    {
        ProfilePhone singleItem = null;
        DataProvider.ExecuteCmd("dbo.Profile_SelectPhoneById"
           , inputParamMapper: delegate (SqlParameterCollection paramCollection)
           {
               paramCollection.AddWithValue("@UserId", userId);
           }
           , singleRecordMapper: delegate (IDataReader reader, short set)
           {
               singleItem = new ProfilePhone();
               int startingIndex = 0; //startingOrdinal
               singleItem.UserId = reader.GetSafeInt32(startingIndex++);
               singleItem.ModifiedBy = reader.GetSafeString(startingIndex++);
               singleItem.CountryCode = reader.GetSafeString(startingIndex++);
               singleItem.PhoneNumber = reader.GetSafeString(startingIndex++);
               singleItem.Extension = reader.GetSafeString(startingIndex++);
           });
        return singleItem;
    }
}
}
