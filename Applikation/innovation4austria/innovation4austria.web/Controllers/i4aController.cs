using innovation4austria.dataAccess;
using innovation4austria.logic;
using innovation4austria.web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using log4net;
using innovation4austria.authentication;

namespace innovation4austria.web.Controllers
{
    public class i4aController : Controller
    {

        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private static readonly i4aMembershipProvider memprovider = new i4aMembershipProvider();

        private static readonly i4aRoleProvider roleprovider = new i4aRoleProvider();

        [HttpGet]
        [Authorize(Roles = Constants.ROLE_I4A)]
        public ActionResult Dashboard(CreateCompanyModel ccModel)
        {
            log.Info("GET - Dashboard - i4a");

            Dashboardi4aModel model = new Dashboardi4aModel();
            model.ManageCompanies = new List<ManageCompanyModel>();

            if (ccModel != null)
            {
                model.CreateCompany = new CreateCompanyModel()
                {
                    City = ccModel.City,
                    Name = ccModel.Name,
                    Number = ccModel.Number,
                    Street = ccModel.Street,
                    Zip = ccModel.Zip
                };
            }
            else
            {
                model.CreateCompany = new CreateCompanyModel();
            }

            List<company> allCompanies = new List<company>();
            allCompanies = CompanyAdministration.GetAllCompanies();

            foreach (var c in allCompanies)
            {
                if (c.active)
                {
                    model.ManageCompanies.Add(new ManageCompanyModel()
                    {
                        Id = c.id,
                        City = c.city,
                        Name = c.name,
                        Number = c.number,
                        Street = c.street,
                        Zip = c.zip
                    }); 
                }
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Constants.ROLE_I4A)]
        public ActionResult CreateCompany(string name, string zip, string city, string street, string number)
        {
            log.Info("POST - i4a - CreateCompany()");

            bool success = CompanyAdministration.CreateNewCompany(name, zip, city, street, number);

            if (success)
            {
                TempData[Constants.SUCCESS_MESSAGE] = "Neue Firma erfolgreich erstellt";
                return RedirectToAction("Dashboard", "i4a");
            }

            CreateCompanyModel model = new CreateCompanyModel()
            {
                Name = name,
                City = city,
                Number = number,
                Street = street,
                Zip = zip
            };

            TempData[Constants.WARNING_MESSAGE] = "Fehler beim Erstellen der neuen Firma!";
            return RedirectToAction("Dashboard", "i4a", new { model });
        }

        [HttpGet]
        [Authorize(Roles = Constants.ROLE_I4A)]
        public ActionResult EditCompany(int id)
        {
            company temp = CompanyAdministration.GetCompanyById(id);

            ManageCompanyModel model = new ManageCompanyModel()
            {
                Id = temp.id,
                City = temp.city,
                Name = temp.name,
                Number = temp.number,
                Street = temp.street,
                Zip = temp.zip
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Constants.ROLE_I4A)]
        public ActionResult EditCompany(ManageCompanyModel model)
        {
            log.Info("EditCompany(ManageCompanyModel model)");

            bool success = CompanyAdministration.EditCompany(model.Id, model.Name, model.Zip, model.City, model.Street, model.Number);

            if (success)
            {
                TempData[Constants.SUCCESS_MESSAGE] = "Firma erfolgreich geändert";
                return RedirectToAction("Dashboard");
            }

            TempData[Constants.ERROR_MESSAGE] = "Fehler beim Ändern der Firma";
            return RedirectToAction("Dashboard");
        }

        [HttpGet]
        [Authorize(Roles = Constants.ROLE_I4A)]
        public ActionResult DeleteCompany(int id)
        {
            log.Info("i4a - DeleteCompany(int id) - GET");

            company temp = CompanyAdministration.GetCompanyById(id);

            ManageCompanyModel model = new ManageCompanyModel()
            {
                Id = temp.id,
                City = temp.city,
                Name = temp.name,
                Number = temp.number,
                Street = temp.street,
                Zip = temp.zip
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Constants.ROLE_I4A)]
        public ActionResult DeleteCompany(ManageCompanyModel model)
        {
            log.Info("i4a - DeleteCompany(ManageCompanyModel model) - POST");

            bool success = CompanyAdministration.DeleteCompanyById(model.Id);

            if (success)
            {
                TempData[Constants.SUCCESS_MESSAGE] = "Firma erfolgreich gelöscht";
                return RedirectToAction("Dashboard");
            }

            TempData[Constants.ERROR_MESSAGE] = "Fehler beim Löschen der Firma";
            return RedirectToAction("Dashboard");
        }

        [HttpGet]
        [Authorize(Roles = Constants.ROLE_I4A)]
        public ActionResult Roomlist()
        {
            log.Info("GET - i4a - Roomlist");

            List<RoomViewModel> model = new List<RoomViewModel>();

            List<room> dbRooms = new List<room>();
            dbRooms = RoomAdministration.GetAllRooms();

            foreach (var r in dbRooms)
            {
                RoomViewModel rv = new RoomViewModel();
                rv.Id = r.id;
                rv.Name = r.description;
                rv.Price = r.price;

                facility dbFacility = new facility();
                dbFacility = FacilityAdministration.GetFacilityByRoomId(r.id);

                rv.Facility = dbFacility.name;
                rv.FacilityId = dbFacility.id;

                List<FurnishmentViewModel> fViewList = new List<FurnishmentViewModel>();

                List<furnishment> dbFurnishments = new List<furnishment>();
                dbFurnishments = FurnishmentAdministration.GetFurnishmentsByRoomId(r.id);

                foreach (var f in dbFurnishments)
                {
                    fViewList.Add(new FurnishmentViewModel() { Id = f.id, Name = f.description });
                }

                rv.Furnishments = new List<FurnishmentViewModel>();
                rv.Furnishments = fViewList;

                model.Add(rv);
            }

            return View(model);
        }
    }
}