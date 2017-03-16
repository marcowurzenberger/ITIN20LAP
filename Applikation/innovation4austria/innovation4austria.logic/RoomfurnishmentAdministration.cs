using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using innovation4austria.dataAccess;

namespace innovation4austria.logic
{
    public class RoomfurnishmentAdministration
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Get all room furnishments from database
        /// </summary>
        /// <returns>List of roomfurnishments</returns>
        public static List<roomfurnishment> GetAllRoomfurnishments()
        {
            log.Info("GetAllRoomfurnishments()");

            List<roomfurnishment> allRoomfurnishments = new List<roomfurnishment>();

            try
            {
                using (var context = new innovations4austriaEntities())
                {
                    allRoomfurnishments.AddRange(context.roomfurnishments.Include("furnishment").Include("room"));
                }
            }
            catch (Exception ex)
            {
                log.Error("Error getting all roomfurnishments", ex);
            }

            return allRoomfurnishments;
        }

        /// <summary>
        /// Get room furnishments from database by room id
        /// </summary>
        /// <param name="id">id of room</param>
        /// <returns>List of roomfurnishments</returns>
        public static List<roomfurnishment> GetRoomfurnishmentsByRoomId(int id)
        {
            log.Info("RoomfurnishmentAdministration - GetRoomfurnishmentsByRoomId(int id)");

            List<roomfurnishment> rfList = new List<roomfurnishment>();

            try
            {
                using (var context = new innovations4austriaEntities())
                {
                    rfList = context.roomfurnishments.Include("furnishment").Include("room").Where(x => x.room_id == id).ToList();
                }
            }
            catch (Exception ex)
            {
                log.Error("Error getting roomfurnishments by room id", ex);
            }

            return rfList;
        }

        /// <summary>
        /// Update roomfurnishments for room
        /// </summary>
        /// <param name="roomId">id of room</param>
        /// <param name="newFurnishments">List of new roomfurnishments</param>
        /// <returns>true if success, false if anything went wrong</returns>
        public static bool UpdateRoomfurnishmentsForRoom(int roomId, List<roomfurnishment> newFurnishments)
        {
            log.Info("RoomfurnishmentAdministration - UpdateRoomfurnishmentsForRoom(int roomId, List<roomfurnishment> newFurnishments)");

            bool success = false;

            try
            {
                using (var context = new innovations4austriaEntities())
                {
                    List<roomfurnishment> oldFurnishments = context.roomfurnishments.Where(x => x.room_id == roomId).ToList();

                    oldFurnishments = newFurnishments;

                    context.SaveChanges();
                    return success = true;
                }
            }
            catch (Exception ex)
            {
                log.Error("Error updating roomfurnishments for room", ex);
            }

            return success;
        }
    }
}
