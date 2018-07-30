using Memeni.Models.Domain;
using Memeni.Models.Requests;
using System.Collections.Generic;

namespace Memeni.Services
{
    public interface IFaqCategoryService
    {
        int Insert(FaqCategoryAddRequest model);
        List<FaqCategory> Get();
        FaqCategory GetById(int Id);
        void Update(FaqCategoryUpdateRequest model);
        void Delete(int Id);
    }
}