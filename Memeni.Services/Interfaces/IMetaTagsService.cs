using Memeni.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memeni.Services.Interfaces
{
    public interface IMetaTagsService
    {
        List<MetaTags> Get();
    }
}
