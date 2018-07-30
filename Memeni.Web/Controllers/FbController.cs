using Memeni.Models.Domain;
using Memeni.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Memeni.Web.Controllers
{
    [RoutePrefix("fb")]
    public class FbController : BaseController
    {
        public FbController(IPageMetaTagsService pageMetaTagsService, IHelpService helpService) : base(pageMetaTagsService, helpService)
        {
        }

        [Route("report")]
        public ActionResult Report()
        {
            return View();
        }

        [Route("loading")]
        public ActionResult Loading()
        {
            return View();
        }

        [Route("{socialURL}")]
        public ActionResult SharedReport(string socialURL)
        {
            Response.Cookies.Add(new HttpCookie("socialURL", socialURL));
            return Redirect("/fb/loading");
        }
    }
}