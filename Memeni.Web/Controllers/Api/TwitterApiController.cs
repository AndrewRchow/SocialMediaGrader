using Memeni.Models.Domain;
using Memeni.Models.Responses;
using Memeni.Services;
using Memeni.Services.Interfaces;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace Memeni.Web.Controllers.Api
{
    [RoutePrefix("api/twitter")]
    public class TwitterApiController : ApiController
    {
        private ITwitterService _twitterService;
        private IConfigService _configService;

        public TwitterApiController(ITwitterService twitterService, IConfigService configService)
        {
            _twitterService = twitterService;
            _configService = configService;
        }

        [Route(), HttpPost]
        public HttpResponseMessage PostBearerToken()
        {
            string consumerKey = _configService.getConfigValusAsString("TwitterConsumerKey");
            string consumerSecret = _configService.getConfigValusAsString("TwitterConsumerSecret");
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(consumerKey + ":" + consumerSecret);
            var encodedKey = System.Convert.ToBase64String(plainTextBytes);
            try
            {
                string response = _twitterService.PostBearerToken(encodedKey);
                JObject jResponse = JObject.Parse(response);
                string token = jResponse["access_token"].ToString();
                return Request.CreateResponse(HttpStatusCode.OK, token);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [Route("{userName}"), HttpGet]
        public HttpResponseMessage GetUserTimeline(string userName)
        {
            string screenName = userName;
            try
            {
                string response = _twitterService.GetUserTimeline(screenName);
                JArray jResponse = JArray.Parse(response);
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [Route("{userName}/{id}"), HttpGet]
        public HttpResponseMessage GetUserTimelineNext(string userName, string id)
        {
            string screenName = userName;
            try
            {
                string response = _twitterService.GetUserTimelineNext(screenName, id);
                JArray jResponse = JArray.Parse(response);
                return Request.CreateResponse(HttpStatusCode.OK, jResponse);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [Route("followers/{id}"), HttpGet]
        public HttpResponseMessage GetFollowers(string id)
        {
            try
            {
                string response = _twitterService.GetFollowers(id);
                JObject jResponse = JObject.Parse(response);
                return Request.CreateResponse(HttpStatusCode.OK, jResponse);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [Route("search/{userName}"), HttpGet]
        public HttpResponseMessage GetSearch(string userName)
        {
            string screenName = userName;
            try
            {
                string response = _twitterService.GetSearch(screenName);
                JObject jResponse = JObject.Parse(response);
                return Request.CreateResponse(HttpStatusCode.OK, jResponse);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [Route("report/{userName}"), HttpGet]
        public HttpResponseMessage GetTwitterReport(string userName)
        {
            string screenName = userName;
            try
            {
                string response = _twitterService.GetTwitterReport(screenName);
                try
                {
                    JObject jResponse = JObject.Parse(response);
                    return Request.CreateResponse(jResponse);
                }
                catch
                {
                    return Request.CreateResponse(response);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}
