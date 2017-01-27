using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using innovation4austria.dataAccess;

namespace innovation4austria.logic
{
    public class FurnishmentAdministration
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static List<furnishment> GetAllFurnishments()
        {
            log.Info("GetAllFurnishments()");

            List<furnishment> allFurnishments = new List<furnishment>();

            try
            {
                using (var context = new innovations4austriaEntities())
                {
                    allFurnishments.AddRange(context.furnishments.Include("roomfurnishments"));
                }
            }
            catch (Exception ex)
            {
                log.Error("Error getting all furnishments", ex);
            }

            return allFurnishments;
        }
    }
}
