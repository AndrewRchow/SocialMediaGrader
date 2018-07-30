using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memeni.Models.Domain
{
    public class ErrorLogGrid
    {
        public int recordsTotal { get; set; }

        public int recordsFiltered { get; set; }

        public List<ErrorLog> data { get; set; }
    }
}
