using System.Collections.Generic;
using Memeni.Models.Domain;
using Memeni.Models.Requests;

namespace Memeni.Services.Interfaces
{
    public interface IHelpService
    {
        int Insert(HelpAddRequest model);
        List<Help> Get();
        Help Get(int id);
        void Update(HelpUpdateRequest model);
        void Delete(int id);
        HelpGrid GetGrid(HelpGridRequest model);
        List<Help> GetByUrl(string PageUrl);
        List<HelpCategories> GetCategories();
    }
}
