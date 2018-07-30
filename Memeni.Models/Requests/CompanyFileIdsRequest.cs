using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memeni.Models.Requests
{
    public class CompanyFileIdsRequest
    {
        public int UserId { get; set; }
        public int FileId { get; set; }
    }
}
