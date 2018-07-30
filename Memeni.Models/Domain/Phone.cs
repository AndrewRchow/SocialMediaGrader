using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memeni.Models.Domain
{
    public class Phone
    {
        public int UserId { get; set; }
        public string CountryCode { get; set; }
        public string PhoneNumber { get; set; }
        public string Extension { get; set; }
        public DateTime CreatedDate { get; set; }

        public DateTime ModifiedDate { get; set; }

        public string ModifiedBy { get; set; }
    }
}
