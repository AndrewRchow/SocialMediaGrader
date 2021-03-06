using Memeni.Models.Domain;
using Memeni.Models.ViewModels;
using System.Collections.Generic;
using System.Web.Mvc;
using System;
using Memeni.Services;
using Memeni.Services.Interfaces;

namespace Memeni.Web.Controllers
{
    public class BaseController : Controller
    {
        private IPageMetaTagsService _pageMetaTagsService;
        private IHelpService _helpService;

        public BaseController(IPageMetaTagsService pageMetaTagsService, IHelpService helpService)
        {
            _pageMetaTagsService = pageMetaTagsService;
            _helpService = helpService;
        }

        protected T GetViewModel<T>() where T : BaseViewModel, new()
        {
            T model = new T();

            //customize base view model here
            model.PageTags = HydrateMetaTags();

            // Simplify this after it works
            model.HelpList = HydrateHelpList();

            return model;
        }

        private List<Help> HydrateHelpList()
        {
            List<Help> helpList = new List<Help>();
            helpList = _helpService.GetByUrl(this.Request.Url.AbsolutePath);
            return helpList;
        }

        private List<PageMetaTags> HydrateMetaTags()
        {
            List<PageMetaTags> metaTagList = _pageMetaTagsService.GetByUrl(this.Request.Url.AbsolutePath);
            return metaTagList;
        }

        public new ActionResult View()
        {
            BaseViewModel model = GetViewModel<BaseViewModel>();
            return base.View(model);
        }
    }
}
