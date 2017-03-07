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
        [Authorize]
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
        [Authorize]
        public ActionResult _Filter(string daterange, int[] furnishmentId)
        {
            log.Info("Room - Filter(string daterange, int[] furnishmentId) - POST");

            daterange = daterange.Replace(" ", "");

            string start = daterange.Substring(0, 10);
            string end = daterange.Remove(0, 11);

            DateTime startdate = Convert.ToDateTime(start);
            DateTime enddate = Convert.ToDateTime(end);

            //Filterlogik aufrufen
            List<int> furnIds = furnishmentId.ToList();

            List<room> rooms = RoomAdministration.GetFilteredRooms(furnIds, startdate, enddate);



            return View();
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
    }
}