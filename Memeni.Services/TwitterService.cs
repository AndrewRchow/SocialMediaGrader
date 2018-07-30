using Memeni.Models.Domain;
using Memeni.Services.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace Memeni.Services
{
    public class TwitterService : BaseService, ITwitterService
    {
        private IConfigService _configService;

        public TwitterService(IConfigService configService)
        {
            _configService = configService;
        }

        public string PostBearerToken(string encodedKey)
        {
            WebClient client = new WebClient();
            client.Headers[HttpRequestHeader.Authorization] = "Basic " + encodedKey;
            client.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded;charset=UTF-8";
            string data = "grant_type=client_credentials";
            string response = client.UploadString("https://api.twitter.com/oauth2/token", data);

            return response;
        }

        public string GetUserTimeline(string screenName)
        {
            string bearer = _configService.getConfigValusAsString("TwitterBearerToken");
            WebClient client = new WebClient();
            client.Headers[HttpRequestHeader.Authorization] = "Bearer " + bearer;
            String link = "https://api.twitter.com/1.1/statuses/user_timeline.json?count=200&screen_name=" + screenName;
            string response = client.DownloadString(link);

            return response;
        }

        public string GetUserTimelineNext(string screenName, string id)
        {
            string bearer = _configService.getConfigValusAsString("TwitterBearerToken");
            WebClient client = new WebClient();
            client.Headers[HttpRequestHeader.Authorization] = "Bearer " + bearer;
            String link = "https://api.twitter.com/1.1/statuses/user_timeline.json?count=200&screen_name=" + screenName + "&max_id=" + id;
            string response = client.DownloadString(link);

            return response;
        }

        public string GetFollowers(string id)
        {
            List<string> keys = new List<string>();
            keys.Add("TwitterCounterKey");
            keys.Add("TwitterCounterKey2");
            keys.Add("TwitterCounterKey3");
            Random random = new Random();
            int position = random.Next(keys.Count - 1);
            string key = _configService.getConfigValusAsString(keys[position]);
            WebClient client = new WebClient();
            String link = "http://api.twittercounter.com/?apikey=" + key + "&twitter_id=" + id;
            string response = client.DownloadString(link);

            return response;
        }

        public string GetSearch(string screenName)
        {
            string bearer = _configService.getConfigValusAsString("TwitterBearerToken");
            WebClient client = new WebClient();
            client.Headers[HttpRequestHeader.Authorization] = "Bearer " + bearer;
            String link = "https://api.twitter.com/1.1/search/tweets.json?count=100&q=@" + screenName;
            string response = client.DownloadString(link);

            return response;
        }

        public string GetSearchNext(string screenName, string id)
        {
            string bearer = _configService.getConfigValusAsString("TwitterBearerToken");
            WebClient client = new WebClient();
            client.Headers[HttpRequestHeader.Authorization] = "Bearer " + bearer;
            String link = "https://api.twitter.com/1.1/search/tweets.json?count=100&q=@" + screenName + "&max_id=" + id;
            string response = client.DownloadString(link);

            return response;
        }

        public string GetTwitterReport(string screenName)
        {
            string response;
            string error = "error";
            try
            {
                response = GetUserTimeline(screenName);
            }
            catch
            {
                return error;
            }
            List<TwitterTimeline> timelineTotal = new List<TwitterTimeline>();
            List<TwitterTimeline> timeline = new JavaScriptSerializer().Deserialize<List<TwitterTimeline>>(response);
            const string format = "ddd MMM dd HH:mm:ss zzzz yyyy";
            DateTime lastDate = new DateTime();
            DateTime tweetDate = new DateTime();
            for (int i = 0; i < timeline.Count; i++)
            {
                tweetDate = DateTime.ParseExact(timeline[i].Created_at, format, CultureInfo.InvariantCulture);
                if (tweetDate > DateTime.Today.AddDays(-10))
                {
                    timelineTotal.Add(timeline[i]);
                }
            }
            lastDate = DateTime.ParseExact(timeline[timeline.Count - 1].Created_at, format, CultureInfo.InvariantCulture);
            string lastTweet = timeline[timeline.Count - 1].Id;
            do
            {
                response = GetUserTimelineNext(screenName, lastTweet);
                List<TwitterTimeline> timelineNext = new JavaScriptSerializer().Deserialize<List<TwitterTimeline>>(response);
                for (int i = 0; i < timelineNext.Count; i++)
                {
                    if (lastDate > DateTime.Today.AddDays(-10))
                    {
                        timelineTotal.Add(timelineNext[i]);
                    }

                }
                lastDate = DateTime.ParseExact(timelineNext[timelineNext.Count - 1].Created_at, format, CultureInfo.InvariantCulture);
                lastTweet = timelineNext[timelineNext.Count - 1].Id;
            } while (timelineTotal.Count < 3200 && (lastDate > DateTime.Today.AddDays(-10)));
            if (timelineTotal.Count < 100)
            {
                timelineTotal.Clear();
                for (int i = 0; i < 100; i++)
                {
                    timelineTotal.Add(timeline[i]);
                }
            }
            double daysTracked = (DateTime.Now - DateTime.ParseExact(timelineTotal[timelineTotal.Count - 1].Created_at, format, CultureInfo.InvariantCulture)).TotalDays;
            int retweets = 0;
            for (int i = 0; i < timelineTotal.Count; i++)
            {
                if (timelineTotal[i].Retweeted_status != null)
                {
                    retweets++;
                }
            }
            int replies = 0;
            for (int i = 0; i < timelineTotal.Count; i++)
            {
                if (timelineTotal[i].In_reply_to_status_id != null)
                {
                    replies++;
                }
            }
            int tweets = timelineTotal.Count - (retweets + replies);
            double posts = (30 / daysTracked) * tweets;
            int actGrade = 0;
            if (posts > 38)
            {
                actGrade = 25;
            }
            else if (posts > 30 && posts <= 38)
            {
                actGrade = 20;
            }
            else if (posts > 23 && posts <= 30)
            {
                actGrade = 15;
            }
            else if (posts > 15 && posts <= 23)
            {
                actGrade = 10;
            }
            else if (posts > 8 && posts <= 15)
            {
                actGrade = 5;
            }
            else
            {
                actGrade = 0;
            }
            string userId = timelineTotal[0].User.Id;
            double followersTotal = Convert.ToDouble(timelineTotal[0].User.Followers_count);
            string counterResponse = GetFollowers(userId);
            TwitterCounter twitterCounter = new JavaScriptSerializer().Deserialize<TwitterCounter>(counterResponse);
            double estDaily = (twitterCounter.Followers_current - twitterCounter.Started_followers) / twitterCounter.Follow_days;
            double followThirty = estDaily * 30;
            double change = ((followersTotal / (followersTotal - followThirty)) - 1) * 100;
            int growth = 0;
            if (change > 2)
            {
                growth = 25;
            }
            else if (change > 1.5 && change <= 2)
            {
                growth = 20;
            }
            else if (change > 1 && change <= 1.5)
            {
                growth = 15;
            }
            else if (change > 0.5 && change <= 1)
            {
                growth = 10;
            }
            else if (change > 0 && change <= 0.5)
            {
                growth = 5;
            }
            else
            {
                growth = 0;
            }
            double changeRound = Math.Round(change, 2);
            int likesCount = 0;
            int retweetCount = 0;
            for (int i = 0; i < timelineTotal.Count; i++)
            {
                likesCount += Convert.ToInt32(timelineTotal[i].Favorite_count);
                retweetCount += Convert.ToInt32(timelineTotal[i].Retweet_count);
            }
            string searchResponse = GetSearch(screenName);
            TwitterSearch search = new JavaScriptSerializer().Deserialize<TwitterSearch>(searchResponse);
            List<Status> searchTotal = new List<Status>();
            int searchRetweets = 0;
            int searchReplies = 0;
            int searchMentions = 0;
            double searchDays = 0;
            int repliesCount = 0;
            int mentionCount = 0;
            int statusesPerMonth = 0;
            if (search.Statuses.Count != 0)
            {
                for (int i = 0; i < search.Statuses.Count; i++)
                {
                    searchTotal.Add(search.Statuses[i]);
                }
                string lastSearch = searchTotal[searchTotal.Count - 1].Id;
                if (searchTotal.Count == 100)
                {
                    int searchCount = 100;
                    do
                    {
                        searchResponse = GetSearchNext(screenName, lastSearch);
                        searchCount += 100;
                        TwitterSearch searchNext = new JavaScriptSerializer().Deserialize<TwitterSearch>(searchResponse);
                        for (int i = 0; i < searchNext.Statuses.Count; i++)
                        {
                            searchTotal.Add(searchNext.Statuses[i]);
                        }
                        lastSearch = searchNext.Statuses[searchNext.Statuses.Count - 1].Id;
                    } while (searchTotal.Count < 500 && (searchCount == 200 || searchCount == 300 || searchCount == 400));
                }
                searchDays = (DateTime.Now - DateTime.ParseExact(searchTotal[searchTotal.Count - 1].Created_at, format, CultureInfo.InvariantCulture)).TotalDays;
                for (int i = 0; i < searchTotal.Count; i++)
                {
                    if (searchTotal[i].Text.ToLower().Contains("rt @" + screenName.ToLower()))
                    {
                        searchRetweets++;
                    }
                }
                for (int i = 0; i < searchTotal.Count; i++)
                {
                    if (searchTotal[i].In_reply_to_screen_name == screenName)
                    {
                        searchReplies++;
                    }
                }
                statusesPerMonth = Convert.ToInt32((30 / searchDays) * searchTotal.Count);
            }
            searchMentions = searchTotal.Count - (searchRetweets + searchReplies);
            int engage = 0;
            double engagePercent = ((((30 / daysTracked) * likesCount) + ((30 / daysTracked) * retweetCount) + repliesCount) / followersTotal) * 100;
            if (engagePercent >= 3)
            {
                engage = 25;
            }
            else if (engagePercent >= 2.5 && engagePercent < 3)
            {
                engage = 22;
            }
            else if (engagePercent >= 1.5 && engagePercent < 2.5)
            {
                engage = 15;
            }
            else if (engagePercent >= 0.5 && engagePercent < 1.5)
            {
                engage = 18;
            }
            else if (engagePercent > 0 && engagePercent < 0.5)
            {
                engage = 3;
            }
            else
            {
                engage = 0;
            }
            int reach = 0;
            double reachPercent = ((0.66 * ((30 / searchDays) * searchRetweets) * ((30 / searchDays) * searchMentions)) / (statusesPerMonth * statusesPerMonth)) * 100;
            if (reachPercent > 20)
            {
                reach = 8;
            }
            else if (reachPercent > 15 && reachPercent <= 20)
            {
                reach = 6;
            }
            else if (reachPercent > 10 && reachPercent <= 15)
            {
                reach = 4;
            }
            else if (reachPercent > 4 && reachPercent <= 10)
            {
                reach = 2;
            }
            else
            {
                reach = 0;
            }
            string dataGrade = _configService.getConfigValusAsString("dataGrade");
            Report report = new Report();
            report.ActGrade = actGrade;
            report.GrowGrade = growth;
            report.EngGrade = engage;
            report.ReachGrade = reach;
            report.DataGrade = Convert.ToInt32(dataGrade);
            report.OverallGrade = report.ActGrade + report.GrowGrade + report.EngGrade + report.ReachGrade + report.DataGrade;
            report.FollowersChange = changeRound;
            report.TweetCount = Convert.ToInt32(posts);
            report.LikesCount = Convert.ToInt32((30 / daysTracked) * likesCount);
            report.RetweetCount = Convert.ToInt32((30 / daysTracked) * retweetCount);
            report.StatusesPerMonth = statusesPerMonth;
            report.Stats = new List<TwitterByDate>();
            List<TwitterTimeline> tbdTimeline = new List<TwitterTimeline>();
            for (int i = 0; i < timelineTotal.Count; i++)
            {
                tbdTimeline.Add(timelineTotal[i]);
            }
            List<TwitterByDate> tbdList = new List<TwitterByDate>();
            TwitterByDate tbd = new TwitterByDate();
            if (tbdTimeline.Count != 0)
            {
                for (int i = 0; i < tbdTimeline.Count; i++)
                {
                    DateTime newDate = DateTime.ParseExact(tbdTimeline[i].Created_at, format, CultureInfo.InvariantCulture);
                    var day = newDate.Date;
                    tbdTimeline[i].Created_at = Convert.ToString(day);
                }
                tbd.Date = Convert.ToDateTime(tbdTimeline[0].Created_at).Date;
                tbd.DateString = tbd.Date.ToString(@"MM/dd/yy");
                tbd.Tweets = 0;
                tbd.Retweets = 0;
                tbd.Likes = 0;
                for (int i = 0; i < tbdTimeline.Count - 1; i++)
                {
                    if (tbdTimeline[i].Created_at == Convert.ToString(tbd.Date))
                    {
                        tbd.Retweets += Convert.ToInt32(tbdTimeline[i].Retweet_count);
                        tbd.Likes += Convert.ToInt32(tbdTimeline[i].Favorite_count);
                        if (tbdTimeline[i].In_reply_to_screen_name == null && !tbdTimeline[i].Text.Contains("RT @"))
                        {
                            tbd.Tweets++;
                        }
                    }
                    else
                    {
                        tbdList.Add(tbd);
                        tbd = new TwitterByDate();
                        tbd.Date = Convert.ToDateTime(tbdTimeline[i].Created_at).Date;
                        tbd.DateString = tbd.Date.ToString(@"MM/dd/yy");
                    }
                }
            }
            List<Status> tbdSearch = new List<Status>();
            for (int i = 0; i < searchTotal.Count; i++)
            {
                tbdSearch.Add(searchTotal[i]);
            }
            if (tbdSearch.Count != 0)
            {
                for (int i = 0; i < tbdSearch.Count; i++)
                {
                    DateTime newDate2 = DateTime.ParseExact(tbdSearch[i].Created_at, format, CultureInfo.InvariantCulture);
                    var day2 = newDate2.Date;
                    tbdSearch[i].Created_at = Convert.ToString(day2);
                }
                tbd.Date = Convert.ToDateTime(tbdSearch[0].Created_at).Date;
                tbd.DateString = tbd.Date.ToString(@"MM/dd/yy");
                tbd.Replies = 0;
                tbd.Mentions = 0;
                tbd.Tweets = 0;
                tbd.Retweets = 0;
                tbd.Likes = 0;
                for (int i = 0; i < tbdSearch.Count; i++)
                {
                    if (tbdSearch[i].Created_at == Convert.ToString(tbd.Date))
                    {
                        if (tbdSearch[i].In_reply_to_screen_name == screenName)
                        {
                            tbd.Replies++;
                        }
                        else if (!tbdSearch[i].Text.ToLower().Contains("rt @" + screenName.ToLower()))
                        {
                            tbd.Mentions++;
                        }
                        else
                        {
                            tbd.Retweeted++;
                        }
                    }
                    else
                    {
                        for (int a = 0; a < tbdList.Count; a++)
                        {
                            if (tbdList[a].DateString == tbd.DateString)
                            {
                                tbdList[a].Mentions = tbd.Mentions;
                                tbdList[a].Replies = tbd.Replies;
                                tbdList[a].Retweeted = tbd.Retweeted;
                            }
                        }
                        tbd = new TwitterByDate();
                        tbd.Date = Convert.ToDateTime(tbdSearch[i].Created_at).Date;
                        tbd.DateString = tbd.Date.ToString(@"MM/dd/yy");
                    }
                    if (tbdSearch[i] == tbdSearch[tbdSearch.Count - 1] && tbdSearch[0].Created_at == tbdSearch[tbdSearch.Count - 1].Created_at)
                    {
                        for (int a = 0; a < tbdList.Count; a++)
                        {
                            if (tbd.DateString == tbdList[a].DateString)
                            {
                                tbdList[a].Mentions = tbd.Mentions;
                                tbdList[a].Replies = tbd.Replies;
                                tbdList[a].Retweeted = tbd.Retweeted;
                            }
                        }
                    }
                }
            }
            daysTracked = (DateTime.Now - tbdList[tbdList.Count - 1].Date).TotalDays;
            double tbdMentions = 0;
            double tbdReplies = 0;
            double tbdRetweeted = 0;
            for (int i = 0; i < tbdList.Count; i++)
            {
                tbdMentions += tbdList[i].Mentions;
                tbdReplies += tbdList[i].Replies;
                tbdRetweeted += tbdList[i].Retweeted;
            }
            mentionCount = Convert.ToInt32((30 / daysTracked) * tbdMentions);
            repliesCount = Convert.ToInt32((30 / daysTracked) * tbdReplies);
            int retweetedCount = Convert.ToInt32((30 / daysTracked) * tbdRetweeted);
            if (tbdList.Count > 10)
            {
                tbdList.RemoveRange(10, tbdList.Count - 10);
            }


            report.RepliesCount = repliesCount;
            report.MentionCount = mentionCount;
            report.RetweetedCount = retweetedCount;
            report.Stats = tbdList;

            //string jsonReport = new JavaScriptSerializer().Serialize(report);
            string jsonReport = new JavaScriptSerializer().Serialize(report);

            return jsonReport;
        }
    }
}