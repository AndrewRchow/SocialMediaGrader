using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Memeni.Models.ViewModels;

namespace Memeni.Web.Areas.Admin.Controllers
{
    [RouteArea("Admin")]
    [RoutePrefix("errorlog")]
    public class ErrorLogAdminController : Controller
    {
        [Route("index")]
        public ActionResult Index()
        {
            return View();
        }
    }
}