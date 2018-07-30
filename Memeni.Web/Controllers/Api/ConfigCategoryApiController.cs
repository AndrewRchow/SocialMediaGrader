using Memeni.Models.Domain;
using Memeni.Models.Requests;
using Memeni.Models.Responses;
using Memeni.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Memeni.Web.Controllers.Api
{
    [RoutePrefix("api/configcategory")]
    public class ConfigCategoryApiController : ApiController
    {
        private IConfigCategoryService _configCategoryService;

        public ConfigCategoryApiController(IConfigCategoryService configCategoryService)
        {
            _configCategoryService = configCategoryService;
        }

        [Route(), HttpGet]
        public HttpResponseMessage Get()
        {
            try
            {
                ItemsResponse<ConfigCategory> response = new ItemsResponse<ConfigCategory>();
                response.Items = _configCategoryService.Get();
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [Route("{Id:int}"), HttpGet]
        public HttpResponseMessage Get(int Id)
        {
            try
            {
                ItemResponse<ConfigCategory> response = new ItemResponse<ConfigCategory>();
                response.Item = _configCategoryService.Get(Id);

                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [Route(), HttpPost]
        public HttpResponseMessage Insert(ConfigCategoryAddRequest model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                ItemResponse<int> response = new ItemResponse<int>();
                response.Item = _configCategoryService.Insert(model);

                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [Route("{Id:int}"), HttpPut]
        public HttpResponseMessage Update(ConfigCategoryUpdateRequest model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                _configCategoryService.Update(model);
                return Request.CreateResponse(HttpStatusCode.OK, new SuccessResponse());
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [Route("{Id:int}"), HttpDelete]
        public HttpResponseMessage Delete(int Id)
        {
            try
            {
                _configCategoryService.Delete(Id);
                return Request.CreateResponse(HttpStatusCode.OK, new SuccessResponse());
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}
