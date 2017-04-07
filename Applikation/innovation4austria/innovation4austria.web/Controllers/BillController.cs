using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rotativa;
using log4net;
using innovation4austria.authentication;
using innovation4austria.logic;
using innovation4austria.dataAccess;
using innovation4austria.web.Models;

namespace innovation4austria.web.Controllers
{
    public class BillController : Controller
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private static readonly i4aMembershipProvider provider = new i4aMembershipProvider();

        private static readonly i4aRoleProvider roleprovider = new i4aRoleProvider();

        [HttpGet]
        [Authorize(Roles = Constants.ROLE_STARTUP)]
        public ActionResult GeneratePDF(int id)
        {
            log.Info("Bill - Download(id) - GET");

            // Erstellen des PDF für die Rechnung

            bill pdfBill = new bill();
            pdfBill = BillAdministration.GetBillById(id);

            List<bookingdetail> bdList = new List<bookingdetail>();
            bdList = pdfBill.bookingdetails.ToList();

            List<room> rList = new List<room>();
            rList = RoomAdministration.GetAllRooms();

            List<booking> bList = new List<booking>();
            bList = BookingAdministration.GetAllBookings();

            BillToPdfModel model = new BillToPdfModel();
            model.Billdate = pdfBill.billdate;
            model.Id = pdfBill.id;

            decimal totalPrice = 0;

            model.Bookingdetails = new List<ViewBookingdetail>();

            foreach (var item in bdList)
            {
                model.Bookingdetails.Add(new ViewBookingdetail()
                {
                    Id = item.id,
                    BookingDate = item.booking_date,
                    Price = item.price,
                    Room = (from bd in bdList
                            join b in bList on bd.booking_id equals b.id
                            join r in rList on b.room_id equals r.id
                            where bd.id == item.id
                            select r.description).FirstOrDefault()
                });
                totalPrice += item.price;
            }


            model.TotalPrice = totalPrice;
            model.TotalNetto = (totalPrice / 100) * 80;
            model.USt = (totalPrice / 100) * 20;

            company dbCompany = new company();
            dbCompany = CompanyAdministration.GetCompanyByUserEmail(User.Identity.Name);

            model.Company = new CreateCompanyModel() { City = dbCompany.city, Name = dbCompany.name, Number = dbCompany.number, Street = dbCompany.street, Zip = dbCompany.zip };

            ViewAsPdf viewPdf = new ViewAsPdf("Details", model);

            return View("Details", model);
        }

        [HttpGet]
        [Authorize(Roles = Constants.ROLE_STARTUP)]
        public ActionAsPdf Download(int id)
        {
            return new ActionAsPdf("GeneratePDF", new { id });
        }

        [HttpGet]
        [Authorize(Roles = Constants.ROLE_I4A)]
        public ActionResult CreateBills()
        {
            log.Info("Bill - CreateBills - GET");

            // Hier wird die Logik aufgerufen, um die Rechnungen für das vergangene Monat für alle Firmen zu erzeugen
            bool success = BillAdministration.GenerateBills();

            if (success)
            {
                TempData[Constants.SUCCESS_MESSAGE] = "Postenlisten erfolgreich erstellt!";
                return RedirectToAction("Dashboard", "i4a");
            }

            TempData[Constants.WARNING_MESSAGE] = "Ein Fehler ist passiert oder keine Daten zum Verarbeiten vorhanden!";
            return RedirectToAction("Dashboard", "i4a");
        }
    }
}