using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memeni.Models.Requests
{
    public class AnonUserAddRequest
    {
        public string Email { get; set; }
        public int VisitCount { get; set; }
        public string SessionId { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
