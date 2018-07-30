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
    [RoutePrefix("api/admin/users")]
    public class AdminUserApiController : ApiController
    {
        private IAdminUserService _AdminUserService;

        public AdminUserApiController(IAdminUserService AdminUserService)
        {
            _AdminUserService = AdminUserService;
        }

        [Route(), HttpGet]
        // GET api/admin/users
        public HttpResponseMessage GetAll()
        {
            try
            {
                ItemsResponse<Users> resp = new ItemsResponse<Users>();
                resp.Items = _AdminUserService.GetAll();
                return Request.CreateResponse(HttpStatusCode.OK, resp);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [Route("{id:int}"), HttpGet]
        // GET BY ID api/admin/users/id
        public HttpResponseMessage GetById(int id)
        {
            try
            {
                ItemResponse<Users> resp = new ItemResponse<Users>();
                resp.Item = _AdminUserService.GetById(id);
                return Request.CreateResponse(HttpStatusCode.OK, resp);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [Route("{id:int}"), HttpPut]
        // PUT api/admin/users/id
        public HttpResponseMessage Update(UsersUpdateRequest model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            try
            {
                _AdminUserService.Update(model);
                return Request.CreateResponse(HttpStatusCode.OK, new SuccessResponse());
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [Route("lock/{id:int}"), HttpPut]
        // PUT api/admin/users/lock/id
        public HttpResponseMessage LockUser(UsersUpdateRequest model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            try
            {
                _AdminUserService.LockUser(model);
                return Request.CreateResponse(HttpStatusCode.OK, new SuccessResponse());
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [Route("confirm/{id:int}"), HttpPost]
        // PUT api/admin/users/confirm/id
        public HttpResponseMessage ConfirmEmail(int id)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            try
            {
                _AdminUserService.ConfirmEmail(id);
                return Request.CreateResponse(HttpStatusCode.OK, new SuccessResponse());
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [Route("access/{id:int}"), HttpPut]
        // PUT api/admin/users/access/id
        public HttpResponseMessage AdminAccess(UsersUpdateRequest model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            try
            {
                _AdminUserService.AdminAccess(model);
                return Request.CreateResponse(HttpStatusCode.OK, new SuccessResponse());
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [Route("grid"), HttpPost]
        public HttpResponseMessage GetGrid(UsersGridRequest model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            try
            {
                ItemResponse<UsersGrid> resp = new ItemResponse<UsersGrid>();
                resp.Item = _AdminUserService.GetGrid(model);
                return Request.CreateResponse(HttpStatusCode.OK, resp);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}