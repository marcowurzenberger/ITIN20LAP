using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;
using log4net;
using log4net.Config;
using System.Diagnostics;
using innovation4austria.dataAccess;

namespace innovation4austria.authentication
{
    public class MyRoleProvider : RoleProvider
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetRolesForUser(string username)
        {
            XmlConfigurator.Configure();

            log.Info("GetRolesForUser(string username)");

            string[] roles = new string[3];

            try
            {
                if (!string.IsNullOrEmpty(username))
                {
                    using (var context = new innovations4austriaEntities())
                    {
                        
                    }
                }
                else
                {
                    log.Warn("username is null or empty!");
                    return null;
                }
            }
            catch (Exception ex)
            {
                log.Error("Error getting all Roles", ex);
                Debugger.Break();
            }

            return roles;
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}
