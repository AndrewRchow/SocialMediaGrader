using System.Collections.Generic;
using Memeni.Models.Domain;
using Memeni.Models.Requests;

namespace Memeni.Services.Interfaces
{
    public interface ICompanyService
    {
        void Delete(int id);
        List<Company> Get();
        Company GetById(int id);
        int Insert(CompanyAddRequest model);
        void Update(CompanyUpdateRequest model);
    }
}