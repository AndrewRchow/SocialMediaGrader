using System.Net.Http;
using System.Threading.Tasks;

namespace Memeni.Services
{
    public interface IFacebookService
    {
        string GetAppAuth();
        string GetFbProfile(string token, string site);
        string GetFbFeed(string token, string site, int number);
        string GetFbFeedNext(string site);
        string GetPostData(string token, string post);
        string GetFbFans(string token, string site);
        string GetFbTalking(string token, string site);
    }
}