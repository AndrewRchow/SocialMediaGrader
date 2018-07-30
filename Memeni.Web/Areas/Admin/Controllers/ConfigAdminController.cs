using Memeni.Models.ViewModels;
using Memeni.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Memeni.Services.Interfaces;

namespace Memeni.Web.Areas.Admin.Controllers
{
    [RouteArea("Admin")]
    [RoutePrefix("config")]
    public class ConfigAdminController : BaseController
    {
        public ConfigAdminController(IPageMetaTagsService pageMetaTagsService, IHelpService helpService) : base(pageMetaTagsService, helpService)
        {
        }
        //[OutputCache(Duration = 3600)]
        [HttpGet]
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

