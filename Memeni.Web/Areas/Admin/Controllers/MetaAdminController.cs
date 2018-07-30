using Memeni.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Memeni.Web.Areas.Admin.Controllers
{
    [RouteArea("admin")]
    [RoutePrefix("meta")]
    public class MetaAdminController : Controller
    {
        [Route("index")]
        // CRUD: Admin/Meta
        public ActionResult Index()
        {
            return View();
        }

        [Route("{id:int}/tags")]
        // GETbyID: Admin/Meta
        public ActionResult Tags(int id = 0)
        {
            ItemViewModel<int> model = new ItemViewModel<int>();
            model.Item = id;
            return View(model);
        }
    }
}