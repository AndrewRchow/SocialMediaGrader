using System.Collections.Generic;
using Memeni.Models.Domain;
using Memeni.Models.Requests;

namespace Memeni.Services.Interfaces
{
    public interface IHelpCategoriesService
    {
        int Insert(HelpCategoriesAddRequest model);
        List<HelpCategories> Get();
        HelpCategories Get(int id);
        void Update(HelpCategoriesUpdateRequest model);
        void Delete(int id);
    }
}
