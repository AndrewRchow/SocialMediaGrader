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
    [RoutePrefix("api/helpcategories")]
    public class HelpCategoriesApiController : ApiController
    {
        private IHelpCategoriesService _helpCategoriesService;

        public HelpCategoriesApiController(IHelpCategoriesService helpCategoriesService)
        {
            _helpCategoriesService = helpCategoriesService;
        }

        // Insert
        [Route(), HttpPost]
        public HttpResponseMessage Insert(HelpCategoriesAddRequest model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            try
            {
                ItemResponse<int> resp = new ItemResponse<int>();
                resp.Item = _helpCategoriesService.Insert(model);
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
                ItemsResponse<HelpCategories> resp = new ItemsResponse<HelpCategories>();
                resp.Items = _helpCategoriesService.Get();
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
                ItemResponse<HelpCategories> resp = new ItemResponse<HelpCategories>();
                resp.Item = _helpCategoriesService.Get(id);
                return Request.CreateResponse(HttpStatusCode.OK, resp);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        // Update
        [Route("{id:int}"), HttpPut]
        public HttpResponseMessage Update(HelpCategoriesUpdateRequest model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            try
            {
                _helpCategoriesService.Update(model);
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
                _helpCategoriesService.Delete(id);
                return Request.CreateResponse(HttpStatusCode.OK, new SuccessResponse());
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}