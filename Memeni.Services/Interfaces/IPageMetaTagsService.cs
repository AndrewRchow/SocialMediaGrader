using Memeni.Models.Domain;
using Memeni.Models.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memeni.Services.Interfaces
{
    public interface IPageMetaTagsService
    {
        List<PageMetaTags> Get();
        List<PageMetaTags> GetByUrl(string pageUrl);
        List<PageMetaTags> GetByUrlId(int id);
        PageMetaTags Get(int id);
        void Insert(PageMetaTagsAddRequest model);
        void Delete(int id);
        void UpdateValue(PageMetaTagsUpdateValueRequest model);
    }
}
