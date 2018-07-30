using Memeni.Models.Domain.Profile;
using Memeni.Models.Requests.Profile;

namespace Memeni.Services
{
    public interface IProfileCompanyService
    {
        ProfileCompany Get(int userId);
        void Insert(ProfileCompanyRequest model);
        void Update(ProfileCompanyRequest model);
    }
}