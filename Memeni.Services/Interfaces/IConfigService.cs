using System.Collections.Generic;
using Memeni.Models.Domain;
using Memeni.Models.Requests;
using Memeni.Models.ViewModels;

namespace Memeni.Services
{
    public interface IConfigService
    {
        List<ConfigIndexModel> Get();
        Config GetById(int Id);
        int Insert(ConfigAddRequest model);
        void Update(ConfigUpdateRequest model);
        void Delete(int Id);
        Config GetByConfigName(string configName);
        string getConfigValusAsString(string configName);
        int getConfigValueAsInt(string configName);
        bool getConfigValueAsBool(string configName);
        decimal getConfigValueAsDecimal(string configName);
    }
}