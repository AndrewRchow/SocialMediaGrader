using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memeni.Models.Requests
{
    public class UsersUpdateRequest
    {
        [Required]
        public int Id
        { get; set; }

        public int Role
        { get; set; }

        public bool EmailConfirmed
        { get; set; }

        public bool Lock
        { get; set; }
    }
}
