using Memeni.Models.Domain.Profile;
using Memeni.Models.Responses;
using Memeni.Services;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Memeni.Web.Controllers.Api.Profile
{
    [RoutePrefix("api/profile")]
    public class ProfileApiController : ApiController
    {
        private IProfileService _profileService;

        public ProfileApiController(IProfileService profileService)
        {
            _profileService = profileService;
        }

        // GET BY ID api/<controller>
        [Route("{id:int}"), HttpGet]
        public HttpResponseMessage GetById(int id)
        {
            try
            {
                ItemResponse<UserProfile> response = new ItemResponse<UserProfile>();
                response.Item = _profileService.GetById(id);

                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}
