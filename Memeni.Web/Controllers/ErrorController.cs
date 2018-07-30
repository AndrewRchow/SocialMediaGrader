using Memeni.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Memeni.Web.Controllers
{
    [RoutePrefix("error")]
    public class ErrorController : BaseController
    {
        public ErrorController(IPageMetaTagsService pageMetaTagsService, IHelpService helpService) : base(pageMetaTagsService, helpService)
        {
        }
        [Route("404")]
        public ActionResult NotFound()
        {
            return View();
        }
        [Route("500")]
        public ActionResult InternalServer()
        {
            return View();
        }
    }
}