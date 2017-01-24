using innovation4austria.web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using log4net;
using log4net.Config;
using System.Diagnostics;
using System.Web.Security;
using innovation4austria.authentication;

namespace innovation4austria.web.Controllers
{
    public class UserController : Controller
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private static readonly UserMembershipProvider provider = new UserMembershipProvider();

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            XmlConfigurator.Configure();

            log.Info("Login - POST");

            try
            {
                if (ModelState.IsValid)
                {
                    if (provider.ValidateUser(model.Email, model.Password))
                    {
                        if (model.StayLoggedIn)
                        {
                            FormsAuthentication.SetAuthCookie(model.Email, true);
                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            FormsAuthentication.SetAuthCookie(model.Email, false);
                            return RedirectToAction("Index", "Home");
                        }
                    }
                    else
                    {
                        return View(model);
                    }
                }
                else
                {
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                log.Error("Error at Login - POST", ex);
                Debugger.Break();
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [Authorize]
        public ActionResult Logout()
        {
            XmlConfigurator.Configure();

            log.Info("Logout()");

            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }
    }
}