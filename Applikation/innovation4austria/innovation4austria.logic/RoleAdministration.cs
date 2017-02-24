using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using innovation4austria.dataAccess;

namespace innovation4austria.logic
{
    public class RoleAdministration
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Get all Roles from Database
        /// </summary>
        /// <returns>List of Roles</returns>
        public static List<role> GetAllRoles()
        {
            log.Info("RoleAdministration - GetAllRoles()");

            List<role> allRoles = new List<role>();

            try
            {
                using (var context = new innovations4austriaEntities())
                {
                    allRoles.AddRange(context.roles.Include("portalusers").ToList());
                }
            }
            catch (Exception ex)
            {
                log.Error("Error getting all Roles", ex);
            }

            return allRoles;
        }

        /// <summary>
        /// Get Role by RoleId
        /// </summary>
        /// <param name="roleId">id of role</param>
        /// <returns>Role Object</returns>
        public static role GetRoleById(int roleId)
        {
            log.Info("RoleAdministration - GetRoleById(int roleId)");

            role roleById = new role();

            try
            {
                using (var context = new innovations4austriaEntities())
                {
                    roleById = context.roles.Include("portalusers").Where(x => x.id == roleId).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                log.Error("Error getting role by id", ex);
            }

            return roleById;
        }
    }
}
