using Memeni.Models.Requests;
using Memeni.Models.Responses;
using Memeni.Services;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace Memeni.Web.Controllers.Api
{
    [RoutePrefix("api/file")]
    public class LogoUploadApiController : ApiController
    {
        private ILogoService _logoService;

        public LogoUploadApiController(ILogoService logoService)
        {
            _logoService = logoService;
        }

        string ServerFileName = string.Empty;

        //--POST FILE--
        [Route("upload"), HttpPost]
        public async Task<HttpResponseMessage> UploadSingleFile()
        {
            try
            {
                ItemResponse<int> response = new ItemResponse<int>();
                HttpPostedFile postedFile = HttpContext.Current.Request.Files[0];
                LogoAddRequest model = new LogoAddRequest
                {
                    FileName = postedFile.FileName,
                    Size = postedFile.ContentLength,
                    ContentType = postedFile.ContentType,                  
                };
                string contentType = Request.Content.Headers.ContentType.MediaType;

                ServerFileName = string.Format("{0}_{1}{2}",
                    Path.GetFileNameWithoutExtension(postedFile.FileName),
                    Guid.NewGuid().ToString(),
                    Path.GetExtension(postedFile.FileName));
                model.ServerFileName = ServerFileName;

                await SavePostedFile(postedFile);
                response.Item = _logoService.Insert(model);

                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        //--PUT FILE--
        [Route("edit/{fileId:int}"), HttpPut]
        public async Task<HttpResponseMessage> UpdateSingleFile(int fileId)
        {
            try
            {
                //ItemResponse<int> response = new ItemResponse<int>();
                HttpPostedFile postedFile = HttpContext.Current.Request.Files[0];
                LogoUpdateRequest model = new LogoUpdateRequest
                {
                    FileName = postedFile.FileName,
                    Size = postedFile.ContentLength,
                    ContentType = postedFile.ContentType
                };
                string contentType = Request.Content.Headers.ContentType.MediaType;

                ServerFileName = string.Format("{0}_{1}{2}",
                    Path.GetFileNameWithoutExtension(postedFile.FileName),
                    Guid.NewGuid().ToString(),
                    Path.GetExtension(postedFile.FileName));
                model.ServerFileName = ServerFileName;

                await SavePostedFile(postedFile);
                _logoService.UpdateLogo(model);
                return Request.CreateResponse(HttpStatusCode.OK, new SuccessResponse());
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        private async Task SavePostedFile(HttpPostedFile postedFile)
        {
            MemoryStream ms = null;
            string rootPath = string.Empty;
            string serverPath = string.Empty;
            string fqn = string.Empty;

            serverPath = System.Configuration.ConfigurationManager.AppSettings["fileFolder"];
            rootPath = HttpContext.Current.Server.MapPath(serverPath);
            fqn = System.IO.Path.Combine(rootPath, ServerFileName);

            using (FileStream fs = new FileStream(fqn, FileMode.Append, FileAccess.Write, FileShare.None, bufferSize: postedFile.ContentLength, useAsync: true))
            {
                ms = new MemoryStream();
                postedFile.InputStream.CopyTo(ms);
                await fs.WriteAsync(ms.ToArray(), 0, postedFile.ContentLength);
            }
        }

        // POST api/<controller>
        [Route("store/{userId:int}"), HttpPost]
        public HttpResponseMessage PostFileIds(CompanyFileIdsRequest model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                _logoService.InsertIds(model);
                return Request.CreateResponse(HttpStatusCode.OK, new SuccessResponse());
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        // PUT api/<controller>
        [Route("store/{userId:int}"), HttpPut]
        public HttpResponseMessage PutFileIds(CompanyFileIdsRequest model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                _logoService.UpdateIds(model);
                return Request.CreateResponse(HttpStatusCode.OK, new SuccessResponse());
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
    }
}
