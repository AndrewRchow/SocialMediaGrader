using Memeni.Models.Domain.Profile;
using Memeni.Models.Requests.Profile;

namespace Memeni.Services
{
    public interface IProfilePersonService
    {
        ProfilePerson Get(int userId);
        void Insert(ProfilePersonRequest model);
        void Update(ProfilePersonRequest model);
    }
}