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
    [RoutePrefix("api/profile/phone")]
    public class ProfilePhoneApiController : ApiController
    {
        private IProfilePhoneService _profilePhoneService;

        public ProfilePhoneApiController(IProfilePhoneService profilePhoneService)
        {
            _profilePhoneService = profilePhoneService;
        }

        // POST api/<controller>
        [Route("{userId:int}"), HttpPost]
        public HttpResponseMessage PostProfilePhone(ProfilePhoneRequest model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                _profilePhoneService.Insert(model);
                return Request.CreateResponse(HttpStatusCode.OK, new SuccessResponse());
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        // PUT api/<controller>
        [Route("{userId:int}"), HttpPut]
        public HttpResponseMessage UpdateProfilePhone(ProfilePhoneRequest model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                _profilePhoneService.Update(model);
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
                ItemResponse<ProfilePhone> response = new ItemResponse<ProfilePhone>();
                response.Item = _profilePhoneService.Get(userId);

                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}
