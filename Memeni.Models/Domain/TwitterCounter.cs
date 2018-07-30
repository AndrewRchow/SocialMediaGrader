using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memeni.Models.Domain
{
    public class TwitterCounter
    {
        public string Twitter_id { get; set; }
        public string Username { get; set; } // Screen_name
        public DateTime Date_updated { get; set; } // Shows last update that's not today
        public int Follow_days { get; set; } // Number of days Twitter Counter has been tracking
        public int Started_followers { get; set; } // Number of followers at start of tracking
        public int Growth_since { get; set; } // Number of followers gained since tracking
        public int Average_growth { get; set; } // Average growth per day (possibly useful in calculations)
        public int Followers_yesterday { get; set; }
        public int Followers_current { get; set; }
        public Dictionary<string, int> Followersperdate { get; set; }
    }
}
