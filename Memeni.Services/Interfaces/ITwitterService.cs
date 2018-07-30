using Memeni.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memeni.Services.Interfaces
{
    public interface ITwitterService
    {
        string PostBearerToken(string encodedKey);
        string GetUserTimeline(string screenName);
        string GetUserTimelineNext(string screenName, string id);
        string GetFollowers(string id);
        string GetSearch(string screenName);
        string GetTwitterReport(string screenName);
    }
}
