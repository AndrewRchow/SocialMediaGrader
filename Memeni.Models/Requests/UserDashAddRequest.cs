using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memeni.Models.Requests
{
    public class UserDashAddRequest
    {
        public int Id { get; set; }
        public string Facebook { get; set; }
        public string Twitter { get; set; }
        public bool WeeklyFB { get; set; }
        public bool WeeklyTwitter { get; set; }
    }
}
