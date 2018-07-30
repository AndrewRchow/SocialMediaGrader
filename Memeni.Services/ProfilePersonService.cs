using Memeni.Data.Providers;
using Memeni.Models.Requests.Profile;
using System.Data.SqlClient;
using Memeni.Models.Domain.Profile;
using System.Data;
using Memeni.Data;

namespace Memeni.Services
{
    public class ProfilePersonService : BaseService, IProfilePersonService
    {
        public ProfilePersonService(IDataProvider dataProvider) : base(dataProvider)
        {           
        }

        public void Insert(ProfilePersonRequest model)
        {
            DataProvider.ExecuteNonQuery("dbo.Profile_InsertPerson"
               , inputParamMapper: delegate (SqlParameterCollection paramCollection)
               {
                   paramCollection.AddWithValue("@UserId", model.UserId);
                   paramCollection.AddWithValue("@ModifiedBy", model.ModifiedBy);
                   paramCollection.AddWithValue("@FirstName", model.FirstName);
                   paramCollection.AddWithValue("@MiddleName", model.MiddleName);
                   paramCollection.AddWithValue("@LastName", model.LastName);
               });
        }

        public void Update(ProfilePersonRequest model)
        {
            DataProvider.ExecuteNonQuery("dbo.Profile_UpdatePerson"
               , inputParamMapper: delegate (SqlParameterCollection paramCollection)
               {
                   paramCollection.AddWithValue("@UserId", model.UserId);
                   paramCollection.AddWithValue("@ModifiedBy", model.ModifiedBy);
                   paramCollection.AddWithValue("@FirstName", model.FirstName);
                   paramCollection.AddWithValue("@MiddleName", model.MiddleName);
                   paramCollection.AddWithValue("@LastName", model.LastName);
               });
        }

        public ProfilePerson Get(int userId)
        {
            ProfilePerson singleItem = null;
            DataProvider.ExecuteCmd("dbo.Profile_SelectPersonById"
               , inputParamMapper: delegate (SqlParameterCollection paramCollection)
               {
                   paramCollection.AddWithValue("@UserId", userId);
               }
               , singleRecordMapper: delegate (IDataReader reader, short set)
               {
                   singleItem = new ProfilePerson();
                   int startingIndex = 0; //startingOrdinal
                   singleItem.UserId = reader.GetSafeInt32(startingIndex++);
                   singleItem.ModifiedBy = reader.GetSafeString(startingIndex++);
                   singleItem.FirstName = reader.GetSafeString(startingIndex++);
                   singleItem.MiddleName = reader.GetSafeString(startingIndex++);
                   singleItem.LastName = reader.GetSafeString(startingIndex++);
               });
            return singleItem;
        }
    }
}
