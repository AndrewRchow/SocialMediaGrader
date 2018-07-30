using Memeni.Models.Domain;
using Memeni.Models.Responses;
using Memeni.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace Memeni.Web.Controllers.Api
{
    [RoutePrefix("api/fb")]
    public class FacebookApiController : ApiController
    {
        private IFacebookService _facebookService;
        private IConfigService _configService;

        public FacebookApiController(IFacebookService facebookService, IConfigService configService)
        {
            _facebookService = facebookService;
            _configService = configService;
        }

        //Gets profile info for the user
        [Route("profile/{name}"), HttpGet]
        public HttpResponseMessage Profile(string name)
        {
            //Facebook App Auth Token below
            string token = _configService.getConfigValusAsString("FbAppAuthToken"); //"340834969679482 |VZHv9QJUimMQxf0Z3GDYN6EGFX4";
            string site = name;
            try
            {
                string item = _facebookService.GetFbProfile(token, site);
                try
                {
                    var data = new JavaScriptSerializer().Deserialize<object>(item);
                    return Request.CreateResponse(data);
                }
                catch
                {
                    return Request.CreateResponse(item);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
        //Gets Total Shares, Likes, Comments, Reactions of all Feed within Time Frame
        [Route("feed/{name}"), HttpGet]
        public HttpResponseMessage Feed(string name)
        {
            //Facebook App Auth Token below
            string token = _configService.getConfigValusAsString("FbAppAuthToken"); //"340834969679482 |VZHv9QJUimMQxf0Z3GDYN6EGFX4";
            string site = name;
            int feedPerCall = _configService.getConfigValueAsInt("FbAuthFeedFeedPerCall");//50;
            int daysInTimeFrame = _configService.getConfigValueAsInt("FbAuthFeedDaysInTimeFrame");// 7;

            try
            {
                string item = _facebookService.GetFbFeed(token, site, feedPerCall);
                FeedData fbFeed = new JavaScriptSerializer().Deserialize<FeedData>(item);

                List<FeedItem> al = new List<FeedItem>();

                //Time frame for Feed to get set to One week
                var timeFrame = DateTime.Today.Date.AddDays(-daysInTimeFrame);

                bool moreFeed = false;
                string next;
                //Gets First batch of feed and adds to array all that is within timeframe
                if (fbFeed.Feed.Data[feedPerCall - 1].Created_Time > timeFrame)
                {
                    for (var i = 0; i < feedPerCall; i++)
                    {
                        al.Add(fbFeed.Feed.Data[i]);
                    }
                    moreFeed = true;
                }
                else
                {
                    bool continueLoop = true;
                    var i = 0;
                    while (continueLoop)
                    {
                        if (fbFeed.Feed.Data[i].Created_Time < timeFrame)
                        {
                            continueLoop = false;
                            continue;
                        }
                        al.Add(fbFeed.Feed.Data[i]);
                        i++;
                    }
                }
                //Loops and adds more Feed until time frame is past
                if (moreFeed)
                {
                    next = fbFeed.Feed.Paging.Next;
                    bool stopper = true;

                    while (stopper)
                    {
                        string nextItem = _facebookService.GetFbFeedNext(next);
                        Feed fbFeedNext = new JavaScriptSerializer().Deserialize<Feed>(nextItem);

                        if (fbFeedNext.Data[feedPerCall - 1].Created_Time > timeFrame)
                        {
                            for (var i = 0; i < feedPerCall; i++)
                            {
                                al.Add(fbFeedNext.Data[i]);
                            }
                            next = fbFeedNext.Paging.Next;
                        }
                        else
                        {
                            bool continueLoop = true;
                            var i = 0;
                            while (continueLoop)
                            {
                                if (fbFeedNext.Data[i].Created_Time < timeFrame)
                                {
                                    continueLoop = false;
                                }
                                al.Add(fbFeedNext.Data[i]);
                                i++;
                            }
                            stopper = false;
                        }
                    }
                }

                List<FbPostData> stats = new List<FbPostData>();
                //Loops thru all of the feed and gets likes, comments, shares, and reactions for each
                for (var i = 0; i < al.Count; i++)
                {
                    string iPost = al[i].Id;
                    string postData = _facebookService.GetPostData(token, iPost);
                    FbPostData post = new JavaScriptSerializer().Deserialize<FbPostData>(postData);
                    post.Date = al[i].Created_Time;
                    stats.Add(post);
                }

                List<FbWkPostData> lpd = new List<FbWkPostData>();
                FbWkPostData pd = new FbWkPostData();
                pd.Shares = 0;
                pd.Likes = 0;
                pd.Reactions = 0;
                pd.Comments = 0;
                pd.Date = stats[0].Date.Date;
                pd.DateString = stats[0].Date.ToString(@"MM/dd/yy");
                pd.Posts = 0;

                int sharesCount = 0;
                int likesCount = 0;
                int commentsCount = 0;
                int reactionsCount = 0;

                for (var i = 0; i < al.Count; i++)
                {
                    if(stats[i].Date.Date == pd.Date.Date)
                    {
                        pd.Posts++;
                        pd.Likes += stats[i].Likes.Summary.Total_Count;
                        pd.Reactions += stats[i].Reactions.Summary.Total_Count;
                        pd.Comments += stats[i].Comments.Summary.Total_Count;

                        if (stats[i].Shares == null)
                        {
                            if (i == al.Count - 1)
                            {
                                lpd.Add(pd);
                            }
                            continue;
                        }
                        pd.Shares += stats[i].Shares.Count;
                        if (i == al.Count -1)
                        {
                            lpd.Add(pd);
                        }
                    }
                    else
                    {
                        while(stats[i].Date.Date != pd.Date.Date.AddDays(-1))
                        {
                            lpd.Add(pd);
                            DateTime prev = pd.Date.Date.AddDays(-1);
                            pd = new FbWkPostData();
                            pd.Posts = 0;
                            pd.Date = prev;
                            pd.DateString = pd.Date.ToString(@"MM/dd/yy");
                            pd.Likes = 0;
                            pd.Reactions = 0;
                            pd.Comments = 0;
                            pd.Shares = 0;
                        }
                        lpd.Add(pd);
                        pd = new FbWkPostData();
                        pd.Posts = 1;
                        pd.Date = stats[i].Date.Date;
                        pd.DateString = stats[i].Date.ToString(@"MM/dd/yy");
                        pd.Likes = stats[i].Likes.Summary.Total_Count;
                        pd.Reactions = stats[i].Reactions.Summary.Total_Count;
                        pd.Comments = stats[i].Comments.Summary.Total_Count;

                        if (stats[i].Shares == null)
                        {
                            pd.Shares = 0;
                            continue;
                        }

                        pd.Shares = stats[i].Shares.Count;
                    }

                    likesCount += stats[i].Likes.Summary.Total_Count;
                    reactionsCount += stats[i].Reactions.Summary.Total_Count;
                    commentsCount += stats[i].Comments.Summary.Total_Count;

                    if (stats[i].Shares == null)
                    {
                        continue;
                    }
                    sharesCount += stats[i].Shares.Count;
                }

                FbPostDataTotals statCount = new FbPostDataTotals();

                statCount.Total_Shares = sharesCount;
                statCount.Total_Likes = likesCount;
                statCount.Total_Comments = commentsCount;
                statCount.Total_Reactions = reactionsCount;
                statCount.Total_Feed_Collected = al.Count;
                statCount.Week_Stats = lpd;

                return Request.CreateResponse(statCount);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [Route("activity/{name}"), HttpGet]
        public HttpResponseMessage Activity(string name)
        {
            //Facebook App Auth Token below
            string token = _configService.getConfigValusAsString("FbAppAuthToken");// "340834969679482|VZHv9QJUimMQxf0Z3GDYN6EGFX4";


            string site = name;
            int feedPerCall = _configService.getConfigValueAsInt("FbAuthActivityFeedPerCall");// 25;
            int daysInTimeFrame = _configService.getConfigValueAsInt("FbAuthActivityDaysInTimeFrame");//30;
            var timeFrame = DateTime.Today.AddDays(-daysInTimeFrame);
            int score = 0;

            try
            {
                string item = _facebookService.GetFbFeed(token, site, feedPerCall);
                FeedData fbFeed = new JavaScriptSerializer().Deserialize<FeedData>(item);

                if (fbFeed.Feed.Data[24].Created_Time > timeFrame)
                {
                    score = 25;
                }
                else if (fbFeed.Feed.Data[19].Created_Time > timeFrame)
                {
                    score = 20;
                }
                else if (fbFeed.Feed.Data[14].Created_Time > timeFrame)
                {
                    score = 15;
                }
                else if (fbFeed.Feed.Data[9].Created_Time > timeFrame)
                {
                    score = 10;
                }
                else if (fbFeed.Feed.Data[4].Created_Time > timeFrame)
                {
                    score = 5;
                }
                else
                {
                    score = 0;
                }

                List<ActivityStat> al = new List<ActivityStat>();
                ActivityStat actStat = new ActivityStat();
                actStat.Date = fbFeed.Feed.Data[0].Created_Time.Date;
                actStat.Frequency = 0;

                for (var i=0; i< feedPerCall; i++)
                {                  
                    if(actStat.Date == fbFeed.Feed.Data[i].Created_Time.Date)
                    {
                        actStat.Frequency++;
                        if(i == feedPerCall - 1)
                        {
                            al.Add(actStat);
                        }
                    }
                    else
                    {
                        al.Add(actStat);
                        actStat = new ActivityStat();
                        actStat.Date = fbFeed.Feed.Data[i].Created_Time.Date;
                        actStat.Frequency = 1;
                        if (i == feedPerCall - 1)
                        {
                            al.Add(actStat);
                        }
                    }
                }

                ActivityLog report = new ActivityLog();
                report.Posts_Counted = fbFeed.Feed.Data.Count;
                report.Points = score;
                report.Activity = al;

                return Request.CreateResponse(report);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }

        }

        [Route("growth/{name}"), HttpGet]
        public HttpResponseMessage Growth(string name)
        {
            //Facebook App Auth Token below
            string token = "340834969679482|VZHv9QJUimMQxf0Z3GDYN6EGFX4";
            string site = name;

            try
            {
                string item = _facebookService.GetFbFans(token, site);
                FbFansData dataReport = new JavaScriptSerializer().Deserialize<FbFansData>(item);
                FbFansReport report = new FbFansReport();

                report.Total_Fans_Current = dataReport.Data[0].Values[28].Value.Sum(x => x.Value);
                report.Date_Current = dataReport.Data[0].Values[28].End_Time.ToString("D");

                report.Total_Fans_Month_Ago = dataReport.Data[0].Values[0].Value.Sum(x => x.Value);
                report.Date_Month_Ago = dataReport.Data[0].Values[0].End_Time.ToString("D");

                return Request.CreateResponse(report);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }

        }

        [Route("reach/{name}"), HttpGet]
        public HttpResponseMessage Reach(string name)
        {
            //Facebook App Auth Token below
            string token = "340834969679482|VZHv9QJUimMQxf0Z3GDYN6EGFX4";
            string site = name;

            try
            {
                string item = _facebookService.GetFbTalking(token, site);
                FbTalking dataReport = new JavaScriptSerializer().Deserialize<FbTalking>(item);
                FbTalkReport report = new FbTalkReport();

                //change data array number for day, week, or month interval since day of the values array number
                // [0] is day; [1] is week; [2] is month previous since date of values[x]
                report.Talk_Current = dataReport.Data[2].Values[28].Value.Sum(x => x.Value);
                report.Date_Current = dataReport.Data[2].Values[28].End_Time.ToString("D");
                report.Country_Talk_Current = dataReport.Data[2].Values[28].Value;

                return Request.CreateResponse(report);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }

        }
        
        //Posts the report as cookie
        [Route("report"), HttpPost]
        public HttpResponseMessage PostReport([FromBody] FbReport fbReport)
        {
            try
            {
                var resp = new HttpResponseMessage();
                string myObjectJson = new JavaScriptSerializer().Serialize(fbReport);
                var cookie = new CookieHeaderValue("Report", myObjectJson);
                cookie.Expires = DateTimeOffset.Now.AddDays(1);
                cookie.Domain = Request.RequestUri.Host;
                cookie.Path = "/";

                resp.Headers.AddCookies(new CookieHeaderValue[] { cookie });
                return resp;

                //HttpResponseMessage resp = Request.CreateResponse(fbReport);
                //return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        ////Gets App Auth Token to access Facebook Graph Api
        //[Route("token"), HttpGet]
        //public HttpResponseMessage GetToken()
        //{
        //    string token = _facebookService.GetAppAuth();

        //    return Request.CreateResponse(HttpStatusCode.OK, token);
        //}
    }
}