using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memeni.Models.Domain
{
    public class FbReport
    {
        public int Total_Shares { get; set; }
        public int Total_Likes { get; set; }
        public int Total_Comments { get; set; }
        public int Total_Reactions { get; set; }
        public int Total_Followers { get; set; }
        public int Score { get; set; }
        public int Engagement_Points { get; set; }
        public int Activity_Points { get; set; }
        public int Activity_Score { get; set; }
    }
}
