using Memeni.Models.Domain;
using Memeni.Models.Requests;
using Memeni.Models.Responses;
using Memeni.Services;
using Memeni.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Memeni.Web.Controllers.Api
{
    [RoutePrefix("api/faqcategory")]
    public class FaqCategoryApiController : ApiController
    {
        private IFaqCategoryService _faqCategoryService;
        private IErrorLogService _errorService;

        public FaqCategoryApiController(IFaqCategoryService faqCategoryService, IErrorLogService errorService)
        {
            _faqCategoryService = faqCategoryService;
            _errorService = errorService;
        }
        [Route(), HttpGet]
        public HttpResponseMessage Get()
        {
            try
            {
                ItemsResponse<FaqCategory> response = new ItemsResponse<FaqCategory>();
                response.Items = _faqCategoryService.Get();
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                _errorService.LogError(ex);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
        [Route("{id:int}"), HttpGet]
        public HttpResponseMessage GetById(int id)
        {
            try
            {
                ItemResponse<FaqCategory> response = new ItemResponse<FaqCategory>();
                response.Item = _faqCategoryService.GetById(id);


                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                _errorService.LogError(ex);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
        [Route, HttpPost]
        public HttpResponseMessage Insert(FaqCategoryAddRequest model)

        {
            try
            {
                ItemResponse<int> response = new ItemResponse<int>();
                response.Item = _faqCategoryService.Insert(model);

                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                _errorService.LogError(ex);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
        [Route("{id:int}"), HttpPut]
        public HttpResponseMessage Update(FaqCategoryUpdateRequest model)
        {
            try
            {
                _faqCategoryService.Update(model);
                return Request.CreateResponse(HttpStatusCode.OK, new SuccessResponse());
            }
            catch (Exception ex)
            {
                _errorService.LogError(ex);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
        [Route("{id:int}"), HttpDelete]
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                _faqCategoryService.Delete(id);
                return Request.CreateResponse(HttpStatusCode.OK, new SuccessResponse());
            }
            catch (Exception ex)
            {
                _errorService.LogError(ex);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}
