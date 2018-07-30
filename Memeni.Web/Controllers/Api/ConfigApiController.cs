using Memeni.Models.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Memeni.Models.Domain;
using Memeni.Models.Responses;
using Memeni.Services;
using Memeni.Models.ViewModels;
using System.Web;

namespace Memeni.Web.Controllers.Api
{
    [RoutePrefix("api/config")]
    public class ConfigApiController : ApiController
    {
        private IConfigService _configService;

        public ConfigApiController(IConfigService configService)
        {
            _configService = configService;
        }

        [Route(), HttpGet]
        public HttpResponseMessage Get()
        {
            try
            {
                ItemsResponse<ConfigIndexModel> response = new ItemsResponse<ConfigIndexModel>();
                response.Items = _configService.Get();
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [Route("{id:int}"), HttpGet]
        public HttpResponseMessage GetbyId(int id)
        {
            try
            {
                ItemResponse<Config> response = new ItemResponse<Config>();
                response.Item = _configService.GetById(id);


                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {

                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [Route, HttpPost]
        public HttpResponseMessage Insert(ConfigAddRequest model)

        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }

                if (_configService.GetByConfigName(model.ConfigName).ConfigName != null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "ConfigName already exists!");
                }



                ItemResponse<int> response = new ItemResponse<int>();
                response.Item = _configService.Insert(model);

                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [Route("byConfigName/{configName}"), HttpGet]
        public HttpResponseMessage GetConfigValusAsString(string configName)
        {
            try
            {
                if (string.IsNullOrEmpty(configName))
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "ConfigName is Missing!");
                }
                ItemResponse<string> response = new ItemResponse<string>();
                response.Item = _configService.getConfigValusAsString(configName);
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [Route("{id:int}"), HttpPut]
        public HttpResponseMessage Update(ConfigUpdateRequest model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                _configService.Update(model);
                return Request.CreateResponse(HttpStatusCode.OK, new SuccessResponse());
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [Route("{id:int}"), HttpDelete]
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                _configService.Delete(id);
                return Request.CreateResponse(HttpStatusCode.OK, new SuccessResponse());
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpGet]
        [Route("{configName}/cookie")]
        public HttpResponseMessage GetCookie(string configName)
        {
            ItemsResponse<string> response = new ItemsResponse<string>();
            response.Items = new List<string>();

            // do your cookie stuff here
            var cookie = HttpContext.Current.Request.Cookies[configName];
            response.Items.Add(cookie.Name);
            response.Items.Add(cookie.Value);
            return Request.CreateResponse(HttpStatusCode.OK, response);

        }

        [HttpGet]
        [Route("{configName}/{configVal}/cookie")]
        public HttpResponseMessage SetCookie(string configName, string configVal)
        {
            SuccessResponse response = new SuccessResponse();

            // do your cookie stuff here
            // HttpCookie myCookie = new HttpCookie(configName, configVal);
            var cookie = new HttpCookie(configName, configVal);
            HttpContext.Current.Response.Cookies.Set(cookie);
            // DateTime now = DateTime.Now;

            // Set the cookie value.
            // cookie.Value = now.ToString();
            // Set the cookie expiration date.
            // cookie.Expires = now.AddMinutes(10);

            return Request.CreateResponse(HttpStatusCode.OK, response);
        }
    }
}