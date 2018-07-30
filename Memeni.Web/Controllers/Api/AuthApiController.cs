using Memeni.Models.Domain;
using Memeni.Models.Requests;
using Memeni.Models.Responses;
using Memeni.Services;
using Memeni.Services.Security;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web.Http;
using Memeni.Web;
using System.Text;
using Memeni.Services.Interfaces;

namespace Memeni.Web.Controllers.Api
{
    [RoutePrefix("api/auth")]
    public class AuthApiController : ApiController
    {
        private IUserService _userService;
        private IAuthenticationService _auth;
        private IPrincipal _principal;
        private IEmailService _emailService;
        private IErrorLogService _errorLogService;

        public AuthApiController(IUserService userService, IAuthenticationService auth, IPrincipal principal, IEmailService emailService, IErrorLogService errorLogService)
        {
            _userService = userService;
            _auth = auth;
            _principal = principal;
            _emailService = emailService;
            _errorLogService = errorLogService;
        }

        [Route("login"), HttpPost]
        // Login
        public HttpResponseMessage LogIn(UserRequest model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            try
            {
                ItemResponse<bool> resp = new ItemResponse<bool>();
                resp.Item = _userService.LogIn(model);
                if (resp.Item == true)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, true);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Login Error. Please check your Email or Password."); 
                };
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [Route("loginFree"), HttpPost]
        // Login through fb/google/linkedin without password
        public HttpResponseMessage LogInFree(UserFreeRequest model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            try
            {
                ItemResponse<bool> resp = new ItemResponse<bool>();
                resp.Item = _userService.LogInFree(model);
                if (resp.Item == true)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, true);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Login Error. Please check your Email");
                };
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [Route("register"), HttpPost]
        // Register
        public async Task<HttpResponseMessage> Register(RegisterUserRequest model)
        {    
            try
            {
                if (!ModelState.IsValid)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                if (_userService.GetByEmail(model.Email)>0)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Email Already Registered");
                }
                ItemResponse<int> resp = new ItemResponse<int>();
                resp.Item = _userService.Create(model);
                String emailGuid = Guid.NewGuid().ToString();
                _userService.CreateCode(resp.Item, emailGuid);
                await _emailService.SendEmailConfirmation(model.Email, emailGuid);
                return Request.CreateResponse(HttpStatusCode.OK, resp);
            }
            catch (Exception ex)
            {
                _errorLogService.LogError(ex);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [Route("facebook"), HttpPost]
        // Register
        public HttpResponseMessage RegisterFacebook(RegisterUserFacebookRequest model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            //If email already exists, store their facebook id > if already stored, return message and continue
            int userId = _userService.GetByEmail(model.Email);
            if (userId > 0)
            {
                try
                {
                    ItemResponse<int> resp = new ItemResponse<int>();
                    resp.Item = _userService.AddFacebookId(userId, model.FbId, model.Picture);
                    return Request.CreateResponse(HttpStatusCode.OK, resp);
                }
                catch (Exception ex)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, ex);
                }
            }
            //If no email, create new account, and add facebook id to facebook table.
            try
            {
                ItemResponse<int> resp = new ItemResponse<int>();
                resp.Item = _userService.RegisterFacebook(model);
                return Request.CreateResponse(HttpStatusCode.OK, resp);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [Route("google"), HttpPost]
        // Register
        public HttpResponseMessage RegisterGoogle(RegisterUserGoogleRequest model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            //If email already exists, store their google id > if already stored, return message and continue
            int userId = _userService.GetByEmail(model.Email);
            if (userId > 0)
            {
                try
                {
                    ItemResponse<int> resp = new ItemResponse<int>();
                    resp.Item = _userService.AddGoogleId(userId, model.GoogleId);
                    return Request.CreateResponse(HttpStatusCode.OK, resp);
                }
                catch (Exception ex)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, ex);
                }
            }
            //If no email, create new account, and add google id to google table.
            try
            {
                ItemResponse<int> resp = new ItemResponse<int>();
                resp.Item = _userService.RegisterGoogle(model);
                return Request.CreateResponse(HttpStatusCode.OK, resp);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [Route("linkedin"), HttpPost]
        // Register
        public HttpResponseMessage RegisterLinkedIn(RegisterUserLinkedInRequest model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            //If email already exists, store their linkedin id > if already stored, return message and continue
            int userId = _userService.GetByEmail(model.Email);
            if (userId > 0)
            {
                try
                {
                    ItemResponse<int> resp = new ItemResponse<int>();
                    resp.Item = _userService.AddLinkedInId(userId, model.LinkedInId);
                    return Request.CreateResponse(HttpStatusCode.OK, resp);
                }
                catch (Exception ex)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, ex);
                }
            }
            //If no email, create new account, and add linkedin id to linkedin table.
            try
            {
                ItemResponse<int> resp = new ItemResponse<int>();
                resp.Item = _userService.RegisterLinkedIn(model);
                return Request.CreateResponse(HttpStatusCode.OK, resp);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [Route("logout"), HttpGet]
        public HttpResponseMessage Logout()
        {
            _auth.LogOut();
            return Request.CreateResponse(HttpStatusCode.OK, new Object());
        }
        [Route("current"), HttpGet]
        public HttpResponseMessage Current()
        {
            var id = _principal.Identity;
            var u = id.GetCurrentUser();
            return Request.CreateResponse(HttpStatusCode.OK, u);
        }
        [Route("current/Authorized"), HttpGet]
        [Authorize]
        public HttpResponseMessage CurrentAuth()
        {
            return Request.CreateResponse(HttpStatusCode.OK, _principal.Identity.GetCurrentUser());
        }

        [Route("current/roles/user"), HttpGet]
        [Authorize(Roles = "User")]
        public HttpResponseMessage CurrentUser()
        {
            return Request.CreateResponse(HttpStatusCode.OK, _principal.Identity.GetCurrentUser());
        }

        [Route("current/roles/admin"), HttpGet]
        [Authorize(Roles = "Admin")]
        public HttpResponseMessage CurrentAdmin()
        {
            return Request.CreateResponse(HttpStatusCode.OK, _principal.Identity.GetCurrentUser());
        }

        [Route("recaptcha"), HttpPost]
        public bool RecaptchaValidate(RecaptchaRequest model)
        {
            if (string.IsNullOrEmpty(model.Secret) || string.IsNullOrEmpty(model.Response)) return false;
            var client = new System.Net.WebClient();
            var googleReply = client.DownloadString($"https://www.google.com/recaptcha/api/siteverify?secret={model.Secret}&response={model.Response}");
            return JsonConvert.DeserializeObject<GoogleRecaptchaResponse>(googleReply).Success;
        }

        [Route("reset"), HttpPut]
        // Reset
        public HttpResponseMessage ResetPassword(ResetPwRequest model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            try
            {
                 _userService.ResetPwConfirm(model);
                return Request.CreateResponse(HttpStatusCode.OK, true);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [Route("resend"), HttpPost]
        // Resend Confirmation Email
        public async Task<HttpResponseMessage> ResendEmail(ResendEmailRequest model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            try
            {
                string guid = _userService.GetGuidByEmail(model.Id);
                await _emailService.SendEmailConfirmation(model.Email, guid);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [Route("forgotPassword"), HttpPost]
        // Resend Confirmation Email
        public async Task<HttpResponseMessage> ForgotPassword(ForgotPwRequest model)
        {
            string resetCode = "";
            try
            {
                int check = _userService.GetByEmail(model.Email);
                if(check != 0)
                {
                    resetCode = _userService.ResetPwEntry(model.Email);
                    await _emailService.ForgotPwEmail(model.Email, resetCode);
                }
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}