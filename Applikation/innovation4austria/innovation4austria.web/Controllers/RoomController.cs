using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using log4net;
using innovation4austria.web.Models;
using innovation4austria.dataAccess;
using innovation4austria.logic;

namespace innovation4austria.web.Controllers
{
    public class RoomController : Controller
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        [HttpGet]
        [Authorize]
        public ActionResult Search()
        {
            log.Info("Room - Search() - GET");

            ViewRoomModel model = new ViewRoomModel();

            List<room> allRooms = RoomAdministration.GetAllRooms();
            List<roomfurnishment> allRoomfurnishments = RoomfurnishmentAdministration.GetAllRoomfurnishments();

            try
            {
                model.Filter = new RoomFilterModel();
                model.RoomList = new List<SearchRoomModel>();

                if (allRooms != null && allRooms.Count > 0)
                {
                    if (allRoomfurnishments != null && allRoomfurnishments.Count > 0)
                    {
                        foreach (var r in allRooms)
                        {
                            model.RoomList.Add(new SearchRoomModel()
                            {
                                Description = r.description,
                                FacilityDescription = r.facility.name,
                                Id = r.id,
                                FurnishmentDescription = (from x in allRoomfurnishments where x.room_id == r.id select x.furnishment.description).FirstOrDefault()
                            });
                        }  
                    }
                    else
                    {
                        log.Warn("allRoomfurnishments is null or Count is 0");
                    }
                }
                else
                {
                    log.Warn("allRooms is null or Count is 0");
                }

                model.Filter.FacilityList = FacilityAdministration.GetAllFacilities();
                model.Filter.FurnishmentList = FurnishmentAdministration.GetAllFurnishments();

                if (model.Filter.FacilityList == null || model.Filter.FacilityList.Count == 0)
                {
                    log.Warn("FacilityList from model.Filter is null or Count is 0");
                }
                if (model.Filter.FurnishmentList == null || model.Filter.FurnishmentList.Count == 0)
                {
                    log.Warn("FurnishmentList from model.Filter is null or Count is 0");
                }

            }
            catch (Exception ex)
            {
                log.Error("Error loading Room Search", ex);
            }

            return View(model);
        }
    }
}