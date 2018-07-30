using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memeni.Models.Domain
{
    public class FeedData
    {
        public long Id { get; set; }
        public Feed Feed { get; set; }
    }

    public class Feed
    {
        public List<FeedItem> Data { get; set; }
        public Paging Paging { get; set; }
    }

    public class FeedItem
    {
        public DateTime Created_Time { get; set; }
        public string Id { get; set; }
    }

    public class Paging
    {
        public string Next { get; set; }
    }

    public class ActivityLog
    {
        public int Points { get; set; }
        public int Posts_Counted { get; set; }
        public List<ActivityStat> Activity { get; set; }
    }

    public class ActivityStat
    {
        public DateTime Date { get; set; }
        public int Frequency { get; set; }
    }
}
