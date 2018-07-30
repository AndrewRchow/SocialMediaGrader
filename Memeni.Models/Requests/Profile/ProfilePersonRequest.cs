using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memeni.Models.Requests.Profile
{
    public class ProfilePersonRequest
    {
        public int UserId { get; set; }
        public string ModifiedBy { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
    }
}
