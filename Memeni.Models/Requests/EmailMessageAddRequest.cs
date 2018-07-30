using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memeni.Models.Requests
{
    public class EmailMessageAddRequest
    {
        [Required]
        public string ToEmail { get; set; }
        [Required]
        public string ToName { get; set; }
        public string FromName { get; set; }
        public string FromEmail { get; set; }
        [Required]
        public string Subject { get; set; }
        [Required]
        public string EmailBody { get; set; }
    }
}
