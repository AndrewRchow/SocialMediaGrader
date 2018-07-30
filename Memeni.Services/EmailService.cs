using Memeni.Data;
using Memeni.Data.Providers;
using Memeni.Models.Domain;
using Memeni.Models.Requests;
using Memeni.Services.Interfaces;
using Newtonsoft.Json.Linq;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.Script.Serialization;

namespace Memeni.Services
{
    public class EmailService : BaseService, IEmailService
    {
        private IConfigService _configService;
        private IErrorLogService _errorLogService;
        private IFacebookService _facebookService;
        private ITwitterService _twitterService;

        public EmailService(IDataProvider dataProvider, IConfigService configService, IErrorLogService errorLogService, IFacebookService facebookService, ITwitterService twitterService) : base(dataProvider)
        {
            _configService = configService;
            _errorLogService = errorLogService;
            _facebookService = facebookService;
            _twitterService = twitterService;
        }

        // Contact us email
        public async Task ContactEmail(EmailMessageAddRequest model)
        {
            await SendContactEmail(model);
        }

        public async Task SendContactEmail(EmailMessageAddRequest model)
        {
            var ToEmail = _configService.getConfigValusAsString("AdminEmail");
            var ToName = _configService.getConfigValusAsString("AdminName");
            var apiKey = _configService.getConfigValusAsString("SendGridKey");
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress(model.FromEmail, model.FromName);
            var subject = model.Subject;
            var plainTextContent = model.EmailBody;
            var to = new EmailAddress(ToEmail, ToName); 
            var htmlContent = "<h3>(From Contact Us Page On Social Media Grader)</h3>" + plainTextContent;
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
        }

        //Verification Email Services
        public EmailCode SelectById(int id)
        {
            EmailCode singleItem = new EmailCode();

            this.DataProvider.ExecuteCmd("dbo.EmailVerification_SelectByID"
               , inputParamMapper: delegate (SqlParameterCollection paramCollection)
               {
                   paramCollection.AddWithValue("@UserId", id);
               }
               , singleRecordMapper: delegate (IDataReader reader, short set)
               {
                   int startingIndex = 0; //startingOrdinal

                   singleItem.UserId = reader.GetSafeInt32(startingIndex++);
                   singleItem.Code = reader.GetSafeString(startingIndex++);
               });
            return singleItem;
        }

        //Confirm Email Send Grid
        public async Task SendEmailConfirmation(string email, string guid)
        {
            await SendConfirmEmail(email, guid);
        }
        public async Task SendConfirmEmail(string email, string guid)
        {
            string Template = File.ReadAllText(Path.Combine(HostingEnvironment.MapPath("~/Content/EmailTemplates"),"ConfirmEmail.html"));

            //Replace {{ConfirmLink}} and {{Guid}}
            Template = Template.Replace("{{ConfirmLink}}", _configService.getConfigValusAsString("ConfirmLink"));
            Template = Template.Replace("{{Guid}}", guid.ToString());

            var FromEmail = _configService.getConfigValusAsString("SupportEmail");
            var FromName = _configService.getConfigValusAsString("SupportName");
            var apiKey = _configService.getConfigValusAsString("SendGridKey");
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress(FromEmail, FromName); 
            var subject = _configService.getConfigValusAsString("ConfirmEmailSubject");
            var to = new EmailAddress(email);
            var plainTextContent = "";
            var htmlContent = Template;
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);

        }

        public void DeleteById(int id)
        {
            this.DataProvider.ExecuteNonQuery("dbo.EmailVerification_DeleteById"
               , inputParamMapper: delegate (SqlParameterCollection paramCollection)
               {
                   paramCollection.AddWithValue("@UserId", id);
               });
        }

        //Forgot Password Email Services
        public EmailCode SelectForgotPwById(string email)
        {
            EmailCode singleItem = new EmailCode();

            this.DataProvider.ExecuteCmd("dbo.ResetPw_SelectByID"
               , inputParamMapper: delegate (SqlParameterCollection paramCollection)
               {
                   paramCollection.AddWithValue("@Email", email);
               }
               , singleRecordMapper: delegate (IDataReader reader, short set)
               {
                   int startingIndex = 0; //startingOrdinal

                   singleItem.UserId = reader.GetSafeInt32(startingIndex++);
                   singleItem.Code = reader.GetSafeString(startingIndex++);
               });
            return singleItem;
        }

