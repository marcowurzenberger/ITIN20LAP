using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace innovation4austria.web.Controllers
{
    public class i4aController : Controller
    {
        [HttpGet]
        [Authorize(Roles = "innovations4austria")]
        public ActionResult Dashboard()
        {
            return View();
        }
    }
}