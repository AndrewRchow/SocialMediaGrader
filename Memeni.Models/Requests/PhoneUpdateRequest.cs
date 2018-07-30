using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memeni.Models.Requests
{
    public class PhoneUpdateRequest : PhoneAddRequest
    {
        [Required]
        public int UserId { get; set; }
    }
}
