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

            List<image> images = new List<image>();
            images = ImageAdministration.GetAllImages();

            foreach (var item in dbFurnishments)
            {
                model.Filter.Furnishments.Add(new FilterFurnishmentModel()
                {
                    Id = item.id,
                    Name = item.description,
                    Image = images.Where(x => x.furnishment_id == item.id).FirstOrDefault()
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

            datediff += 1;

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

            if (RoomAdministration.BookingRoom(id, startdate, enddate, User.Identity.Name))
            {
                TempData[Constants.SUCCESS_MESSAGE] = "Buchung erfolgreich getätigt";

                //if (MailDelivery.SendBookingConfirmation(User.Identity.Name,
                //    BookingAdministration.GetLastBookingByCompany(
                //        CompanyAdministration.GetCompanyByUserEmail(User.Identity.Name).id)))
                //{
                //    TempData[Constants.SUCCESS_MESSAGE] += "\nBuchungsbestätigung wird gesendet";
                //}

                return RedirectToAction("Dashboard", "User");
            }

            TempData[Constants.WARNING_MESSAGE] = "Fehler bei der Buchung";
            return RedirectToAction("Booking", new { Id = id, DateDiff = datediff, Startdate = startdate, Enddate = enddate });
        }

        [HttpGet]
        [Authorize(Roles = Constants.ROLE_STARTUP)]
        public ActionResult BookAndPay(int id, string start, string end, decimal price)
        {
            log.Info("GET - Room - BookAndPay(int id, string start, string end, decimal price)");

            string startString = start.Replace("/", "");
            string endString = end.Replace("/", "");

            string startMonth = startString.Substring(0, 2);
            string endMonth = endString.Substring(0, 2);

            string startDay = startString.Substring(2, 2);
            string endDay = endString.Substring(2, 2);

            string startYear = startString.Substring(4, 4);
            string endYear = endString.Substring(4, 4);

            int sMonth = Convert.ToInt32(startMonth);
            int eMonth = Convert.ToInt32(endMonth);
            int sYear = Convert.ToInt32(startYear);
            int eYear = Convert.ToInt32(endYear);
            int sDay = Convert.ToInt32(startDay);
            int eDay = Convert.ToInt32(endDay);

            DateTime startdate = new DateTime(sYear, sMonth, sDay);
            DateTime enddate = new DateTime(eYear, eMonth, eDay);

            TimeSpan span = enddate.Subtract(startdate);
            int datediff = span.Days;

            if (datediff == 0)
            {
                datediff = 1;
            }

            datediff += 1;

            PaymentViewModel model = new PaymentViewModel();
            model.Amount = price * datediff;
            model.RoomId = id;
            model.Start = startdate;
            model.End = enddate;

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = Constants.ROLE_STARTUP)]
        [ValidateAntiForgeryToken]
        public ActionResult BookAndPay(int id, string start, string end, string cardNumber)
        {
            log.Info("POST - Room - BookAndPay()");

            start = start.Replace("/", "");
            end = end.Replace("/", "");

            string startString = start.Substring(0, 8);
            string endString = end.Substring(0, 8);

            string startMonth = startString.Substring(0, 2);
            string endMonth = endString.Substring(0, 2);

            string startDay = startString.Substring(2, 2);
            string endDay = endString.Substring(2, 2);

            string startYear = startString.Substring(4, 4);
            string endYear = endString.Substring(4, 4);

            int sMonth = Convert.ToInt32(startMonth);
            int eMonth = Convert.ToInt32(endMonth);

            int sDay = Convert.ToInt32(startDay);
            int eDay = Convert.ToInt32(endDay);

            int sYear = Convert.ToInt32(startYear);
            int eYear = Convert.ToInt32(endYear);

            DateTime startdate = new DateTime(sYear, sMonth, sDay);
            DateTime enddate = new DateTime(eYear, eMonth, eDay);

            TimeSpan span = enddate.Subtract(startdate);
            int datediff = span.Days;

            if (datediff == 0)
            {
                datediff = 1;
            }

            datediff += 1;

            if (RoomAdministration.BookingRoom(id, startdate, enddate, User.Identity.Name) && PaymentAdministration.CheckLuhn(cardNumber))
            {
                TempData[Constants.SUCCESS_MESSAGE] = "Buchung erfolgreich getätigt\nZahlung erfolgreich";

                if (MailDelivery.SendPaidBookingConfirmation(User.Identity.Name,
                    BookingAdministration.GetLastBookingByCompany(
                        CompanyAdministration.GetCompanyByUserEmail(User.Identity.Name).id
                        )))
                {
                    TempData[Constants.SUCCESS_MESSAGE] += "\nBuchungsbestätigung wird gesendet";
                }

                return RedirectToAction("Dashboard", "User");
            }

            TempData[Constants.WARNING_MESSAGE] = "Fehler bei Tätigung der Buchung und Zahlung";
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

        [HttpGet]
        [Authorize(Roles = Constants.ROLE_I4A)]
        public ActionResult Edit(int id)
        {
            log.Info("GET - Room - Edit(int id)");

            EditRoomModel model = new EditRoomModel();

            room dbRoom = new room();
            dbRoom = RoomAdministration.GetRoomById(id);

            model.Description = dbRoom.description;
            model.FacilityId = dbRoom.facility_id;
            model.Id = dbRoom.id;
            model.Price = dbRoom.price;

            model.Furnishments = new List<FurnishmentViewModel>();
            List<furnishment> dbFurnishments = new List<furnishment>();
            dbFurnishments = FurnishmentAdministration.GetAllFurnishments();

            foreach (var item in dbFurnishments)
            {
                model.Furnishments.Add(new FurnishmentViewModel() { Id = item.id, Name = item.description });
            }

            model.Facilities = new List<FacilityViewModel>();
            List<facility> dbFacilities = new List<facility>();
            dbFacilities = FacilityAdministration.GetAllFacilities();

            foreach (var item in dbFacilities)
            {
                model.Facilities.Add(new FacilityViewModel() { Id = item.id, Name = item.name });
            }

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = Constants.ROLE_I4A)]
        public ActionResult Edit(int id, string description, string price, int[] furnishments, int facilities)
        {
            log.Info("POST - Room - Edit()");

            room updatedRoom = new room();

            if (furnishments == null)
            {
                List<roomfurnishment> oldFurnishments = new List<roomfurnishment>();
                oldFurnishments = RoomfurnishmentAdministration.GetRoomfurnishmentsByRoomId(id);

                updatedRoom.roomfurnishments = oldFurnishments;
            }
            else
            {
                List<roomfurnishment> newFurnishments = new List<roomfurnishment>();

                foreach (var item in furnishments)
                {
                    newFurnishments.Add(new roomfurnishment() { furnishment_id = item, room_id = id });
                }

                updatedRoom.roomfurnishments = newFurnishments;
            }

            updatedRoom.id = id;
            updatedRoom.description = description;

            decimal newPrice = Convert.ToDecimal(price);
            updatedRoom.price = newPrice;

            updatedRoom.facility_id = facilities;
            updatedRoom.facility = FacilityAdministration.GetFacilityById(facilities);

            updatedRoom.bookings = BookingAdministration.GetAllBookingsByRoomId(updatedRoom.id);

            if (RoomAdministration.UpdateRoom(updatedRoom, updatedRoom.roomfurnishments.ToList()))
            {
                TempData[Constants.SUCCESS_MESSAGE] = "Raum erfolgreich geändert";
                return RedirectToAction("RoomList", "i4a");
            }

            TempData[Constants.WARNING_MESSAGE] = "Fehler beim Aktualisieren des Raumes";
            return RedirectToAction("Edit", new { id = updatedRoom.id });
        }

        [HttpGet]
        [Authorize(Roles = Constants.ROLE_STARTUP)]
        public ActionResult CancelBooking(int id)
        {
            log.Info("GET - Room - CancelBooking");

            booking b = new booking();
            b = BookingAdministration.GetBookingById(id);

            CancelBookingModel model = new CancelBookingModel()
            {
                Id = b.id,
                Room = b.room.description,
                Start = b.bookingdetails.Select(x => x.booking_date).FirstOrDefault(),
                End = b.bookingdetails.Select(x => x.booking_date).LastOrDefault()
            };

            TimeSpan span = model.End.Subtract(model.Start);
            int datediff = span.Days + 1;

            model.Price = b.room.price * datediff;

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Cancelling(int id)
        {
            log.Info("POST - Room - Cancelling(int id)");

            if (BookingAdministration.CancelBooking(id))
            {
                TempData[Constants.SUCCESS_MESSAGE] = "Buchung erfolgreich storniert";
                return RedirectToAction("Dashboard", "User");
            }

            TempData[Constants.WARNING_MESSAGE] = "Fehler beim Stornieren der Buchung!";
            return RedirectToAction("CancelBooking", new { @id = id });
        }
    }
}