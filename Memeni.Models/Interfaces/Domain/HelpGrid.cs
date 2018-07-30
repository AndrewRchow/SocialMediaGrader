using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memeni.Models.Domain
{
    public class HelpGrid
    {
        public int recordsTotal { get; set; }

        public int recordsFiltered { get; set; }

        public List<Help> data { get; set; }
    }
}
