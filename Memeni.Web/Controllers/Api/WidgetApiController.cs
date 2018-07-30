using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Memeni.Services.Interfaces;
using Memeni.Services;
using System.Web.Script.Serialization;
using System.Net.Http;
using System.Net;

namespace Memeni.Web.Controllers.Api
{
    [RoutePrefix("api/widget")]
    public class WidgetApiController : ApiController
    {
        private IWidgetService _widgetService;
        private IConfigService _configService;

        public WidgetApiController(IWidgetService widgetService, IConfigService configService)
        {
            _widgetService = widgetService;
            _configService = configService;
        }

        //Gets profile info for the user
        [Route("profile/{name}"), HttpGet]
        public HttpResponseMessage Profile(string name)
        {
            //Facebook App Auth Token below
            string token = _configService.getConfigValusAsString("FbAppAuthToken");
            string site = name;
            string item = string.Empty;
            try
            {
                item = _widgetService.GetFbProfile(token, site);
                return Request.CreateResponse(HttpStatusCode.OK, item,Configuration.Formatters.JsonFormatter);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        //Gets profile picture for the user
        [Route("profile/picture/{id}"), HttpGet]
        public HttpResponseMessage Picture(string id)
        {
            //Facebook App Auth Token below
            string token = _configService.getConfigValusAsString("FbAppAuthToken");
            string site = id;
            string item = string.Empty;
            try
            {
                item = _widgetService.GetPicture(token, site);
                return Request.CreateResponse(HttpStatusCode.OK, item,Configuration.Formatters.JsonFormatter);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}