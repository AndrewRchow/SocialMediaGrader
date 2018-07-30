using Memeni.Models.Domain.Profile;

namespace Memeni.Services
{
    public interface IProfileService
    {
        UserProfile GetById(int id);
    }
}