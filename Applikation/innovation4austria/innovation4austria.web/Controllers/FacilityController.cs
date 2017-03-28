using innovation4austria.dataAccess;
using innovation4austria.logic;
using innovation4austria.web.Models;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace innovation4austria.web.Controllers
{
    public class FacilityController : Controller
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        [HttpGet]
        [Authorize(Roles = Constants.ROLE_I4A)]
        public ActionResult List()
        {
            log.Info("Facility - List() - GET");

            ViewFacilityModel model = new ViewFacilityModel();
            model.Create = new CreateFacilityModel();
            model.Facilities = new List<ListFacilityModel>();

            List<facility> fList = new List<facility>();
            fList = FacilityAdministration.GetAllFacilities();

            foreach (var item in fList)
            {
                model.Facilities.Add(new ListFacilityModel()
                {
                    City = item.city,
                    Id = item.id,
                    Name = item.name,
                    Number = item.number,
                    Street = item.street,
                    Zip = item.zip
                });
            }

            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = Constants.ROLE_I4A)]
        public ActionResult Create()
        {
            log.Info("Facility - Create() - GET");

            CreateFacilityModel model = new CreateFacilityModel();

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = Constants.ROLE_I4A)]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateFacilityModel model)
        {
            log.Info("Facility - CreateFacility(CreateFacilityModel model) - POST");

            bool success = FacilityAdministration.CreateFacility(model.Name, model.Zip, model.City, model.Street, model.Number);

            if (success)
            {
                TempData[Constants.SUCCESS_MESSAGE] = "Gebäude erfolgreich erstellt";
                return RedirectToAction("Dashboard", "i4a");
            }

            TempData[Constants.WARNING_MESSAGE] = "Fehler beim Erstellen des Gebäudes";
            return RedirectToAction("List", "Facility");
        }

        [HttpGet]
        [Authorize(Roles = Constants.ROLE_I4A)]
        public ActionResult Edit(int id)
        {
            log.Info("Facility - Edit(int id) - GET");

            facility currentFacility = new facility();
            currentFacility = FacilityAdministration.GetFacilityById(id);

            CreateFacilityModel model = new CreateFacilityModel()
            {
                City = currentFacility.city,
                Id = currentFacility.id,
                Name = currentFacility.name,
                Number = currentFacility.number,
                Street = currentFacility.street,
                Zip = currentFacility.zip
            };

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = Constants.ROLE_I4A)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CreateFacilityModel model)
        {
            log.Info("Facility - Edit() - POST");

            bool success = FacilityAdministration.EditFacility(model.Id, model.Name, model.Zip, model.City, model.Street, model.Number);

            if (success)
            {
                TempData[Constants.SUCCESS_MESSAGE] = "Gebäude erfolgreich geändert";
                return RedirectToAction("Dashboard", "i4a");
            }

            TempData[Constants.WARNING_MESSAGE] = "Fehler beim Bearbeiten des Gebäudes";
            return View(model.Id);
        }

        [HttpGet]
        [Authorize(Roles = Constants.ROLE_I4A)]
        public ActionResult Rooms(int id)
        {
            log.Info("GET - Facility - Rooms(int id)");

            List<room> dbRooms = new List<room>();
            dbRooms = RoomAdministration.GetAllRoomsByFacilityId(id);

            List<RoomViewModel> model = new List<RoomViewModel>();


            foreach (var r in dbRooms)
            {
                RoomViewModel rv = new RoomViewModel();
                rv.Id = r.id;
                rv.Facility = r.facility.name;
                rv.FacilityId = r.facility_id;
                rv.Name = r.description;
                rv.Price = r.price;

                rv.Furnishments = new List<FurnishmentViewModel>();
                List<furnishment> fList = new List<furnishment>();
                fList = FurnishmentAdministration.GetFurnishmentsByRoomId(r.id);

                foreach (var f in fList)
                {
                    rv.Furnishments.Add(new FurnishmentViewModel()
                    {
                        Id = f.id,
                        Name = f.description
                    });
                }

                model.Add(rv);
            }

            return View(model);
        }
    }
}