using Memeni.Models.Domain;
using Memeni.Models.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memeni.Services.Interfaces
{
    public interface IConfigDataService
    {
        List<ConfigType> Get();
        ConfigType Get(int Id);
        int Insert(ConfigDataAddRequest model);
        void Update(ConfigDataUpdateRequest model);
        void Delete(int Id);
    }
}
