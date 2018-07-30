using Memeni.Models.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Memeni.Services;
using Memeni.Models.Responses;
using Memeni.Models.Domain;
using Memeni.Services.Interfaces;

namespace Memeni.Web.Controllers.Api
{
    [RoutePrefix("api/sme")]
    public class SMEApiController : ApiController
    {
        private ISMEService _SMEService;

        public SMEApiController(ISMEService SMEService)
        {
            _SMEService = SMEService;
        }
        [Route, HttpGet]
        // GET api/sme
        public HttpResponseMessage SelectAll()
        {
            try
            {
                ItemsResponse<SME> resp = new ItemsResponse<SME>();
                resp.Items = _SMEService.SelectAll();
                return Request.CreateResponse(HttpStatusCode.OK, resp);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
        [Route("{id:int}"), HttpGet]
        // GET BY ID api/sme
        public HttpResponseMessage SelectById(int id)
        {
            try
            {
                ItemResponse<SME> resp = new ItemResponse<SME>();
                resp.Item = _SMEService.SelectById(id);
                return Request.CreateResponse(HttpStatusCode.OK, resp);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
        [Route, HttpPost]
        // POST api/sme
        public HttpResponseMessage Insert(SMEAddRequest model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            try
            {
                ItemResponse<int> resp = new ItemResponse<int>();
                resp.Item = _SMEService.Insert(model);

                return Request.CreateResponse(HttpStatusCode.OK, resp);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
        [Route("{id:int}"), HttpPut]
        // PUT api/sme
        public HttpResponseMessage Update(SMEUpdateRequest model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            try
            {
                _SMEService.Update(model);
                return Request.CreateResponse(HttpStatusCode.OK, new SuccessResponse());
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
        [Route("{id:int}"), HttpDelete]
        // DELETE api/sme
        public HttpResponseMessage DeleteById(int id)
        {
            try
            {
                _SMEService.DeleteById(id);
                return Request.CreateResponse(HttpStatusCode.OK, new SuccessResponse());
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}