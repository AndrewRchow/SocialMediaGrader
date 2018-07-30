using Memeni.Models.Domain;
using Memeni.Models.Requests;
using System.Collections.Generic;

namespace Memeni.Services.Interfaces
{
    public interface IPrivacyService
    {
        Privacy Get(int id);
        List<Privacy> Get();
        int Insert(PrivacyAddRequest model);
        void Update(PrivacyUpdateRequest model);
        void Update2(List<Privacy> model);
        void Delete(int id);
    }
}