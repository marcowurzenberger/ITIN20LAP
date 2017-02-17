using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using log4net;
using innovation4austria.logic;
using innovation4austria.authentication;
using innovation4austria.dataAccess;
using innovation4austria.web.Models;

namespace innovation4austria.web.Controllers
{
    public class CompanyController : Controller
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private static readonly i4aMembershipProvider provider = new i4aMembershipProvider();

        private static readonly i4aRoleProvider roleprovider = new i4aRoleProvider();

        [HttpGet]
        [Authorize(Roles = Constants.ROLE_I4A)]
        public ActionResult Worker(int id)
        {
            log.Info("Company - Worker(id) - GET");

            List<portaluser> userList = new List<portaluser>();
            userList = PortaluserAdministration.GetAllUserByCompanyId(id);

            List<ViewWorkerModel> model = new List<ViewWorkerModel>();

            foreach (var u in userList)
            {
                model.Add(new ViewWorkerModel()
                {
                    Company = u.company.name,
                    Email = u.email,
                    Firstname = u.firstname,
                    Id = u.id,
                    Lastname = u.lastname,
                    Role = u.role.description
                });
            }

            return View(model);
        }
    }
}