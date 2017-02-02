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
                                FormsAuthentication.SetAuthCookie(model.Email, true);
                                return RedirectToAction("Dashboard", "i4a");
                            }

                            FormsAuthentication.SetAuthCookie(model.Email, true);
                            return RedirectToAction("Dashboard", "User");
                        }
                        else
                        {
                            if (roleprovider.IsUserInRole(model.Email, "innovations4austria"))
                            {
                                FormsAuthentication.SetAuthCookie(model.Email, false);
                                return RedirectToAction("Dashboard", "i4a");
                            }

                            FormsAuthentication.SetAuthCookie(model.Email, false);
                            return RedirectToAction("Dashboard", "User");
                        }
                    }
                    else
                    {
                        return View(model);
                    }
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

        [HttpGet]
        [Authorize]
        public ActionResult Logout()
        {
            log.Info("Logout()");

            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [Authorize(Roles = "startups")]
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

                    List<room> allRooms = new List<room>();
                    allRooms = RoomAdministration.GetAllRoomsByCompany(company);

                    foreach (var r in allRooms)
                    {
                        model.Rooms.Add(new DashboardRoomModel()
                        {
                            Room_Id = r.id,
                            RoomDescription = r.description
                        });
                    }

                }
            }
            catch (Exception ex)
            {
                log.Error("Error loading Dashboard()", ex);
            }

            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "innovations4austria")]
        public ActionResult Dashboardi4a()
        {
            log.Info("Dashboardi4a()");



            return View();
        }

    }
}