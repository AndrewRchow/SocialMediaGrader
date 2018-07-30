using Hangfire;
using Memeni.Models.Domain;
using Memeni.Models.Requests;
using Memeni.Models.Responses;
using Memeni.Services;
using Memeni.Services.Interfaces;
using Memeni.Services.Proxy;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Memeni.Web.Controllers.Api
{
    [RoutePrefix("api/email")]
    public class EmailMessageApiController : ApiController
    {
        private IEmailService _emailService;
        private IUserService _userService;
        private IErrorLogService _errorLogService;

        public EmailMessageApiController(IUserService userService, IEmailService emailService, IErrorLogService errorLogService)
        {
            _emailService = emailService;
            _userService = userService;
            _errorLogService = errorLogService;
        }

        // POST api
        [Route(), HttpPost]
        public HttpResponseMessage Post(EmailMessageAddRequest model)
        {
            ItemResponse<int> response = new ItemResponse<int>();
            response.Item = _emailService.Insert(model);

            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        [Route("{id:int}"), HttpGet]
        // GET BY ID api/email/id
        public HttpResponseMessage SelectById(int id)
        {
            try
            {
                ItemResponse<EmailCode> resp = new ItemResponse<EmailCode>();
                resp.Item = _emailService.SelectById(id);
                return Request.CreateResponse(HttpStatusCode.OK, resp);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [Route("delete/{id:int}"), HttpDelete]
        // Delete BY ID api/email/id
        public HttpResponseMessage DeleteById(int id)
        {
            try
            {
                _emailService.DeleteById(id);
                return Request.CreateResponse(HttpStatusCode.OK, new SuccessResponse());
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [Route("contact"), HttpPost]
        // Contact Page Post
        public HttpResponseMessage ContactEmail(EmailMessageAddRequest model)
        {
            try
            {
                _emailService.SendContactEmail(model);
                return Request.CreateResponse(HttpStatusCode.OK, new SuccessResponse());
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        // Error Log Reports... Hit this endpoint with data
        [Route("error"), HttpPost]
        public HttpResponseMessage ErrorEmail()
        {
            try
            {
                EmailProxyService svc = new EmailProxyService();
                RecurringJob.AddOrUpdate(() => svc.ScheduleErrorEmail(), Cron.Daily(17)); 
                return Request.CreateResponse(HttpStatusCode.OK, new SuccessResponse());
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        // User Reports if anon return one else weekly reports
        // Send in ToEmail and ToName
        [Route("weeklyreport/{url}/{name}"), HttpPost]
        public HttpResponseMessage ReportEmail(ReportAddRequest model)
        {
            try
            {
                EmailProxyService svc = new EmailProxyService();
                string cronExp = "0 17 * * 1";
                RecurringJob.AddOrUpdate(model.Id, () => svc.ScheduleReportEmail(model), cronExp); 
                return Request.CreateResponse(HttpStatusCode.OK, new SuccessResponse());
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        // New Api for turning off weekly reports
        [Route("cancelreport/{id}"), HttpPost]
        public HttpResponseMessage CancelReport(string id)
        {
            try
            {
                RecurringJob.RemoveIfExists(id);
                return Request.CreateResponse(HttpStatusCode.OK, new SuccessResponse());
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        // Trigger report instantly by id 
        [Route("triggerreport/{id}"), HttpPost]
        public HttpResponseMessage TriggerReport(string id)
        {
            try
            {
                RecurringJob.Trigger(id);
                return Request.CreateResponse(HttpStatusCode.OK, new SuccessResponse());
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        // Single report without scheduling
        [Route("singlereport/{url}/{name}"), HttpPost]
        public HttpResponseMessage AnonEmail(ReportAddRequest model)
        {
            try
            {
                _emailService.ScheduleReportEmail(model);
                return Request.CreateResponse(HttpStatusCode.OK, new SuccessResponse());
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}