using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Memeni.Services
{
    public class FacebookService : BaseService, IFacebookService
    {
        public string GetAppAuth()
        {
            WebClient client = new WebClient();
            String URI = "https://graph.facebook.com/oauth/access_token?client_id=340834969679482&client_secret=f64d16a2987194e1b1d6587c3d1ffd8e&grant_type=client_credentials";

            string fbResponse = client.DownloadString(URI);
            
            int startIndex = fbResponse.IndexOf(":") + 2;
            int endIndex = fbResponse.IndexOf(",") - 1;
            String authToken = fbResponse.Substring(startIndex, endIndex - startIndex);

            return authToken;
        }

        public string GetFbProfile(string token, string site)
        {
            WebClient client = new WebClient();
            String link = "https://graph.facebook.com/" + site + "?fields=name,is_verified,picture,cover,engagement,talking_about_count&access_token=" + token;

            string response = string.Empty;
            try
            {
                response = client.DownloadString(link);
            }
            catch (Exception ex)
            {
                response = "An error occurred: " + ex;
            }

            return response;
        }

        public string GetFbFeed(string token, string site, int number)
        {
            WebClient client = new WebClient();
            String link = "https://graph.facebook.com/" + site + "?fields=feed.limit(" + number + "){created_time}&access_token=" + token;

            string response = client.DownloadString(link);

            return response;
        }

        public string GetFbFeedNext(string site)
        {
            WebClient client = new WebClient();
            string response = client.DownloadString(site);
            return response;
        }

        public string GetPostData(string token, string post)
        {
            WebClient client = new WebClient();
            String link = "https://graph.facebook.com/" + post + "?fields=shares,likes.limit(0).summary(true),comments.limit(0).summary(true),reactions.limit(0).summary(true)&access_token=" + token;
            string response = client.DownloadString(link);

            return response;
        }

        public string GetFbFans(string token, string site)
        {
            WebClient client = new WebClient();
            String link = "https://graph.facebook.com/" + site + @"/insights?access_token=" + token + "&metric=page_fans_country&date_preset=last_30d";
            string response = client.DownloadString(link);

            return response;
        }

        public string GetFbTalking(string token, string site)
        {
            WebClient client = new WebClient();
            String link = "https://graph.facebook.com/" + site + @"/insights?access_token=" + token + "&metric=page_storytellers_by_country&date_preset=last_30d";
            string response = client.DownloadString(link);

            return response;
        }
    }
}
