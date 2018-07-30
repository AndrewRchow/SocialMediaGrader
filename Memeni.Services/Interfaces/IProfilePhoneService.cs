using Memeni.Models.Domain.Profile;
using Memeni.Models.Requests.Profile;

namespace Memeni.Services
{
    public interface IProfilePhoneService
    {
        ProfilePhone Get(int userId);
        void Insert(ProfilePhoneRequest model);
        void Update(ProfilePhoneRequest model);
    }
}