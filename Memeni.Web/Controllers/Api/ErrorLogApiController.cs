using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Hangfire;
using Memeni.Models.Domain;
using Memeni.Models.Requests;
using Memeni.Models.Responses;
using Memeni.Services;
using Memeni.Services.Interfaces;

namespace Memeni.Web.Controllers.Api
{
    [RoutePrefix("api/errors")]
    public class ErrorLogApiController : ApiController
    {
        private IErrorLogService _errorService;
        public ErrorLogApiController(IErrorLogService errorService)
        {
            _errorService = errorService;
        }

        public ErrorLogApiController()
        {
            _errorService = new ErrorLogService();
        }

        // GET api/<controller>
        [Route(), HttpGet]
        public HttpResponseMessage GetAll()
        {
            try
            {
                ItemsResponse<ErrorLog> resp = new ItemsResponse<ErrorLog>();
                resp.Items = _errorService.GetAll();
                return Request.CreateResponse(HttpStatusCode.OK, resp);
            }
            catch (Exception ex)
            {
                _errorService.LogError(ex);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        // GET api/<controller>/5
        [Route("id/{id:int}"), HttpGet]
        public HttpResponseMessage Get(int id)
        {
            try
            {
                ItemResponse<ErrorLog> resp = new ItemResponse<ErrorLog>();
                resp.Item = _errorService.Get(id);
                return Request.CreateResponse(HttpStatusCode.OK, resp);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        // POST api/<controller>
        [Route(), HttpPost]
        public HttpResponseMessage Add(ErrorLogAddRequest model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            try
            {
                ItemResponse<int> resp = new ItemResponse<int>();
                resp.Item = _errorService.Add(model);
                return Request.CreateResponse(HttpStatusCode.OK, resp);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        //Hangfire Schedule - Minutely
        [Route("hangfire"), HttpPost]
        public HttpResponseMessage Schedule(ErrorLogAddRequest model)
        {
            RecurringJob.AddOrUpdate(() => _errorService.Schedule(model), Cron.Minutely);
            return Request.CreateResponse(HttpStatusCode.OK, new SuccessResponse());
        }

        // PUT api/<controller>/5
        [Route("{id:int}"), HttpPut]
        public HttpResponseMessage Put(ErrorLogUpdateRequest model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            try
            {
                _errorService.Put(model);
                return Request.CreateResponse(HttpStatusCode.OK, new SuccessResponse());
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        // DELETE api/<controller>/5
        [Route("{id:int}"), HttpDelete]
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                _errorService.Delete(id);
                return Request.CreateResponse(HttpStatusCode.OK, new SuccessResponse());
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        // Paging Sorting and Searching for grid
        [Route("grid"), HttpPost]
        public HttpResponseMessage GetGrid(ErrorLogGridRequest model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            try
            {
                ItemResponse<ErrorLogGrid> resp = new ItemResponse<ErrorLogGrid>();
                resp.Item = _errorService.GetGrid(model);
                return Request.CreateResponse(HttpStatusCode.OK, resp);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        // Truncate Table
        [Route("Truncate"), HttpDelete]
        public HttpResponseMessage Truncate()
        {
            try
            {
                _errorService.Truncate();
                return Request.CreateResponse(HttpStatusCode.OK, new SuccessResponse());
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        // Delete Multiple
        [Route("DeleteSelected"), HttpDelete]
        public HttpResponseMessage MultiDelete([FromUri] int[] ids)
        {
            try
            {
                _errorService.MultiDelete(ids);
                return Request.CreateResponse(HttpStatusCode.OK, new SuccessResponse());
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}