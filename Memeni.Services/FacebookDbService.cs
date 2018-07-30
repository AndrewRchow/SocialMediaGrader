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
    public class FacebookDbService : BaseService, IFbDbService
    {
        public FacebookDbService(IDataProvider dataProvider) : base(dataProvider) { }

        public FbProfileAddRequest InsertProfile(FbProfileAddRequest model)
        {
            DataProvider.ExecuteNonQuery(storedProc: "dbo.Fb_Profile_Insert", inputParamMapper: delegate (SqlParameterCollection paramCollection)
            {
                paramCollection.AddWithValue("@Id", model.Id);
                paramCollection.AddWithValue("@Name", model.Name);
                paramCollection.AddWithValue("@Is_Verified", model.Is_Verified);
                paramCollection.AddWithValue("@Picture", model.Picture.Data.Url);
                paramCollection.AddWithValue("@Cover", model.Cover.Source);
                paramCollection.AddWithValue("@Total_Likes", model.Engagement.Count);
                paramCollection.AddWithValue("@Talking_Count", model.Talking_About_Count);
            });
            return model;
        }
        public void InsertDayFeed(FbWkPostIdData model)
        {
            DataProvider.ExecuteNonQuery(storedProc: "dbo.Fb_Data_Insert", inputParamMapper: delegate (SqlParameterCollection paramCollection)
            {
                paramCollection.AddWithValue("@Id", model.Id);
                paramCollection.AddWithValue("@Date", model.Date);
                paramCollection.AddWithValue("@DateString", model.DateString);
                paramCollection.AddWithValue("@Posts", model.Posts);
                paramCollection.AddWithValue("@Reactions", model.Reactions);
                paramCollection.AddWithValue("@Likes", model.Likes);
                paramCollection.AddWithValue("@Shares", model.Shares);
                paramCollection.AddWithValue("@Comments", model.Comments);
            });
        }
        public UserDashFb SelectById(int id)
        {
            UserDashFb singleItem = new UserDashFb();
            singleItem.Id = id;
            this.DataProvider.ExecuteCmd("dbo.UserDashboard_SelectById"
               , inputParamMapper: delegate (SqlParameterCollection paramCollection)
               {
                   paramCollection.AddWithValue("@Id", id);
               }
               , singleRecordMapper: delegate (IDataReader reader, short set)
               {
                   int startingIndex = 0; //startingOrdinal

                   singleItem.Facebook = reader.GetSafeString(startingIndex++);
                   singleItem.Twitter = reader.GetSafeString(startingIndex++);
                   singleItem.WklyFb = reader.GetSafeBool(startingIndex++);
                   singleItem.WklyTwt = reader.GetSafeBool(startingIndex++);
               });
            return singleItem;
        }
        public void InsertUserDashboard(UserDashAddRequest model)
        {
            DataProvider.ExecuteNonQuery("dbo.UserDashboard_Insert"
                , inputParamMapper: delegate (SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@Id", model.Id);
                    paramCollection.AddWithValue("@Facebook", model.Facebook);
                    paramCollection.AddWithValue("@Twitter", model.Twitter);
                    paramCollection.AddWithValue("@WeeklyFB", model.WeeklyFB);
                    paramCollection.AddWithValue("@WeeklyTwitter", model.WeeklyTwitter);
                }
            );
        }
        public FbWkPostData SelectDashPosts(string id)
        {
            FbWkPostData item = null;

            this.DataProvider.ExecuteCmd("dbo.UserDashboard_SelectById"
               , inputParamMapper: delegate (SqlParameterCollection paramCollection)
               {
                   paramCollection.AddWithValue("@Id", id);
               }
               , singleRecordMapper: delegate (IDataReader reader, short set)
               {
                   int startingIndex = 0; //startingOrdinal

                   item.DateString = reader.GetSafeString(startingIndex++);
                   item.Date = reader.GetSafeDateTime(startingIndex++);
                   item.Shares = reader.GetSafeInt32(startingIndex++);
                   item.Likes = reader.GetSafeInt32(startingIndex++);
                   item.Comments = reader.GetSafeInt32(startingIndex++);
                   item.Reactions = reader.GetSafeInt32(startingIndex++);
                   item.Posts = reader.GetSafeInt32(startingIndex++);
               });
            return item;
        }
    }
}
