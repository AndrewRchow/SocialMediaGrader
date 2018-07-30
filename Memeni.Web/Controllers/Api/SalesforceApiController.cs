using Hangfire;
using Memeni.Models.Requests;
using Memeni.Models.Responses;
using Memeni.Services;
using Memeni.Services.Proxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Memeni.Web.Controllers.Api
{
    [RoutePrefix("api/salesforce")]
    public class SalesforceApiController : ApiController
    {
        private ISalesforceService _SalesforceService;

        public SalesforceApiController(ISalesforceService SalesforceService)
        {
            _SalesforceService = SalesforceService;
        }

        [Route("facebook"), HttpPost]
        public HttpResponseMessage PostFB(SalesforceAddRequest model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            try
            {
                SalesforceProxyService svc = new SalesforceProxyService();
                ItemResponse<string> resp = new ItemResponse<string>();
                //Ques the job in hangfire so the home page goes to loading page faster - needs hangfire switch TRUE in web.config
                //resp.Item = _SalesforceService.InsertFB(model);
                BackgroundJob.Enqueue(() => svc.InsertFB(model));
                return Request.CreateResponse(HttpStatusCode.OK, resp);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [Route("twitter"), HttpPost]
        public HttpResponseMessage PostTWITT(SalesforceAddRequest model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            try
            {
                SalesforceProxyService svc = new SalesforceProxyService();
                ItemResponse<string> resp = new ItemResponse<string>();
                //Ques the job in hangfire so the home page goes to loading page faster - needs hangfire switch TRUE in web.config
                BackgroundJob.Enqueue(() => svc.InsertTWITT(model));
                return Request.CreateResponse(HttpStatusCode.OK, new SuccessResponse());
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [Route("widget"), HttpPost]
        public HttpResponseMessage PostWidget(SalesforceAddRequest model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            try
            {
                SalesforceProxyService svc = new SalesforceProxyService();
                ItemResponse<string> resp = new ItemResponse<string>();
                //Ques the job in hangfire so the home page goes to loading page faster - needs hangfire switch TRUE in web.config
                BackgroundJob.Enqueue(() => svc.InsertWidget(model));
                return Request.CreateResponse(HttpStatusCode.OK, new SuccessResponse());
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [Route("register"), HttpPost]
        public HttpResponseMessage Register(SalesforceUpdateRequest model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            try
            {
                SalesforceProxyService svc = new SalesforceProxyService();
                ItemResponse<string> resp = new ItemResponse<string>();
                //Ques the job in hangfire so the home page goes to loading page faster - needs hangfire switch TRUE in web.config
                BackgroundJob.Enqueue(() => svc.Register(model));
                return Request.CreateResponse(HttpStatusCode.OK, new SuccessResponse());
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}