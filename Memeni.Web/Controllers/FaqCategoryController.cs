﻿using Memeni.Models.ViewModels;
using Memeni.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Memeni.Web.Controllers
{
    [RouteArea("admin")]
    [RoutePrefix("faqcategory")]
    public class FaqCategoryController : BaseController
    {
        public FaqCategoryController(IPageMetaTagsService pageMetaTagsService, IHelpService helpService) : base(pageMetaTagsService, helpService)
        {
        }
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