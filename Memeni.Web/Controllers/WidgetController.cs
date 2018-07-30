using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Memeni.Web.Controllers
{
    [RoutePrefix("widget")]
    public class WidgetController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}