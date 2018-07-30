using Memeni.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Memeni.Web.Controllers
{
    [RoutePrefix("metatest")]
    public class MetaTestController : BaseController
    {
        public MetaTestController(IPageMetaTagsService pageMetaTagsService, IHelpService helpService) : base(pageMetaTagsService, helpService)
        {
        }

        // GET: MetaTest
        [Route("index")]
        public ActionResult Index()
        {
            return View();
        }
    }
}