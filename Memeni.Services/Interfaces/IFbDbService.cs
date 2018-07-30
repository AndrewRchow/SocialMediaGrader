using Memeni.Models.Domain;
using Memeni.Models.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memeni.Services.Interfaces
{
    public interface IFbDbService
    {
        FbProfileAddRequest InsertProfile(FbProfileAddRequest model);
        void InsertDayFeed(FbWkPostIdData model);
        UserDashFb SelectById(int id);
        void InsertUserDashboard(UserDashAddRequest model);
        FbWkPostData SelectDashPosts(string id);
    }
}