        //Forgot Password Send Grid
        public async Task ForgotPwEmail(string email, string code)
        {
            await SendForgotPw(email, code);
        }
        public async Task SendForgotPw(string email, string code)
        {
            string Template = File.ReadAllText(Path.Combine(HostingEnvironment.MapPath("~/Content/EmailTemplates"),"ForgotPassword.html"));

            //replace {{ResetLink}} and {{ResetCode}} 
            Template = Template.Replace("{{ResetLink}}", _configService.getConfigValusAsString("ResetLink"));
            Template = Template.Replace("{{ResetCode}}", code.ToString());

            var FromEmail = _configService.getConfigValusAsString("SupportEmail");
            var FromName = _configService.getConfigValusAsString("SupportName");
            var apiKey = _configService.getConfigValusAsString("SendGridKey"); 
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress(FromEmail, FromName); 
            var subject = _configService.getConfigValusAsString("ForgotPasswordSubject");
            var to = new EmailAddress(email);
            var plainTextContent = "";
            var htmlContent = Template;
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
        }

        // Email message to database
        public int Insert(EmailMessageAddRequest model)
        {
            int id = 0;
            DataProvider.ExecuteNonQuery("dbo.EmailMessage_Insert"
                , inputParamMapper: delegate (SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@ToEmail", model.ToEmail);
                    paramCollection.AddWithValue("@ToName", model.ToName);
                    paramCollection.AddWithValue("@FromEmail", model.FromEmail);
                    paramCollection.AddWithValue("@FromName", model.FromName);
                    paramCollection.AddWithValue("@Subject", model.Subject);
                    paramCollection.AddWithValue("@EmailBody", model.EmailBody);

                    SqlParameter idParameter = new SqlParameter("@Id", System.Data.SqlDbType.Int);
                    idParameter.Direction = System.Data.ParameterDirection.Output;

                    paramCollection.Add(idParameter);
                },
                returnParameters: delegate (SqlParameterCollection param)
                {
                    Int32.TryParse(param["@Id"].Value.ToString(), out id);
                });
            return id;
        }


        // Hangfire Task for daily error email
        public async Task ErrorEmail()
        {
            await ScheduleErrorEmail();
        }

        public async Task ScheduleErrorEmail()
        {
            string Template = File.ReadAllText(Path.Combine(HostingEnvironment.MapPath("~/Content/EmailTemplates"),"ErrorReport.html"));

            Template = Template.Replace("{{ErrorLink}}", _configService.getConfigValusAsString("ErrorLink"));

            // Call get severity and add to template
            var ErrorList = _errorLogService.GetSeverity();

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < ErrorList.Count; i++)
            {
                sb.Append("<br/>");
                if (i == 0)
                {
                    sb.Append(string.Format("There are {0} info errors", ErrorList[i].ErrorCount));
                }
                else if (i == 1)
                {
                    sb.Append(string.Format("There are {0} warnings", ErrorList[i].ErrorCount));
                }
                else if (i == 2)
                {
                    sb.Append(string.Format("There are {0} errors", ErrorList[i].ErrorCount));
                }
                else if (i == 3)
                {
                    sb.Append(string.Format("There are {0} critical errors", ErrorList[i].ErrorCount));
                }
            }
            Template = Template.Replace("{{Errors}}", sb.ToString());

