using Memeni.Models.Domain;
using Memeni.Models.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memeni.Services
{
    public interface IConfigCategoryService
    {
        List<ConfigCategory> Get();
        ConfigCategory Get(int Id);
        int Insert(ConfigCategoryAddRequest model);
        void Update(ConfigCategoryUpdateRequest model);
        void Delete(int Id);
    }
}
