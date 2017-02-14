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

        /// <summary>
        /// Get all Ids from Furnishments
        /// </summary>
        /// <returns>List of Integer</returns>
        public static List<int> GetAllIDs()
        {
            log.Info("GetAllIDs()");

            List<int> furnishmentIDs = new List<int>();

            try
            {
                List<furnishment> allFurn = new List<furnishment>();

                using (var context = new innovations4austriaEntities())
                {
                    allFurn = GetAllFurnishments();

                    foreach (var item in allFurn)
                    {
                        furnishmentIDs.Add(item.id);
                    }
                }

                return furnishmentIDs;
            }
            catch (Exception ex)
            {
                log.Error("Error getting all IDs", ex);
            }

            return furnishmentIDs;
        }
    }
}
