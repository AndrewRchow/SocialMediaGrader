using Memeni.Models.Domain;
using Memeni.Models.Requests;
using System.Collections.Generic;

namespace Memeni.Services.Interfaces
{
    public interface IUrlAddressService
    {
        void Delete(int id);
        int Insert(UrlAddressAddRequest model);
        void Update(UrlAddressUpdateRequest model);
        UrlAddress Get(int id);
        List<UrlAddress> GetAll();
    }
}