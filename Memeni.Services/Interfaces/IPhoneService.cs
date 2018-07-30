using System.Collections.Generic;
using Memeni.Models.Domain;
using Memeni.Models.Requests;

namespace Memeni.Services.Interfaces
{
    public interface IPhoneService
    {
        void Delete(int id);
        Phone Get(int id);
        List<Phone> GetAll();
        int Insert(PhoneAddRequest model);
        void Update(PhoneUpdateRequest model);
    }
}