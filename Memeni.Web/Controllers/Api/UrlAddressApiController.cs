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
    [RoutePrefix("api/UrlAddress")]
    public class UrlAddressApiController : ApiController
    {
        private IUrlAddressService _urlAddressService;

        public UrlAddressApiController(IUrlAddressService urlAddressService)
        {
            _urlAddressService = urlAddressService;
        }

        // GET api/<controller>
        [Route(), HttpGet]
        public HttpResponseMessage GetAll()
        {
            try
            {
                ItemsResponse<UrlAddress> response = new ItemsResponse<UrlAddress>();
                response.Items = _urlAddressService.GetAll();

                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        // GET api/<controller>/5
        [Route("{id:int}"), HttpGet]
        public HttpResponseMessage Get(int id)
        {
            try
            {
                ItemResponse<UrlAddress> response = new ItemResponse<UrlAddress>();
                response.Item = _urlAddressService.Get(id);

                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        // POST api/<controller>
        [Route(), HttpPost]
        public HttpResponseMessage Insert(UrlAddressAddRequest model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                ItemResponse<int> response = new ItemResponse<int>();
                response.Item = _urlAddressService.Insert(model);

                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        // PUT api/<controller>/5
        [Route("{id:int}"), HttpPut]
        public HttpResponseMessage Update(UrlAddressUpdateRequest model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                _urlAddressService.Update(model);

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
                _urlAddressService.Delete(id);
                return Request.CreateResponse(HttpStatusCode.OK, new SuccessResponse());
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}