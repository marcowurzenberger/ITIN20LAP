using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using innovation4austria.dataAccess;

namespace innovation4austria.logic
{
    public class RoomAdministration
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Get all rooms from Database
        /// </summary>
        /// <returns>List of all Rooms</returns>
        public static List<room> GetAllRooms()
        {
            log.Info("GetAllRooms()");

            List<room> allRooms = new List<room>();

            try
            {
                using (var context = new innovations4austriaEntities())
                {
                    allRooms.AddRange(context.rooms.Include("bookings").Include("facility").Include("roomfurnishments"));
                }
            }
            catch (Exception ex)
            {
                log.Error("Error getting all rooms", ex);
            }

            return allRooms;
        }
    }
}
