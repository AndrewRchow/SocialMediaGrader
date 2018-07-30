using System.Collections.Generic;
using Memeni.Models.Domain;
using Memeni.Models.Requests;

namespace Memeni.Services.Interfaces
{
    public interface ITncService
    {
        void Delete(int id);
        List<Tnc> Get();
        Tnc Get(int id);
        int Insert(TncAddRequest model);
        void Update(TncUpdateRequest model);
        void UpdateAll(List<Tnc> TncTable);
    }
}