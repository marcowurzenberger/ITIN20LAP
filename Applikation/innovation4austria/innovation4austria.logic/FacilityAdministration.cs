using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using innovation4austria.dataAccess;

namespace innovation4austria.logic
{
    public class FacilityAdministration
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Get all Facilities from Database
        /// </summary>
        /// <returns>List of all Facilities</returns>
        public static List<facility> GetAllFacilities()
        {
            log.Info("GetAllFacilities()");

            List<facility> allFacilities = new List<facility>();

            try
            {
                using (var context = new innovations4austriaEntities())
                {
                    allFacilities.AddRange(context.facilities.Include("rooms"));
                }
            }
            catch (Exception ex)
            {
                log.Error("Error getting all facilities", ex);
            }

            return allFacilities;
        }
    }
}
