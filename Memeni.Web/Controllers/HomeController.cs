using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Memeni.Services.Interfaces;

namespace Memeni.Web.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(IPageMetaTagsService pageMetaTagsService, IHelpService helpService) : base(pageMetaTagsService, helpService)
        {
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Login()
        {
            ViewBag.Message = "Login";
            //If User is already logged in and attempts to go to login page, return them to home
            if (User.Identity.IsAuthenticated)
            {
                Response.Redirect("/Home/Index");
            }
            //Else function as normal
                return View();
        }

        public ActionResult Register()
        {
            ViewBag.Message = "Sign up";

            return View();
        }

        public ActionResult Confirmation()
        {
            ViewBag.Message = "Confirmation Page";

            return View();
        }

        public ActionResult ForgotPassword()
        {
            ViewBag.Message = "Recover Password";

            return View();
        }

        public ActionResult ResetPassword()
        {
            ViewBag.Message = "Reset Password";

            return View();
        }

        public ActionResult Loading()
        {
            ViewBag.Message = "Loading Page";

            return View();
        }

        public ActionResult ConfirmEmail()
        {
            ViewBag.Message = "Confirm Email Address";

            return View();
        }
    }
}
