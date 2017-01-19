using innovations4austria.logic;
using innovations4austria.web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace innovations4austria.web.Controllers
{
    public class UserController : Controller
    {
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {
            if (Tools.ValidateLogin(model.Email, model.Password))
            {
                if (model.StayLoggedIn)
                {
                    FormsAuthentication.SetAuthCookie(model.Email, true);
                }
                else
                {
                    FormsAuthentication.SetAuthCookie(model.Email, false);
                }

                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }
    }
}