using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Memeni.Web.Areas.Admin.Controllers
{
    [RouteArea("Admin")]
    [RoutePrefix("Home")]
    public class HomeAdminController : Controller
    {
        [Authorize(Roles = "Admin")]
        [Route("index")]
        public ActionResult Index()
        {
            return View();
        }
    }
}