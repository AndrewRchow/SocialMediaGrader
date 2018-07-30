using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memeni.Models.Domain
{
    public class Users
    {
        public int Id
        { get; set; }

        public string Email
        { get; set; }

        public string Role
        { get; set; }

        public bool EmailConfirmed
        { get; set; }

        public bool Lock
        { get; set; }

        public DateTime DateCreated
        { get; set; }
    }
}
