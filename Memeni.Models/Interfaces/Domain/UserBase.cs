using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memeni.Models.Domain
{
    public class UserBase : IUserAuthData
    {
        public int Id
        { get;set; }

        public string Name
        { get; set; }

        public string Password
        { get; set; }

        public IEnumerable<string> Roles
        { get; set; }

        public string Salt
        { get; set; }

    }
}
