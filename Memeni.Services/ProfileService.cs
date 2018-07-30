using Memeni.Data;
using Memeni.Data.Providers;
using Memeni.Models.Domain.Profile;
using System.Data;
using System.Data.SqlClient;

namespace Memeni.Services
{
    public class ProfileService : BaseService, IProfileService
    {
        public ProfileService(IDataProvider dataProvider) : base(dataProvider)
        {
        }

        public UserProfile GetById(int id)
        {
            UserProfile singleItem = null;
            DataProvider.ExecuteCmd("dbo.Profile_SelectById"
               , inputParamMapper: delegate (SqlParameterCollection paramCollection)
               {
                   paramCollection.AddWithValue("@Id", id);
               }
               , singleRecordMapper: delegate (IDataReader reader, short set)
               {
                   singleItem = new UserProfile();
                   int startingIndex = 0; //startingOrdinal
                   singleItem.Id = reader.GetSafeInt32(startingIndex++);
                   singleItem.Email = reader.GetSafeString(startingIndex++);
                   singleItem.DateCreated = reader.GetSafeDateTime(startingIndex++);

                   singleItem.ModifiedBy = reader.GetSafeString(startingIndex++);
                   singleItem.PersonId = reader.GetSafeInt32(startingIndex++);
                   singleItem.FirstName = reader.GetSafeString(startingIndex++);
                   singleItem.MiddleName = reader.GetSafeString(startingIndex++);
                   singleItem.LastName = reader.GetSafeString(startingIndex++);

                   singleItem.CompanyId = reader.GetSafeInt32(startingIndex++);
                   singleItem.CompanyName = reader.GetSafeString(startingIndex++);
                   singleItem.CompanyUrl = reader.GetSafeString(startingIndex++);
                   singleItem.CompanyLogoUrl = reader.GetSafeString(startingIndex++);

                   singleItem.PhoneId = reader.GetSafeInt32(startingIndex++);
                   singleItem.CountryCode = reader.GetSafeString(startingIndex++);
                   singleItem.PhoneNumber = reader.GetSafeString(startingIndex++);
                   singleItem.Extension = reader.GetSafeString(startingIndex++);

                   singleItem.CoLogoId = reader.GetSafeInt32(startingIndex++);
                   singleItem.FileId = reader.GetSafeInt32(startingIndex++);

                   singleItem.SystemFileName = reader.GetSafeString(startingIndex++);
                   singleItem.Picture = reader.GetSafeString(startingIndex++);
               });
            return singleItem;
        }
    }
}
