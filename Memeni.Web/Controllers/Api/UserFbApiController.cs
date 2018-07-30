using Memeni.Models.Domain;
using Memeni.Models.Requests;
using Memeni.Services;
using Memeni.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace Memeni.Web.Controllers.Api
{
    [RoutePrefix("api/user/fb")]
    public class UserFbApiController : ApiController
    {
        private IFacebookService _facebookService;
        private IConfigService _configService;
        private IFbDbService _facebookDbService;

        public UserFbApiController(IFacebookService facebookService, IConfigService configService, IFbDbService facebookDbService)
        {
            _facebookService = facebookService;
            _configService = configService;
            _facebookDbService = facebookDbService;
        }

        //inserts profile info for the user
        [Route("profile/{name}"), HttpGet]
        public HttpResponseMessage Profile(string name)
        {
            //Facebook App Auth Token below
            string token = _configService.getConfigValusAsString("FbAppAuthToken"); //"340834969679482 |VZHv9QJUimMQxf0Z3GDYN6EGFX4";
            string site = name;
            try
            {
                string item = _facebookService.GetFbProfile(token, site);
                FbProfileAddRequest data = new JavaScriptSerializer().Deserialize<FbProfileAddRequest>(item);

                FbProfileAddRequest profile = _facebookDbService.InsertProfile(data);

                return Request.CreateResponse(HttpStatusCode.OK, profile);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
        //inserts or updates 30d posts for the user and returns list of the 30d posts
        [Route("posts/{name}"), HttpPost]
        public HttpResponseMessage Posts(string name)
        {
            //Facebook App Auth Token below
            string token = _configService.getConfigValusAsString("FbAppAuthToken"); //"340834969679482 |VZHv9QJUimMQxf0Z3GDYN6EGFX4";
            string site = name;
            int feedPerCall = 100;
            int daysInTimeFrame = 30;
            string userId = "";

            try
            {
                string item = _facebookService.GetFbFeed(token, site, feedPerCall);
                FeedData fbFeed = new JavaScriptSerializer().Deserialize<FeedData>(item);

                userId = fbFeed.Id.ToString();
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

                List<FbWkPostIdData> lpd = new List<FbWkPostIdData>();
                FbWkPostIdData pd = new FbWkPostIdData();
                pd.Shares = 0;
                pd.Likes = 0;
                pd.Reactions = 0;
                pd.Comments = 0;
                pd.Date = stats[0].Date.Date;
                pd.DateString = stats[0].Date.ToString(@"MM/dd/yy");
                pd.Posts = 0;
                pd.Id = userId;

                for (var i = 0; i < al.Count; i++)
                {
                    if (stats[i].Date.Date == pd.Date.Date)
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
                        if (i == al.Count - 1)
                        {
                            lpd.Add(pd);
                        }
                    }
                    else
                    {
                        while (stats[i].Date.Date != pd.Date.Date.AddDays(-1))
                        {
                            lpd.Add(pd);
                            DateTime prev = pd.Date.Date.AddDays(-1);
                            pd = new FbWkPostIdData();
                            pd.Id = userId;
                            pd.Posts = 0;
                            pd.Date = prev;
                            pd.DateString = pd.Date.ToString(@"MM/dd/yy");
                            pd.Likes = 0;
                            pd.Reactions = 0;
                            pd.Comments = 0;
                            pd.Shares = 0;
                        }
                        lpd.Add(pd);
                        pd = new FbWkPostIdData();
                        pd.Id = userId;
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

                    if (stats[i].Shares == null)
                    {
                        continue;
                    }
                }

                foreach (FbWkPostIdData dbPost in lpd)
                {
                    _facebookDbService.InsertDayFeed(dbPost);
                }

                return Request.CreateResponse(HttpStatusCode.OK, lpd);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
        [Route("{id:int}"), HttpGet]
        // GET BY ID
        public HttpResponseMessage SelectById(int id)
        {
            try
            {
                UserDashFb resp = new UserDashFb();
                resp = _facebookDbService.SelectById(id);
                return Request.CreateResponse(HttpStatusCode.OK, resp);
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
                FbFans2 report = new FbFans2();

                report.Current_Value = dataReport.Data[0].Values[28].Value;
                report.Current = dataReport.Data[0].Values[28].End_Time.ToString("D");
                report.Past_Value = dataReport.Data[0].Values[0].Value;
                report.Past = dataReport.Data[0].Values[0].End_Time.ToString("D");

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
                FbTalkReport2 report = new FbTalkReport2();

                //change data array number for day, week, or month interval since day of the values array number
                // [0] is day; [1] is week; [2] is month previous since date of values[x]
                report.Talk_Current = dataReport.Data[2].Values[28].Value.Sum(x => x.Value);
                report.Date_Current = dataReport.Data[2].Values[28].End_Time.ToString("D");
                report.Country_Talk_Current = dataReport.Data[2].Values[28].Value;

                report.Talk_Month_Ago = dataReport.Data[2].Values[0].Value.Sum(x => x.Value);
                report.Date_Month_Ago = dataReport.Data[2].Values[0].End_Time.ToString("D");
                report.Country_Talk_Month_Ago = dataReport.Data[2].Values[0].Value;

                return Request.CreateResponse(report);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }

        }
        [Route("dashboard"), HttpPost]
        // POST
        public HttpResponseMessage UserDash(UserDashAddRequest model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            try
            {
                _facebookDbService.InsertUserDashboard(model);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}