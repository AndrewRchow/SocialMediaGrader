using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memeni.Models.Domain
{
    public class TwitterTimeline
    {
        public string Created_at { get; set; } // Check if DateTime should be used
        public string Id { get; set; }
        public string Text { get; set; }
        public string In_reply_to_status_id { get; set; } // Null if not reply
        public string In_reply_to_user_id { get; set; } // Null if not reply
        public string In_reply_to_screen_name { get; set; }
        public User User { get; set; }
        public Retweeted_status Retweeted_status { get; set; } // Null if not retweet
        public string Retweet_count { get; set; } // Tweet Retweets
        public string Favorite_count { get; set; } // Tweet Likes
    }

    public class User
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Screen_name { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public string Followers_count { get; set; } // Total Followers
        public string Friends_count { get; set; } // Total Following
        public string Created_at { get; set; }
        public string Favourites_count { get; set; } // Total Likes
        public string Statuses_count { get; set; } // Total Tweets
        public string Profile_image_url { get; set; }
        public string Profile_banner_url { get; set; }
    }

    public class Retweeted_status
    {
        public string Created_at { get; set; }
        public string Id { get; set; }
    }
}
