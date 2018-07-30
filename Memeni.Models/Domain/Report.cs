using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memeni.Models.Domain
{
    public class Report
    {
        public int OverallGrade { get; set; }
        public int EngGrade { get; set; }
        public int ActGrade { get; set; }
        public int GrowGrade { get; set; }
        public int ReachGrade { get; set; }
        public int DataGrade { get; set; }
        public double FollowersChange { get; set; }
        public int TweetCount { get; set; }
        public int LikesCount { get; set; }
        public int RetweetCount { get; set; }
        public int RepliesCount { get; set; }
        public int MentionCount { get; set; }
        public int RetweetedCount { get; set; }
        public int StatusesPerMonth { get; set; }
        public List<TwitterByDate> Stats { get; set; }
    }
}
