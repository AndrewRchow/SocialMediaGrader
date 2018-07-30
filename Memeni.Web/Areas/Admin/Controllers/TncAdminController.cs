using Memeni.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Memeni.Web.Areas.Admin.Controllers
{
    [RouteArea("Admin")]
    [RoutePrefix("terms")]
    public class TncAdminController : Controller
    {
        [Route("index")]
        // GETALL/DELETE: Admin/Terms
        public ActionResult Index()
        {
            return View();
        }

        [Route("create")]
        [Route("{id:int}/edit")]
        // POST/PUT: Admin/Terms
        public ActionResult Manage(int id = 0)
        {
            ItemViewModel<int> model = new ItemViewModel<int>();
            model.Item = id;
            return View(model);
        }
    }
}