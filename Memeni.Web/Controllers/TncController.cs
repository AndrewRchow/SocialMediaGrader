using Memeni.Models.Domain;
using Memeni.Models.Requests;
using Memeni.Models.ViewModels;
using Memeni.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Memeni.Web.Controllers
{
    [RoutePrefix("terms")]
    public class TncController : BaseController
    {
        public TncController(IPageMetaTagsService pageMetaTagsService, IHelpService helpService) : base(pageMetaTagsService, helpService)
        {
        }

        [Route("")]
        [Route("index")]
        // PUBLIC VIEW: Tnc
        public ActionResult Index()
        {
            return View();
        }
    }
}