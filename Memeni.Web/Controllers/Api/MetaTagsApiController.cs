using Memeni.Models.Domain;
using Memeni.Models.Responses;
using Memeni.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Memeni.Web.Controllers.Api
{
    [RoutePrefix("api/tags")]
    public class MetaTagsApiController : ApiController
    {
        private IMetaTagsService _metaTagsService;

        public MetaTagsApiController(IMetaTagsService metaTagsService)
        {
            _metaTagsService = metaTagsService;
        }

        // GET api/<controller>
        [Route(), HttpGet]
        public HttpResponseMessage Get()
        {
            try
            {
                ItemsResponse<MetaTags> response = new ItemsResponse<MetaTags>();
                response.Items = _metaTagsService.Get();
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}
