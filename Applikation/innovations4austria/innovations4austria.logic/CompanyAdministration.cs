using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace innovations4austria.logic
{
    /// <summary>
    /// Helperclass which provide Methods e.g. to load all companies
    /// </summary>
    public class CompanyAdministration
    {
        /// <summary>
        /// Loading all companies incl. their bookings and portalusers
        /// </summary>
        /// <returns>List of all companies</returns>
        public static List<company> LoadAllCompanies()
        {
            Debug.WriteLine("companyAdministration - LoadAllCompanies()");
            Debug.Indent();

            List<company> allCompanies = new List<company>();

            try
            {
                using (var context = new innovations4austriaEntities())
                {
                    foreach (var c in context.companies.Include("bookings").Include("portalusers"))
                    {
                        allCompanies.Add(new company()
                        {
                            id = c.id,
                            bookings = c.bookings,
                            city = c.city,
                            name = c.name,
                            number = c.number,
                            portalusers = c.portalusers,
                            street = c.street,
                            zip = c.zip
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error at Loading all companies");
                Debug.WriteLine(ex.Message);
                Debugger.Break();
            }

            Debug.Unindent();
            return allCompanies;
        }
    }
}
