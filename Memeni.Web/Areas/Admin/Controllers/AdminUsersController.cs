using Memeni.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Memeni.Web.Areas.Admin.Controllers
{
    [RouteArea("Admin")]
    [RoutePrefix("Users")]
    public class AdminUsersController : Controller
    {
        [Route("index")]
        [Authorize(Roles = "Admin")]
        // List of Users to Edit
        public ActionResult Index()
        {
            return View();
        }

        [Route("indexng")]
        [Authorize(Roles = "Admin")]
        // List of Users to Edit
        public ActionResult IndexNg()
        {
            return View();
        }

        [Route("{id:int}")]
        [Authorize(Roles = "Admin")]
        // List of Users to Edit
        public ActionResult UserProfile(int id)
        {
            ItemViewModel<int> model = new ItemViewModel<int>();
            model.Item = id;
            return View(model);
        }

    }
}