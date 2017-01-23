using innovation4austria.web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using log4net;
using log4net.Config;
using System.Diagnostics;

namespace innovation4austria.web.Controllers
{
    public class UserController : Controller
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        
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


                    return RedirectToAction("Index", "Home");
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
    }
}