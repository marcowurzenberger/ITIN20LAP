using innovation4austria.dataAccess;
using innovation4austria.logic;
using innovation4austria.web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace innovation4austria.web.Controllers
{
    
    public class HomeController : Controller
    {
        [AllowAnonymous]
        [HttpGet]
        public ActionResult Index()
        {
            List<BannerModel> model = new List<BannerModel>();

            List<room> dbRooms = new List<room>();
            dbRooms = RoomAdministration.GetThreeMostExpensiveRooms();

            foreach (var item in dbRooms)
            {
                model.Add(new BannerModel()
                {
                    Facility = item.facility.name,
                    Price = item.price,
                    RoomId = item.id,
                    RoomName = item.description
                });
            }

            return View(model);
        }
    }
}