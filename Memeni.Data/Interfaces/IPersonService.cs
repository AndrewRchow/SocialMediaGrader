using System.Collections.Generic;
using Memeni.Models.Domain;
using Memeni.Models.Requests;


namespace Memeni.Data.Interfaces
{
    public interface IPersonService
    {
        void Delete(int id);
        List<Person> Get();
        Person Get(int id);
        int Insert(PersonAddRequest model);
        void Update(PersonUpdateRequest model);
    }
}