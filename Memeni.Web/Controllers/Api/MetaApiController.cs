using Memeni.Models.Domain;
using Memeni.Models.Requests;
using Memeni.Models.Responses;
using Memeni.Services.Interfaces;
using Memeni.Services.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace Memeni.Web.Controllers.Api
{
    [RoutePrefix("api/meta")]
    public class MetaApiController : ApiController
    {
        private IPageMetaTagsService _pageMetaTagsService;

        public MetaApiController(IPageMetaTagsService pageMetaTagsService)
        {
            _pageMetaTagsService = pageMetaTagsService;
        }

        // GET api/<controller>
        [Route(), HttpGet]
        public HttpResponseMessage Get()
        {
            try
            {
                ItemsResponse<PageMetaTags> response = new ItemsResponse<PageMetaTags>();
                response.Items = _pageMetaTagsService.Get();
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        // GET api/<controller>/<id>
        [Route("{id:int}"), HttpGet]
        public HttpResponseMessage Get(int id)
        {
            try
            {
                ItemResponse<PageMetaTags> response = new ItemResponse<PageMetaTags>();
                response.Item = _pageMetaTagsService.Get(id);
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        // GET api/<controller>/url/<id>
        [Route("url/{id:int}"), HttpGet] 
        public HttpResponseMessage GetByUrlId(int id)
        {
            try
            {
                ItemsResponse<PageMetaTags> response = new ItemsResponse<PageMetaTags>();
                response.Items = _pageMetaTagsService.GetByUrlId(id);
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        // POST api/<controller>
        [Route("{id:int}"), HttpPost]
        public HttpResponseMessage Insert(PageMetaTagsAddRequest model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            try
            {
                //var u = HttpContext.Current.User.Identity.GetCurrentUser();
                _pageMetaTagsService.Insert(model);
                return Request.CreateResponse(HttpStatusCode.OK, new SuccessResponse());
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        // PUT api/<controller>/value/<id>
        [Route("value/{id:int}"), HttpPut]
        public HttpResponseMessage UpdateValue(PageMetaTagsUpdateValueRequest model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            try
            {
                _pageMetaTagsService.UpdateValue(model);
                return Request.CreateResponse(HttpStatusCode.OK, new SuccessResponse());
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        // DELETE api/<controller>/<id>
        [Route("{id:int}"), HttpDelete]
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                _pageMetaTagsService.Delete(id);
                return Request.CreateResponse(HttpStatusCode.OK, new SuccessResponse());
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}