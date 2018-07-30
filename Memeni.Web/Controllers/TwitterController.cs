using Memeni.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Memeni.Web.Controllers
{
    public class TwitterController : BaseController
    {
        public TwitterController(IPageMetaTagsService pageMetaTagsService, IHelpService helpService) : base(pageMetaTagsService, helpService)
        {
        }
        public ActionResult Report()
        {
            return View();
        }
        public ActionResult Loading()
        {
            return View();
        }
    }
}