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

        public static List<roomfurnishment> GetAllRoomfurnishments()
        {
            log.Info("GetAllRoomfurnishments()");

            List<roomfurnishment> allRoomfurnishments = new List<roomfurnishment>();

            try
            {
                using (var context = new innovations4austriaEntities())
                {
                    allRoomfurnishments.AddRange(context.roomfurnishments);
                }
            }
            catch (Exception ex)
            {
                log.Error("Error getting all roomfurnishments", ex);
            }

            return allRoomfurnishments;
        }
    }
}
