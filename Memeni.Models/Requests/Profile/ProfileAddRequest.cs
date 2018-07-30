using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memeni.Models.Requests.Profile
{
    public class ProfileAddRequest
    {
        public int UserId { get; set; }
        public string ModifiedBy { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string CompanyName { get; set; }
        public string CompanyUrl { get; set; }
        public string CompanyLogoUrl { get; set; }
        public string CountryCode { get; set; }
        public string PhoneNumber { get; set; }
        public string Extension { get; set; }
    }
}
