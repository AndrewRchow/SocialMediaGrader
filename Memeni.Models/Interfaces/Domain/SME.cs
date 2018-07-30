using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memeni.Models.Domain
{
    public class SME
    {
        public int Id { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime ModifiedDate { get; set; }

        public string ModifiedBy { get; set; }

        public decimal MaxInteractionsPer1k { get; set; }

        public decimal MinInteractionsPer1k { get; set; }

        public decimal SumInteractionsPer1k { get; set; }
    }
}
