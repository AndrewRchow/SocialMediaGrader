using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using Memeni.Services.Interfaces;
using System.Threading.Tasks;

namespace Memeni.Services
{
    public class WidgetService : BaseService, IWidgetService
    {
        public string GetFbProfile(string token, string site)
        {
            WebClient client = new WebClient();
            //69 is max value of data you can get at one time
            String link = "https://graph.facebook.com/" + site + "?fields=name,id,fan_count,picture,likes.limit(50)&access_token=" + token;

            string response = client.DownloadString(link);

            return response;
        }

        public string GetPicture(string token, string id)
        {
            WebClient client = new WebClient();
            String link = "https://graph.facebook.com/" + id + "?fields=picture&access_token=" + token;

            string response = client.DownloadString(link);

            return response;
        }
    }
}
