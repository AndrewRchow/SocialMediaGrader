using Memeni.Models.ViewModels;
using Memeni.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Mvc;

namespace Memeni.Web.Controllers
{
    [RoutePrefix("privacy")]
    public class PrivacyController : BaseController
    {
        public PrivacyController(IPageMetaTagsService pageMetaTagsService, IHelpService helpService) : base(pageMetaTagsService, helpService)
        {
        }

        [Route("Policies")]
        public ActionResult Policies()
        {
            return View();
        }
    }
}
