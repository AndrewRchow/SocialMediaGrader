using Memeni.Models.Requests;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Memeni.Services
{
    public interface IUserService
    {
        int Create(RegisterUserRequest model);
        int RegisterFacebook(RegisterUserFacebookRequest model);
        int AddFacebookId(int UserId, string FbId, string Picture);
        int RegisterGoogle(RegisterUserGoogleRequest model);
        int AddGoogleId(int UserId, string GoogleId);
        int RegisterLinkedIn(RegisterUserLinkedInRequest model);
        int AddLinkedInId(int UserId, string LinkedInId);
        bool LogIn(UserRequest model);
        bool LogInFree(UserFreeRequest model);
        int GetByEmail(string email);
        void CreateCode(int id, string guid);
        string GetGuidByEmail(int id);
        string ResetPwEntry(string email);
        void ResetPwConfirm(ResetPwRequest model);
    }
}
