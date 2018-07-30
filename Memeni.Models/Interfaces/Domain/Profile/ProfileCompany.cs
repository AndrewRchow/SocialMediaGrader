using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memeni.Models.Domain.Profile
{
    public class ProfileCompany
    {
        public int UserId { get; set; }
        public string ModifiedBy { get; set; }
        public string CompanyName { get; set; }
        public string CompanyUrl { get; set; }
        public string CompanyLogoUrl { get; set; }

    }
}