            var FromEmail = _configService.getConfigValusAsString("FromEmailErrorReport");
            var FromName = _configService.getConfigValusAsString("FromNameSendGrid");
            var ToEmail = _configService.getConfigValusAsString("AdminEmail");
            var ToName = _configService.getConfigValusAsString("AdminName");
            var apiKey = _configService.getConfigValusAsString("SendGridKey");
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress(FromEmail, FromName);
            var subject = _configService.getConfigValusAsString("ErrorReportSubject"); 
            var plainTextContent = ""; 
            var to = new EmailAddress(ToEmail, ToName); // send this to admin for errors
            var htmlContent = Template;// send list of errors here - go make a database call to gather information that will fill the body of the email.
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
        }

        // Weekly Reports
        public async Task ReportEmail(ReportAddRequest model)
        {
            await ScheduleReportEmail(model);
        }

        public async Task ScheduleReportEmail(ReportAddRequest model)
        {
            // Report Template
            string Template = File.ReadAllText(Path.Combine(HostingEnvironment.MapPath("~/Content/EmailTemplates"),"FacebookReport.html"));

            Template = Template.Replace("{{ReportLink}}", _configService.getConfigValusAsString("ReportLink"));

            if (model.Url == "twitter")
            {
                var jsonReport = _twitterService.GetTwitterReport(model.Name);

                JavaScriptSerializer js = new JavaScriptSerializer();
                TwitterReport sData = js.Deserialize<TwitterReport>(jsonReport);

                string overallGrade = sData.OverallGrade;
                string engGrade = sData.EngGrade;
                string actGrade = sData.ActGrade;
                string growGrade = sData.GrowGrade;
                string reachGrade = sData.ReachGrade;
                string dataGrade = sData.DataGrade;

                Template = Template.Replace("{{FirstName}}", model.FirstName);
                Template = Template.Replace("{{OverallScore}}", overallGrade.ToString());
                Template = Template.Replace("{{EngagementScore}}", engGrade.ToString());
                Template = Template.Replace("{{ActivityScore}}", actGrade.ToString());
                Template = Template.Replace("{{GrowthScore}}", growGrade.ToString());
                Template = Template.Replace("{{ReachScore}}", reachGrade.ToString());
                Template = Template.Replace("{{DataScore}}", dataGrade.ToString());
                
            }
            else if (model.Url == "facebook")
            {

                ///////////////////
                //Facebook Report//
                ///////////////////
                string token = _configService.getConfigValusAsString("FbAppAuthToken"); // "340834969679482|VZHv9QJUimMQxf0Z3GDYN6EGFX4";
                string site = model.Name;

                //Fb Profile
                string profile_item = _facebookService.GetFbProfile(token, site);
                var json = new JavaScriptSerializer();
                FbProfileAddRequest profile = json.Deserialize<FbProfileAddRequest>(profile_item);
                string userName = profile.Name;
                int totalLikes = profile.Engagement.Count;
                int talkCount = profile.Talking_About_Count;
                bool verified = profile.Is_Verified;

                //Fb Feed
                int feedPerCall = _configService.getConfigValueAsInt("FbAuthFeedFeedPerCall");//50;
                int daysInTimeFrame = _configService.getConfigValueAsInt("FbAuthFeedDaysInTimeFrame");// 7;

                string feed_item = _facebookService.GetFbFeed(token, site, feedPerCall);
                FeedData fbFeed = new JavaScriptSerializer().Deserialize<FeedData>(feed_item);

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

                double engScore = ((double)(sharesCount + likesCount + commentsCount + reactionsCount) / (double)totalLikes * 100);
                int engGrade;
                if (engScore > 3)
                {
                    engGrade = 25;
                }
                else if (engScore >= 2.5)
                {
                    engGrade = 22;
                }
                else if (engScore >= 1.5)
                {
                    engGrade = 15;
                }
                else if (engScore >= 0.5)
                {
                    engGrade = 8;
                }
                else if (engScore > 0)
                {
                    engGrade = 3;
                }
                else
                {
                    engGrade = 0;
                }

                //Fb Activity
                int feedPerCallA = _configService.getConfigValueAsInt("FbAuthActivityFeedPerCall");// 25;
                int daysInTimeFrameA = _configService.getConfigValueAsInt("FbAuthActivityDaysInTimeFrame");//30;
                var timeFrameA = DateTime.Today.AddDays(-daysInTimeFrameA);
                int actGrade = 0;

                string item = _facebookService.GetFbFeed(token, site, feedPerCall);
                FeedData fbFeedA = new JavaScriptSerializer().Deserialize<FeedData>(item);

                if (fbFeedA.Feed.Data[24].Created_Time > timeFrameA)
                {
                    actGrade = 25;
                }
                else if (fbFeedA.Feed.Data[19].Created_Time > timeFrameA)
                {
                    actGrade = 20;
                }
                else if (fbFeedA.Feed.Data[14].Created_Time > timeFrameA)
                {
                    actGrade = 15;
                }
                else if (fbFeedA.Feed.Data[9].Created_Time > timeFrameA)
                {
                    actGrade = 10;
                }
                else if (fbFeedA.Feed.Data[4].Created_Time > timeFrameA)
                {
                    actGrade = 5;
                }
                else
                {
                    actGrade = 0;
                }

                //Fb Growth
                string itemG = _facebookService.GetFbFans(token, site);
                FbFansData dataReport = new JavaScriptSerializer().Deserialize<FbFansData>(itemG);
                int Total_Fans_Current = dataReport.Data[0].Values[28].Value.Sum(x => x.Value);
                int Total_Fans_Month_Ago = dataReport.Data[0].Values[0].Value.Sum(x => x.Value);
                double percentChange = ((double)(Total_Fans_Current - Total_Fans_Month_Ago) / (double)Total_Fans_Month_Ago * 100);
                int growGrade = 0;
                if (percentChange > 1)
                {
                    growGrade = 25;
                }
                else if (percentChange > 0.75)
                {
                    growGrade = 20;
                }
                else if (percentChange > 0.5)
                {
                    growGrade = 15;
                }
                else if (percentChange > 0.25)
                {
                    growGrade = 10;
                }
                else if (percentChange > 0)
                {
                    growGrade = 5;
                }
                else
                {
                    growGrade = 0;
                }

                //Fb Reach
                double reachScore = ((double)talkCount / (double)totalLikes * 100);
                int reachGrade = 0;
                if (reachScore > 2)
                {
                    reachGrade = 10;
                }
                else if (reachScore >= 1.5)
                {
                    reachGrade = 8;
                }
                else if (reachScore >= 1)
                {
                    reachGrade = 6;
                }
                else if (reachScore >= 0.5)
                {
                    reachGrade = 4;
                }
                else if (reachScore >= 0.20)
                {
                    reachGrade = 2;
                }
                else
                {
                    reachGrade = 1;
                }

                int dataGrade = 8;

                if (verified == false)
                {
                    actGrade = Convert.ToInt32((double)actGrade * 0.6);
                    engGrade = Convert.ToInt32((double)engGrade * 0.6);
                    growGrade = Convert.ToInt32((double)growGrade * 0.6);
                    reachGrade = Convert.ToInt32((double)reachGrade * 0.6);
                }
                else
                {
                    actGrade += 3;
                    if (actGrade > 25) { actGrade = 25; };
                    engGrade += 3;
                    if (engGrade > 25) { engGrade = 25; };
                    growGrade += 4;
                    if (growGrade > 25) { growGrade = 25; };
                    reachGrade += 2;
                    if (reachGrade > 10) { reachGrade = 10; };
                }

                int overallGrade = engGrade + actGrade + growGrade + reachGrade + dataGrade;

                Template = Template.Replace("{{FirstName}}", model.FirstName);
                Template = Template.Replace("{{OverallScore}}", overallGrade.ToString());
                Template = Template.Replace("{{EngagementScore}}", engGrade.ToString());
                Template = Template.Replace("{{ActivityScore}}", actGrade.ToString());
                Template = Template.Replace("{{GrowthScore}}", growGrade.ToString());
                Template = Template.Replace("{{ReachScore}}", reachGrade.ToString());
                Template = Template.Replace("{{DataScore}}", dataGrade.ToString());
            }

            var FromEmail = _configService.getConfigValusAsString("FromEmailErrorReport");
            var FromName = _configService.getConfigValusAsString("FromNameSendGrid");
            var ToEmail = model.Email;
            var ToName = _configService.getConfigValusAsString("AdminName");
            var apiKey = _configService.getConfigValusAsString("SendGridKey");
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress(FromEmail, FromName);
            var subject = _configService.getConfigValusAsString("UserReportSubject");
            var plainTextContent = ""; 
            var to = new EmailAddress(ToEmail, ToName); // send this to user for errors
            var htmlContent = Template + plainTextContent; // report template
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
        }
    }
}
