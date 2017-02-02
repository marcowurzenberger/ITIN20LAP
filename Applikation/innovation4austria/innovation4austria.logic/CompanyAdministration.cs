using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using innovation4austria.dataAccess;

namespace innovation4austria.logic
{
    public class CompanyAdministration
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Get all companies from database
        /// </summary>
        /// <returns>List of all companies</returns>
        public static List<company> GetAllCompanies()
        {
            log.Info("GetAllCompanies()");

            List<company> allCompanies = new List<company>();

            try
            {
                using (var context = new innovations4austriaEntities())
                {
                    foreach (var c in context.companies.Include("bookings").Include("portalusers"))
                    {
                        allCompanies.Add(c);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("Error getting all companies", ex);
            }

            return allCompanies;
        }
    }
}
