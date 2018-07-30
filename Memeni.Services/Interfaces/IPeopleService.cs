using System.Collections.Generic;
using Memeni.Data.Providers;
using Memeni.Models.Domain;
using Memeni.Models.Requests;

namespace Memeni.Services.Interfaces
{
    public interface IPeopleService
    {
        int Insert(PersonAddRequest model);
        Person Get(int id);
        List<Person> Get();
        void Update(PersonUpdateRequest model);
        void Delete(int id);
    }
}