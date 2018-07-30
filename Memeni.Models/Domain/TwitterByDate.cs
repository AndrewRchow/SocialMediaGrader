using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memeni.Models.Domain
{
    public class TwitterByDate
    {
        public DateTime Date { get; set; }
        public string DateString { get; set; }
        public int Tweets { get; set; }
        public int Retweets { get; set; }
        public int Likes { get; set; }
        public int Replies { get; set; }
        public int Mentions { get; set; }
        public int Retweeted { get; set; }
    }
}
