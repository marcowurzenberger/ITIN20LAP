using innovation4austria.dataAccess;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace innovation4austria.logic
{
    public class DiscountAdministration
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Get percentage from database by user email
        /// </summary>
        /// <param name="email">email from user</param>
        /// <returns>percentage as int</returns>
        public static int GetDiscountByMail(string email)
        {
            log.Info("DiscountAdministration - GetDiscountByMail(string email)");

            int percentage = 0;

            try
            {
                using (var context = new innovations4austriaEntities())
                {
                    company comp = new company();
                    comp = CompanyAdministration.GetCompanyByUserEmail(email);

                    percentage = context.discounts.Where(x => x.company_id == comp.id).Select(x => x.percentage).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                log.Error("Error getting discount by email address", ex);
            }

            return percentage;
        }
    }
}
