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
using System.Drawing;
using System.Reflection;

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

            WorkerModel model = new WorkerModel();
            model.CompanyId = id;
            model.Workers = new List<ViewWorkerModel>();

            foreach (var u in userList)
            {
                model.Workers.Add(new ViewWorkerModel()
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

        [HttpGet]
        [Authorize(Roles = Constants.ROLE_I4A)]
        public ActionResult CreateWorker(int id)
        {
            log.Info("Company - CreateWorker() - GET");

            CreateWorkerModel model = new CreateWorkerModel();
            model.RoleList = new List<RoleDropdownModel>();
            model.CompanyId = id;

            List<role> allRoles = new List<role>();
            allRoles = RoleAdministration.GetAllRoles();

            foreach (var r in allRoles)
            {
                model.RoleList.Add(new RoleDropdownModel()
                {
                    Id = r.id,
                    Name = r.description
                });
            }

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = Constants.ROLE_I4A)]
        [ValidateAntiForgeryToken]
        public ActionResult CreateWorker(string firstname, string lastname, string email, int rolelist, int companyid)
        {
            log.Info("Company - CreateWorker(string...) - POST");

            company comp = CompanyAdministration.GetCompanyById(companyid);

            bool success = PortaluserAdministration.CreateUser(firstname, lastname, email, rolelist, comp.name);

            if (success)
            {
                TempData[Constants.SUCCESS_MESSAGE] = "Benutzer erfolgreich hinzugefügt";
                return RedirectToAction("Dashboard", "i4a");
            }

            TempData[Constants.WARNING_MESSAGE] = "Fehler beim Erstellen des neuen Benutzers";

            CreateWorkerModel model = new CreateWorkerModel();
            model.RoleList = new List<RoleDropdownModel>();
            model.Email = email;
            model.Firstname = firstname;
            model.Lastname = lastname;
            
            List<role> allRoles = new List<role>();
            allRoles = RoleAdministration.GetAllRoles();

            foreach (var r in allRoles)
            {
                model.RoleList.Add(new RoleDropdownModel()
                {
                    Id = r.id,
                    Name = r.description
                });
            }

            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = Constants.ROLE_I4A)]
        public ActionResult DeleteWorker(int id)
        {
            log.Info("Company - DeleteWorker(int id) - GET");

            ViewWorkerModel model = new ViewWorkerModel();

            model.Id = id;

            portaluser user = PortaluserAdministration.GetUserById(id);

            model.Company = user.company.name;
            model.Email = user.email;
            model.Firstname = user.firstname;
            model.Lastname = user.lastname;
            model.Role = user.role.description;

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = Constants.ROLE_I4A)]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteWorkerWithId(int id)
        {
            log.Info("Company - DeleteWorker(ViewWorkerModel) - POST");

            portaluser user = PortaluserAdministration.GetUserById(id);

            bool success = PortaluserAdministration.SetUserInactive(user.email);

            if (success)
            {
                TempData[Constants.SUCCESS_MESSAGE] = "Mitarbeiter erfolgreich gelöscht";
                return RedirectToAction("Dashboard", "i4a");
            }

            TempData[Constants.WARNING_MESSAGE] = "Fehler beim Löschen des Mitarbeiters";
            return RedirectToAction("Dashboard", "i4a");
        }

        [HttpGet]
        [Authorize(Roles = Constants.ROLE_I4A)]
        public ActionResult Expenditure()
        {
            log.Info("GET - Company - Expenditure()");

            ChartModel model = new ChartModel();
            model.FilterCompanies = new List<ViewCompanyModel>();

            List<company> dbCompanies = new List<company>();
            dbCompanies = CompanyAdministration.GetAllCompanies();

            foreach (var item in dbCompanies)
            {
                model.FilterCompanies.Add(new ViewCompanyModel() { Id = item.id, Name = item.name });
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Constants.ROLE_I4A)]
        public ActionResult Filter(int FilterCompanies)
        {
            log.Info("POST - Company - Filter()");

            ChartModel model = new ChartModel();
            model.FilterCompanies = new List<ViewCompanyModel>();

            List<company> dbCompanies = new List<company>();
            dbCompanies = CompanyAdministration.GetAllCompanies();

            foreach (var item in dbCompanies)
            {
                model.FilterCompanies.Add(new ViewCompanyModel() { Id = item.id, Name = item.name });
            }

            model.CompanyId = FilterCompanies;
            model.CompanyName = CompanyAdministration.GetCompanyById(model.CompanyId).name;
            model.CompanyColor = Color.Azure;
            model.Expenditure = CompanyAdministration.GetExpenditures(model.CompanyId, DateTime.Now.AddMonths(-1).Month, DateTime.Now.Year);
    
            return PartialView("_List", model);
        }

        [HttpGet]
        [ChildActionOnly]
        public ActionResult _List(ChartModel model)
        {
            log.Info("GET - Company - _List(ChartModel model)");

            return PartialView(model);
        }

    }
}