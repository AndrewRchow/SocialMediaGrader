using System.Collections.Generic;
using Memeni.Models.Domain;
using Memeni.Models.Requests;
using Memeni.Models.Domain.Profile;

namespace Memeni.Services
{
    public interface ILogoService
    {
        int Insert(LogoAddRequest model);
        void InsertIds(CompanyFileIdsRequest model);
        void UpdateLogo(LogoUpdateRequest model);
        void UpdateIds(CompanyFileIdsRequest model);
    }
}