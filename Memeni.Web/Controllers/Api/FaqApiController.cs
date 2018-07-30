using Memeni.Models.Domain;
using Memeni.Models.Requests;
using Memeni.Models.Responses;
using Memeni.Models.ViewModels;
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
    [RoutePrefix("api/faq")]
    public class FaqApiController : ApiController
    {
        private IFaqService _faqService;
        private IErrorLogService _errorService;

        public FaqApiController(IFaqService faqService, IErrorLogService errorService)
        {
            _faqService = faqService;
            _errorService = errorService;
        }
        [Route(), HttpGet]
        public HttpResponseMessage Get()
        {
            try
            {
                List<FaqIndexModel> response = new List<FaqIndexModel>();
                response = _faqService.Get();
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
                ItemResponse<Faq> response = new ItemResponse<Faq>();
                response.Item = _faqService.GetById(id);


                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                _errorService.LogError(ex);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
        [Route, HttpPost]
        public HttpResponseMessage Insert(FaqAddRequest model)

        {
            try
            {
                ItemResponse<int> response = new ItemResponse<int>();
                response.Item = _faqService.Insert(model);

                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                _errorService.LogError(ex);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
        [Route("{id:int}"), HttpPut]
        public HttpResponseMessage Update(FaqUpdateRequest model)
        {
            try
            {
                _faqService.Update(model);
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
                _faqService.Delete(id);
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
