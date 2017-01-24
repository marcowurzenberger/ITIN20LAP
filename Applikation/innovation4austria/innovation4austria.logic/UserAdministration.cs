using innovation4austria.dataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using log4net.Config;

namespace innovation4austria.logic
{
    public class UserAdministration
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Get all Users from DB with their company, logs and role
        /// </summary>
        /// <returns>List of all User</returns>
        public static List<portaluser> GetAllUsers()
        {
            XmlConfigurator.Configure();

            log.Info("GetAllUsers()");

            List<portaluser> allUser = new List<portaluser>();

            try
            {
                using (var context = new innovations4austriaEntities())
                {
                    foreach (var u in context.portalusers.Include("companies").Include("logs").Include("roles"))
                    {
                        allUser.Add(new portaluser()
                        {
                            active = u.active,
                            company = u.company,
                            company_id = u.company_id,
                            email = u.email,
                            firstname = u.firstname,
                            id = u.id,
                            lastname = u.lastname,
                            logs = u.logs,
                            password = u.password,
                            role = u.role,
                            role_id = u.role_id
                        });
                    }

                    return allUser;
                }
            }
            catch (Exception ex)
            {
                log.Error("Error getting all User", ex);
            }

            return allUser;
        }
    }
}
