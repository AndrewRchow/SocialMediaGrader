using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memeni.Models.Requests.Profile
{
    public class ProfileCompanyRequest
    {
        public int UserId { get; set; }
        public string ModifiedBy { get; set; }
        public string CompanyName { get; set; }
        public string CompanyUrl { get; set; }
        public string CompanyLogoUrl { get; set; }
    }
}
