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
using innovation4austria.dataAccess;
using innovation4austria.logic;

namespace innovation4austria.web.Controllers
{
    public class UserController : Controller
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private static readonly i4aMembershipProvider provider = new i4aMembershipProvider();

        private static readonly i4aRoleProvider roleprovider = new i4aRoleProvider();

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {
            log.Info("Login - POST");

            try
            {
                if (provider.ValidateUser(model.Email, model.Password))
                {
                    if (ModelState.IsValid)
                    {
                        if (model.StayLoggedIn)
                        {
                            if (roleprovider.IsUserInRole(model.Email, "innovations4austria"))
                            {
                                TempData[Constants.SUCCESS_MESSAGE] = "Login erfolgreich";

                                FormsAuthentication.SetAuthCookie(model.Email, true);
                                return RedirectToAction("Dashboard", "i4a");
                            }

                            TempData[Constants.SUCCESS_MESSAGE] = "Login erfolgreich";

                            FormsAuthentication.SetAuthCookie(model.Email, true);
                            return RedirectToAction("Dashboard", "User");
                        }
                        else
                        {
                            if (roleprovider.IsUserInRole(model.Email, "innovations4austria"))
                            {
                                TempData[Constants.SUCCESS_MESSAGE] = "Login erfolgreich";

                                FormsAuthentication.SetAuthCookie(model.Email, false);
                                return RedirectToAction("Dashboard", "i4a");
                            }

                            TempData[Constants.SUCCESS_MESSAGE] = "Login erfolgreich";

                            FormsAuthentication.SetAuthCookie(model.Email, false);
                            return RedirectToAction("Dashboard", "User");
                        }
                    }
                    else
                    {
                        TempData[Constants.ERROR_MESSAGE] = "Login fehlgeschlagen";

                        return View(model);
                    }
                }
                else
                {
                    TempData[Constants.ERROR_MESSAGE] = "Login fehlgeschlagen";

                    return View(model);
                }
            }
            catch (Exception ex)
            {
                log.Error("Error at Login - POST", ex);
            }

            TempData[Constants.ERROR_MESSAGE] = "Login fehlgeschlagen";

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [Authorize]
        public ActionResult Logout()
        {
            log.Info("Logout()");

            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [Authorize(Roles = Constants.ROLE_STARTUP)]
        public ActionResult Dashboard()
        {
            log.Info("Dashboard()");

            DashboardModel model = new DashboardModel();

            model.Bills = new List<DashboardBillModel>();
            model.Rooms = new List<DashboardRoomModel>();
            model.Users = new List<DashboardUserModel>();

            try
            {
                using (var context = new innovations4austriaEntities())
                {
                    #region Auffüllen der Mitarbeiter
                    List<portaluser> allUsers = new List<portaluser>();
                    string company = PortaluserAdministration.GetCompanyNameByUserMail(User.Identity.Name);

                    allUsers = PortaluserAdministration.GetAllUserByCompany(company);

                    foreach (var u in allUsers)
                    {
                        model.Users.Add(new DashboardUserModel()
                        {
                            Company = u.company.name,
                            Email = u.email,
                            Firstname = u.firstname,
                            Lastname = u.lastname,
                            Role = u.role.description,
                            Id = u.id,
                            Active = u.active
                        });
                    }
                    #endregion

                    #region Auffüllen der Rechnungen
                    List<bill> allBills = new List<bill>();
                    allBills = BillAdministration.GetAllBillsByCompany(company);

                    foreach (var b in allBills)
                    {
                        model.Bills.Add(new DashboardBillModel()
                        {
                            Id = b.id,
                            Billdate = b.billdate
                        });
                    }
                    #endregion

                    #region Auffüllen der gebuchten Räume
                    List<room> allRooms = new List<room>();
                    allRooms = RoomAdministration.GetAllRoomsByCompany(company);

                    //List<booking> allBookings = new List<booking>();
                    //allBookings = BookingAdministration.GetAllBookingsByCompany(company);

                    //List<bookingdetail> allBookingDetails = new List<bookingdetail>();
                    //allBookingDetails = BookingdetailAdministration.GetAllBookingdetailsByCompany(company);

                    foreach (var r in allRooms)
                    {

                        if (BookingAdministration.GetBookingsByCompanyAndRoom(company, r.id).Count > 1)
                        {

                            List<booking> bList = new List<booking>();
                            List<bookingdetail> bdList = new List<bookingdetail>();

                            bList = BookingAdministration.GetBookingsByCompanyAndRoom(company, r.id);
                            foreach (var book in bList)
                            {
                                DashboardRoomModel room = new DashboardRoomModel();
                                room.RoomDescription = r.description;
                                room.Room_Id = r.id;

                                bdList = BookingdetailAdministration.GetAllBookingdetailsByBookingId(book.id);
                                room.Booking_Id = book.id;
                                room.Startdate = bdList.Select(x => x.booking_date).FirstOrDefault();
                                room.Enddate = bdList.Select(x => x.booking_date).LastOrDefault();

                                model.Rooms.Add(room);
                            }
                        }
                        else
                        {
                            DashboardRoomModel room = new DashboardRoomModel();
                            room.RoomDescription = r.description;
                            room.Room_Id = r.id;

                            booking b = new booking();
                            List<bookingdetail> bdList = new List<bookingdetail>();

                            b = BookingAdministration.GetBookingByCompanyAndRoomId(company, r.id);
                            bdList = BookingdetailAdministration.GetAllBookingdetailsByBookingId(b.id);
                            room.Booking_Id = b.id;
                            room.Startdate = bdList.Select(x => x.booking_date).FirstOrDefault();
                            room.Enddate = bdList.Select(x => x.booking_date).LastOrDefault();

                            model.Rooms.Add(room);
                        }
                    }

                    model.Rooms.OrderBy(x => x.Enddate);
                    #endregion

                }
            }
            catch (Exception ex)
            {
                log.Error("Error loading Dashboard()", ex);
            }

            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = Constants.ROLE_STARTUP)]
        public ActionResult Detail(int id)
        {
            portaluser tempUser = PortaluserAdministration.GetUserById(id);

            UserDetailModel model = new UserDetailModel()
            {
                Email = tempUser.email,
                Firstname = tempUser.firstname,
                Lastname = tempUser.lastname,
                Role = tempUser.role.description,
                Password = tempUser.password.ToString()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangeFirstname(string firstname)
        {
            bool success = PortaluserAdministration.ChangeFirstname(User.Identity.Name, firstname);
            int id = PortaluserAdministration.GetIdFromUser(User.Identity.Name);

            if (success)
            {
                TempData[Constants.SUCCESS_MESSAGE] = "Vorname erfolgreich geändert";

                return RedirectToAction("Detail", "User", new { id });
            }
            TempData[Constants.ERROR_MESSAGE] = "Fehler beim Ändern des Vornamens";

            return RedirectToAction("Dashboard");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangeLastname(string lastname)
        {
            bool success = PortaluserAdministration.ChangeLastname(User.Identity.Name, lastname);
            int id = PortaluserAdministration.GetIdFromUser(User.Identity.Name);

            if (success)
            {
                TempData[Constants.SUCCESS_MESSAGE] = "Nachname erfolgreich geändert";

                return RedirectToAction("Detail", "User", new { id });
            }
            TempData[Constants.ERROR_MESSAGE] = "Fehler beim Ändern des Nachnamens";

            return RedirectToAction("Dashboard");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(string oldPassword, string newPassword)
        {
            bool success = PortaluserAdministration.ChangePassword(User.Identity.Name, oldPassword, newPassword);
            int id = PortaluserAdministration.GetIdFromUser(User.Identity.Name);

            if (success)
            {
                TempData[Constants.SUCCESS_MESSAGE] = "Passwort erfolgreich geändert";

                return RedirectToAction("Detail", "User", new { id });
            }
            TempData[Constants.ERROR_MESSAGE] = "Fehler beim Ändern des Passworts";

            return RedirectToAction("Dashboard");
        }

    }
}