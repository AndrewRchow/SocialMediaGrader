using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memeni.Models.Requests
{
    public class RegisterUserGoogleRequest
    {
        [Required]
        public string Email { get; set; }   

        public bool EmailConfirmed { get; set; }

        public bool Locked { get; set; }
        [Required]
        public string GoogleId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
    }
}
