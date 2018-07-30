using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memeni.Models.Requests
{
    public class LogoUpdateRequest : LogoAddRequest
    {
        public int FileId { get; set; }        
    }
}
