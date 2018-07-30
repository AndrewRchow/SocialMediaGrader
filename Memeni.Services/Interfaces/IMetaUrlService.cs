using Memeni.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Memeni.Models.Requests;

namespace Memeni.Services.Interfaces
{
    public interface IMetaUrlService
    {
        List<MetaUrl> Get();
        MetaUrl Get(int id);
        int Insert(MetaUrlAddRequest model);
        void Update(MetaUrlUpdateRequest model);
        void UpdateLock(MetaUrlUpdateLockRequest model);
        void Delete(int id);
    }
}
