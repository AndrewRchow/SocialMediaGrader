using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Memeni.Web.Areas.Admin.Controllers
{
    [RouteArea("admin")]
    [RoutePrefix("twitter")]
    public class TwitterAdminController : Controller
    {
        [Route()]
        [Route("index")]
        // POST: Admin/TwitterAdmin
        public ActionResult Index()
        {
            return View();
        }
    }
}