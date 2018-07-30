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
    [RoutePrefix("api/profile/person")]
    public class ProfilePeopleApiController : ApiController
    {
        private IProfilePersonService _profilePersonService;

        public ProfilePeopleApiController(IProfilePersonService profilePersonService)
        {
            _profilePersonService = profilePersonService;
        }

        // POST api/<controller>
        [Route("{userId:int}"), HttpPost]
        public HttpResponseMessage PostProfilePerson(ProfilePersonRequest model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                _profilePersonService.Insert(model);
                return Request.CreateResponse(HttpStatusCode.OK, new SuccessResponse());        
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        // PUT api/<controller>
        [Route("{userId:int}"), HttpPut]
        public HttpResponseMessage UpdateProfilePerson(ProfilePersonRequest model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                _profilePersonService.Update(model);
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
                ItemResponse<ProfilePerson> response = new ItemResponse<ProfilePerson>();
                response.Item = _profilePersonService.Get(userId);

                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

    }
}
