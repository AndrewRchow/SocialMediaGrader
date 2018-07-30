using Memeni.Models.Domain;
using Memeni.Models.Requests;
using Memeni.Models.ViewModels;
using System.Collections.Generic;

namespace Memeni.Services
{
    public interface IFaqService
    {
        List<FaqIndexModel> Get();
        Faq GetById(int Id);
        int Insert(FaqAddRequest model);
        void Update(FaqUpdateRequest model);
        void Delete(int Id);
    }
}