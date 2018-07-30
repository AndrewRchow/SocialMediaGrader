using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memeni.Models.Requests
{
    public class SMEAddRequest
    {
        [Required]
        public string ModifiedBy { get; set; }
        [Required]
        public decimal MaxInteractionsPer1k { get; set; }
        [Required]
        public decimal MinInteractionsPer1k { get; set; }
        [Required]
        public decimal SumInteractionsPer1k { get; set; }
    }
}
