using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memeni.Models.Requests
{
    public class ResetPwRequest
    {
        [Required]
        public string Password { get; set; }
        [Required]
        public string Code { get; set; }
    }
}
