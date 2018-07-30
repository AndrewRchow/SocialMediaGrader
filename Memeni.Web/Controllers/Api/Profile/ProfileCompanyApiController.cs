using Memeni.Models.Domain.Profile;
using Memeni.Models.Requests.Profile;
using Memeni.Models.Responses;
using Memeni.Services;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Memeni.Web.Controllers.Api.Profile
{
    [RoutePrefix("api/profile/company")]
    public class ProfileCompanyApiController : ApiController
    {
        private IProfileCompanyService _profileCompanyService;

        public ProfileCompanyApiController(IProfileCompanyService profileCompanyService)
        {
            _profileCompanyService = profileCompanyService;
        }

        // POST api/<controller>
        [Route("{userId:int}"), HttpPost]
        public HttpResponseMessage PostProfileCompany(ProfileCompanyRequest model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                _profileCompanyService.Insert(model);
                return Request.CreateResponse(HttpStatusCode.OK, new SuccessResponse());
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        // PUT api/<controller>
        [Route("{userId:int}"), HttpPut]
        public HttpResponseMessage UpdateProfileCompany(ProfileCompanyRequest model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                _profileCompanyService.Update(model);
                return Request.CreateResponse(HttpStatusCode.OK, new SuccessResponse());
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        // GET BY ID api/<controller>
        [Route("{userId:int}"), HttpGet]
        public HttpResponseMessage Get(int userId)
        {
            try
            {
                ItemResponse<ProfileCompany> response = new ItemResponse<ProfileCompany>();
                response.Item = _profileCompanyService.Get(userId);

                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}
