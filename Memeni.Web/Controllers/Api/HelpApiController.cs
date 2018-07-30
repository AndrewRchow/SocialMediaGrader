using System;
using System.Collections.Generic;
using System.Linq;
using Memeni.Services;
using Memeni.Models.Requests;
using Memeni.Models.Responses;
using Memeni.Models.Domain;
using Memeni.Services.Interfaces;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Memeni.Web.Controllers.Api
{
    [RoutePrefix("api/help")]
    public class HelpApiController : ApiController
    {
        private IHelpService _helpService;

        public HelpApiController(IHelpService helpService)
        {
            _helpService = helpService;
        }

        // Insert
        [Route(), HttpPost]
        public HttpResponseMessage Insert(HelpAddRequest model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            try
            {
                ItemResponse<int> resp = new ItemResponse<int>();
                resp.Item = _helpService.Insert(model);
                return Request.CreateResponse(HttpStatusCode.OK, resp);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        // Get All
        [Route(), HttpGet]
        public HttpResponseMessage Get()
        {
            try
            {
                ItemsResponse<Help> resp = new ItemsResponse<Help>();
                resp.Items = _helpService.Get();
                return Request.CreateResponse(HttpStatusCode.OK, resp);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        // Get By Id
        [Route("{id:int}"), HttpGet]
        public HttpResponseMessage Get(int id)
        {
            try
            {
                ItemResponse<Help> resp = new ItemResponse<Help>();
                resp.Item = _helpService.Get(id);
                return Request.CreateResponse(HttpStatusCode.OK, resp);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        // Get By Url
        [Route("items"), HttpPost]
        public HttpResponseMessage GetByUrl([FromUri] string pageUrl)
        {
            try
            {
                ItemsResponse<Help> resp = new ItemsResponse<Help>();
                resp.Items = _helpService.GetByUrl(pageUrl);
                return Request.CreateResponse(HttpStatusCode.OK, resp);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        // Update
        [Route("{id:int}"), HttpPut]
        public HttpResponseMessage Update(HelpUpdateRequest model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            try
            {
                _helpService.Update(model);
                return Request.CreateResponse(HttpStatusCode.OK, new SuccessResponse());
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        // Delete
        [Route("{id:int}"), HttpDelete]
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                _helpService.Delete(id);
                return Request.CreateResponse(HttpStatusCode.OK, new SuccessResponse());
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        // Get Category Names
        [Route("categories"), HttpGet]
        public HttpResponseMessage GetCategories()
        {
            try
            {
                ItemsResponse<HelpCategories> resp = new ItemsResponse<HelpCategories>();
                resp.Items = _helpService.GetCategories();
                return Request.CreateResponse(HttpStatusCode.OK, resp);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        // Grid Post
        [Route("grid"), HttpPost]
        public HttpResponseMessage GetGrid(HelpGridRequest model)
        {
            if(!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            try
            {
                ItemResponse<HelpGrid> resp = new ItemResponse<HelpGrid>();
                resp.Item = _helpService.GetGrid(model);
                return Request.CreateResponse(HttpStatusCode.OK, resp);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }

        }
    }
}
