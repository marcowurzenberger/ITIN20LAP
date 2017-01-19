using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace innovations4austria.logic
{
    /// <summary>
    /// Helperclass which provide Methods e.g. to load all roles
    /// </summary>
    public class RoleAdministration
    {
        /// <summary>
        /// Load all roles incl. their portalusers
        /// </summary>
        /// <returns></returns>
        public static List<role> LoadAllRoles()
        {
            Debug.WriteLine("roleAdministration -  LoadAllRoles()");
            Debug.Indent();

            List<role> allRoles = new List<role>();

            try
            {
                using (var context = new innovations4austriaEntities())
                {
                    foreach (var r in context.roles.Include("portalusers"))
                    {
                        allRoles.Add(new role()
                        {
                            description = r.description,
                            id = r.id,
                            portalusers = r.portalusers
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error at loading all roles");
                Debug.WriteLine(ex.Message);
                Debugger.Break();
            }

            Debug.Unindent();
            return allRoles;
        }
    }
}
