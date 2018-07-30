using System.Collections.Generic;
using Memeni.Models.Domain;
using Memeni.Models.Requests;

namespace Memeni.Services.Interfaces
{
    public interface ISMEService
    {
        void DeleteById(int id);
        int Insert(SMEAddRequest model);
        List<SME> SelectAll();
        SME SelectById(int id);
        void Update(SMEUpdateRequest model);
    }
}