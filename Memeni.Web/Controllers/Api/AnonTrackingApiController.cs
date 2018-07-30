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
using System.Web;
using System.Web.Http;
using System.Web.SessionState;

namespace Memeni.Web.Controllers.Api
{
    [RoutePrefix("api/tracking")]
    public class AnonTrackingApiController : ApiController
    {
        private IAnonTrackingService _AnonTrackingService;

        public AnonTrackingApiController(IAnonTrackingService AnonTrackingService)
        {
            _AnonTrackingService = AnonTrackingService;
        }

        [Route(), HttpPost]
        //Called when user grades a url
        public HttpResponseMessage Track(AnonUserChangeRequest model)
        {
            //If anonymous email used before
            if (_AnonTrackingService.SelectUserByEmail(model.Email) != null)
            {
                //If no current url cookie, then user is using a new session, increase visit count by 1
                if (model.Session ==null)
                {
                    _AnonTrackingService.IncreaseVisitCount(model);
                }

                //If Url graded and stored before, increase Times Graded 
                if (_AnonTrackingService.SelectUrlsByIdAndEmail(model) != null)
                {
                    try
                    {
                        _AnonTrackingService.IncreaseTimesGraded(model);
                        return Request.CreateResponse(HttpStatusCode.OK, new SuccessResponse());
                    }
                    catch (Exception ex)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
                    }
                }
                // If url never graded, insert for current email
                else
                {
                    try
                    {
                        ItemResponse<int> resp = new ItemResponse<int>();
                        resp.Item = _AnonTrackingService.InsertUrl(model);
                        return Request.CreateResponse(HttpStatusCode.OK, resp);
                    }
                    catch (Exception ex)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
                    }
                }
            }

            //If Anonymous email never used, insert 
            else
            {
                try
                {
                    ItemResponse<int> resp = new ItemResponse<int>();
                    resp.Item = _AnonTrackingService.InsertUser(model);
                    return Request.CreateResponse(HttpStatusCode.OK, resp);
                }
                catch (Exception ex)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
                }
            }
        }

        [Route("grid"), HttpPost]
        public HttpResponseMessage GetGrid(AnonTrackingGridRequest model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            try
            {
                ItemResponse<AnonTrackingGrid> resp = new ItemResponse<AnonTrackingGrid>();
                resp.Item = _AnonTrackingService.GetGrid(model);
                return Request.CreateResponse(HttpStatusCode.OK, resp);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [Route("userurls"), HttpPost]
        public HttpResponseMessage GetUrlsByUser(AnonUserChangeRequest model)
        {
            try
            {
                ItemsResponse<AnonUserUrls> response = new ItemsResponse<AnonUserUrls>();
                response.Items = _AnonTrackingService.SelectUrlsByUser(model);
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [Route("DeleteSelected"), HttpDelete]
        public HttpResponseMessage MultiDelete([FromUri] int[] ids)
        {
            try
            {
                _AnonTrackingService.DeleteMultiple(ids);
                return Request.CreateResponse(HttpStatusCode.OK, new SuccessResponse());
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}