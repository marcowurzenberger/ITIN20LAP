using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using log4net;
using innovation4austria.logic;
using innovation4austria.authentication;
using innovation4austria.web.Models;
using innovation4austria.dataAccess;

namespace innovation4austria.web.Controllers
{
    public class RoomController : Controller
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        [HttpGet]
        [Authorize(Roles = Constants.ROLE_STARTUP)]
        public ActionResult List()
        {
            log.Info("Room - List() - GET");

            RoomListModel model = new RoomListModel();
            model.Filter = new RoomFilterModel();
            model.Rooms = new List<RoomViewModel>();
            model.Filter.Furnishments = new List<FilterFurnishmentModel>();

            List<FilterFurnishmentModel> filterFurnishments = new List<FilterFurnishmentModel>();

            List<furnishment> dbFurnishments = new List<furnishment>();
            dbFurnishments = FurnishmentAdministration.GetAllFurnishments();

            foreach (var item in dbFurnishments)
            {
                model.Filter.Furnishments.Add(new FilterFurnishmentModel()
                {
                    Id = item.id,
                    Name = item.description
                });
            }

            model.Filter.Furnishments.Distinct();

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = Constants.ROLE_STARTUP)]
        [ValidateAntiForgeryToken]
        public ActionResult _Filter(string daterange, int[] furnishment)
        {
            log.Info("Room - Filter(string daterange, int[] furnishmentId) - POST");

            daterange = daterange.Replace(" ", "");

            string start = daterange.Substring(0, 10);
            string end = daterange.Remove(0, 11);

            DateTime startdate = Convert.ToDateTime(start);
            DateTime enddate = Convert.ToDateTime(end);

            TimeSpan span = enddate.Subtract(startdate);
            int datediff = (int)span.TotalDays;

            if (datediff == 0)
            {
                datediff = 1;
            }

            //Filterlogik aufrufen
            List<int> furnIds = new List<int>();

            List<room> rooms = new List<room>();

            if (furnishment != null)
            {
                furnIds = furnishment.ToList();
                rooms = RoomAdministration.GetFilteredRooms(furnIds, startdate, enddate);
            }
            else
            {
                furnIds = FurnishmentAdministration.GetAllIDs();
                rooms = RoomAdministration.GetAllRoomsByDate(startdate, enddate);
            }

            //Mappen der Suchergebnisse
            List<RoomViewModel> model = new List<RoomViewModel>();

            foreach (var r in rooms)
            {
                List<FurnishmentViewModel> fViewList = new List<FurnishmentViewModel>();

                List<furnishment> fList = new List<furnishment>();
                fList = FurnishmentAdministration.GetFurnishmentsByRoomId(r.id);

                foreach (var f in fList)
                {
                    if (f != null)
                    {
                        fViewList.Add(new FurnishmentViewModel() { Id = f.id, Name = f.description });
                    }
                }

                RoomViewModel rv = new RoomViewModel();

                rv.Furnishments = new List<FurnishmentViewModel>();
                rv.Furnishments = fViewList;
                rv.Id = r.id;
                rv.Name = r.description;
                rv.Price = r.price;
                rv.DateDiff = datediff;
                rv.Start = startdate;
                rv.End = enddate;

                model.Add(rv);
            }

            return PartialView("_List", model);
        }

        [HttpGet]
        [ChildActionOnly]
        public ActionResult _List(List<RoomViewModel> model)
        {
            log.Info("Room - _List()");

            List<room> dbRooms = new List<room>();
            dbRooms = RoomAdministration.GetAllRooms();

            List<furnishment> dbFurnishments = new List<furnishment>();
            dbFurnishments = FurnishmentAdministration.GetAllFurnishments();

            List<roomfurnishment> dbRoomFurnishments = new List<roomfurnishment>();
            dbRoomFurnishments = RoomfurnishmentAdministration.GetAllRoomfurnishments();

            foreach (var r in dbRooms)
            {
                RoomViewModel rv = new RoomViewModel() { Id = r.id, Price = r.price, Name = r.description };
                rv.Furnishments = new List<FurnishmentViewModel>();

                List<FurnishmentViewModel> roomFurnishments = new List<FurnishmentViewModel>();

                List<roomfurnishment> rfList = new List<roomfurnishment>();

                foreach (var f in dbFurnishments)
                {
                    foreach (var rf in dbRoomFurnishments)
                    {
                        if (r.id == rf.room_id && f.id == rf.furnishment_id)
                        {
                            rv.Furnishments.Add(new FurnishmentViewModel() { Id = f.id, Name = f.description });

                        }
                    }
                }
                rv.Furnishments.Distinct();
                model.Add(rv);
            }

            return PartialView(model);
        }

        [HttpGet]
        [Authorize(Roles = Constants.ROLE_STARTUP)]
        public ActionResult Booking(int id, int datediff, string startdate, string enddate)
        {
            log.Info("GET - Room - Booking(int id, string DateDiff)");

            RoomViewModel model = new RoomViewModel();

            List<furnishment> furList = new List<furnishment>();
            furList = FurnishmentAdministration.GetFurnishmentsByRoomId(id);

            room dbRoom = new room();
            dbRoom = RoomAdministration.GetRoomById(id);

            model.DateDiff = datediff;
            model.Id = id;
            model.Name = dbRoom.description;
            model.Price = dbRoom.price;
            model.TotalPrice = dbRoom.price * datediff;
            if (startdate != null && enddate != null)
            {
                model.Start = Convert.ToDateTime(startdate);
                model.End = Convert.ToDateTime(enddate); 
            }
            else
            {
                model.Start = DateTime.Today;
                model.End = DateTime.Today;
            }
            model.Furnishments = new List<FurnishmentViewModel>();

            foreach (var item in furList)
            {
                model.Furnishments.Add(new FurnishmentViewModel() { Id = item.id, Name = item.description });
            }

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = Constants.ROLE_STARTUP)]
        [ValidateAntiForgeryToken]
        public ActionResult Booking(int id, string start, string end, decimal price)
        {
            log.Info("POST - Room - Booking()");

            DateTime startdate = DateTime.Parse(start);
            DateTime enddate = DateTime.Parse(end);

            TimeSpan span = enddate.Subtract(startdate);
            int datediff = span.Days;

            bool success = RoomAdministration.BookingRoom(id, startdate, enddate, User.Identity.Name);
            if (success)
            {
                TempData[Constants.SUCCESS_MESSAGE] = "Buchung erfolgreich getätigt";
                return RedirectToAction("Dashboard", "User");
            }

            TempData[Constants.WARNING_MESSAGE] = "Fehler bei der Buchung";
            return RedirectToAction("Booking", new { Id = id, DateDiff = datediff, Startdate = startdate, Enddate = enddate });
        }

        [HttpGet]
        [Authorize(Roles = Constants.ROLE_STARTUP)]
        public ActionResult Detail(int id)
        {
            log.Info("GET - Room - Detail(int id)");

            room r = new room();
            r = RoomAdministration.GetRoomByBookingId(id);

            booking b = new booking();
            b = BookingAdministration.GetBookingById(id);

            RoomDetailModel model = new RoomDetailModel();
            model.BookingId = b.id;
            model.Description = r.description;
            model.Id = r.id;
            model.Startdate = b.bookingdetails.Select(x => x.booking_date).FirstOrDefault();
            model.Enddate = b.bookingdetails.Select(x => x.booking_date).LastOrDefault();
            model.PricePerDay = b.bookingdetails.Select(x => x.price).FirstOrDefault();

            TimeSpan span = model.Enddate.Subtract(model.Startdate);
            int days = span.Days;

            model.BookedDays = days;
            model.TotalPrice = model.PricePerDay * days;

            return View(model);
        }
    }
}