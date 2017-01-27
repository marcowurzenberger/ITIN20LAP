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
            model.RoomList = new List<SearchRoomModel>();
            model.Filter = new RoomFilterModel();
            model.Filter.FacilityList = new List<facility>();
            model.Filter.FurnishmentList = new List<furnishment>();
            model.Filter.RoomList = new List<SearchRoomModel>();

            List<room> allRooms = new List<room>();
            List<roomfurnishment> allRoomfurnishments = new List<roomfurnishment>();

            List<facility> facList = new List<facility>();
            List<furnishment> furList = new List<furnishment>();

            furnishment temp = new furnishment();

            try
            {
                using (var context = new innovations4austriaEntities())
                {
                    allRooms = RoomAdministration.GetAllRooms();
                    allRoomfurnishments = RoomfurnishmentAdministration.GetAllRoomfurnishments();

                    if (allRooms != null && allRooms.Count > 0)
                    {
                        if (allRoomfurnishments != null && allRoomfurnishments.Count > 0)
                        {
                            foreach (var r in allRooms)
                            {
                                foreach (var item in allRoomfurnishments)
                                {
                                    if (item.room_id == r.id)
                                    {
                                        temp = item.furnishment;
                                    }
                                }

                                model.Filter.RoomList.Add(new SearchRoomModel()
                                {
                                    Description = r.description,
                                    FacilityDescription = r.facility.name,
                                    Id = r.id,
                                    FurnishmentDescription = temp.description
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

                    facList = FacilityAdministration.GetAllFacilities();
                    furList = FurnishmentAdministration.GetAllFurnishments();

                    model.Filter.FacilityList = facList;
                    model.Filter.FurnishmentList = furList;

                    if (model.Filter.FacilityList == null || model.Filter.FacilityList.Count == 0)
                    {
                        log.Warn("FacilityList from model.Filter is null or Count is 0");
                    }
                    if (model.Filter.FurnishmentList == null || model.Filter.FurnishmentList.Count == 0)
                    {
                        log.Warn("FurnishmentList from model.Filter is null or Count is 0");
                    }
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