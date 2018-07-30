using Memeni.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Memeni.Services.Interfaces;

namespace Memeni.Web.Areas.User.Controllers
{
    [RouteArea("User")]
    [RoutePrefix("Home")]
    [Authorize(Roles = "User")]
    public class UserHomeController : BaseController
    {
        public UserHomeController(IPageMetaTagsService pageMetaTagsService, IHelpService helpService) : base(pageMetaTagsService, helpService)
        {
        }

        [Route("Index")]
        [Authorize(Roles = "User")]
        public ActionResult Index()
        {
            return View();
        }

        [Route("upload")]
        [Route("upload/{id:int}/edit")]
        [Authorize(Roles = "User")]
        public ActionResult Manage(int id = 0)
        {
            return View();
        }
    }
}