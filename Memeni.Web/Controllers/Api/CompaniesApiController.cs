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
    [RoutePrefix("api/companies")]
    public class CompaniesApiController : ApiController
    {
        private ICompanyService _companyService;

        public CompaniesApiController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        // POST api/<controller>
        [Route(), HttpPost]
        public HttpResponseMessage PostCompany(CompanyAddRequest model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                ItemResponse<int> response = new ItemResponse<int>();
                response.Item = _companyService.Insert(model);

                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        // PUT api/<controller>
        [Route("{id:int}"), HttpPut]
        public HttpResponseMessage UpdateCompany(CompanyUpdateRequest model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                _companyService.Update(model);
                return Request.CreateResponse(HttpStatusCode.OK, new SuccessResponse());
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        // GET api/<controller>
        [Route(), HttpGet]
        public HttpResponseMessage Get()
        {
            try
            {
                ItemsResponse<Company> response = new ItemsResponse<Company>();
                response.Items = _companyService.Get();

                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        // GET BY ID api/<controller>
        [Route("{id:int}"), HttpGet]
        public HttpResponseMessage Get(int id)
        {
            try
            {
                ItemResponse<Company> response = new ItemResponse<Company>();
                response.Item = _companyService.GetById(id);

                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        // DELETE api/<controller>
        [Route("{id:int}"), HttpDelete]
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                _companyService.Delete(id);
                return Request.CreateResponse(HttpStatusCode.OK, new SuccessResponse());
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}