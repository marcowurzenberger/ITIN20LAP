using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace innovations4austria.logic
{
    /// <summary>
    /// Helperclass which provide Methods e.g. to load all portalusers
    /// </summary>
    public class PortaluserAdministration
    {
        /// <summary>
        /// Loading all portalusers incl. their companies and roles
        /// </summary>
        /// <returns>List of all portalusers</returns>
        public static List<portaluser> LoadAllPortalusers()
        {
            Debug.WriteLine("portaluserAdministration - LoadAllPortalusers");
            Debug.Indent();

            List<portaluser> allPortalusers = new List<portaluser>();

            try
            {
                using (var context = new innovations4austriaEntities())
                {
                    foreach (var pu in context.portalusers.Include("companies").Include("roles"))
                    {
                        allPortalusers.Add(new portaluser()
                        {
                            company = pu.company,
                            company_id = pu.company_id,
                            email = pu.email,
                            firstname = pu.firstname,
                            id = pu.id,
                            lastname = pu.lastname,
                            password = pu.password,
                            role = pu.role,
                            role_id = pu.role_id
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error at loading all portalusers");
                Debug.WriteLine(ex.Message);
                Debugger.Break();
            }

            Debug.Unindent();
            return allPortalusers;
        }
    }
}
