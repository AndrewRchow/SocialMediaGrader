using Memeni.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Mvc;

namespace Memeni.Web.Areas.Admin.Controllers
{
    [RouteArea("Admin")]
    [RoutePrefix("PrivacyPolicy")]
    public class PrivacyPolicyController : Controller
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
