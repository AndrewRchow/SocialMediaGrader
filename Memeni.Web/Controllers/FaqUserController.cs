using Memeni.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Memeni.Web.Controllers
{
    [RoutePrefix("faq")]
    public class FaqUserController : BaseController
    {
        public FaqUserController(IPageMetaTagsService pageMetaTagsService, IHelpService helpService) : base(pageMetaTagsService, helpService)
        {
        }
        [Route("index")]
        public ActionResult Index()
        {
            return View();
        }
        [Route("manage")]
        public ActionResult Manage()
        {
            return View();
        }
    }
}