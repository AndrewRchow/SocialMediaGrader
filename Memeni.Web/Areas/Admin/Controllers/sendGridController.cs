using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Memeni.Web.Areas.Admin.Controllers
{
    [RouteArea("admin")]
    [RoutePrefix("sendGrid")]
    public class sendGridController : Controller
    {
        [Route("index")]
        // GET: Admin/sendGrid
        public ActionResult Index()
        {
            return View();
        }
    }
}