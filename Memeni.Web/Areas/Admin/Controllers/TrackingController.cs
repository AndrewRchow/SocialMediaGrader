using Memeni.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Memeni.Web.Areas.Admin.Controllers
{
    [RouteArea("admin")]
    [RoutePrefix("tracking")]
    public class TrackingController : Controller
    {
        [Route("index")]
        public ActionResult Index()
        {
            return View();
        }
    }
}