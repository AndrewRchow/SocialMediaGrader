using Memeni.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Memeni.Web.Areas.Admin.Controllers
{
    [RouteArea("admin")]
    [RoutePrefix("help")]
    public class HelpAdminController : Controller
    {
        [Route("index")]
        public ActionResult Index()
        {
            return View();
        }

        [Route("create")]
        [Route("{id:int}/edit")]
        public ActionResult Manage(int id = 0)
        {
            ItemViewModel<int> model = new ItemViewModel<int>();
            model.Item = id;
            return View(model);
        }
    }
}