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
    public sealed class i4aRoleProvider : RoleProvider
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

        /// <summary>
        /// Get all Roles
        /// </summary>
        /// <returns>string array with rolenames</returns>
        public override string[] GetAllRoles()
        {
            XmlConfigurator.Configure();

            log.Info("GetAllRoles()");

            string[] roles = new string[2];

            try
            {
                using (var context = new innovations4austriaEntities())
                {
                    List<role> dbRoles = new List<role>();
                    dbRoles = context.roles.ToList();

                    for (int i = 0; i < dbRoles.Count; i++)
                    {
                        roles[i] = dbRoles[i].description;
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("Error getting all roles", ex);
            }

            return roles;
        }

        /// <summary>
        /// Get all Roles for User
        /// </summary>
        /// <param name="username">email of user</param>
        /// <returns>string array with rolenames</returns>
        public override string[] GetRolesForUser(string username)
        {
            XmlConfigurator.Configure();

            log.Info("GetRolesForUser(string username)");
            List<string> roleList = new List<string>();
            
            try
            {
                if (!string.IsNullOrEmpty(username))
                {
                    using (var context = new innovations4austriaEntities())
                    {
                        roleList = (from pu in context.portalusers where pu.email == username select pu.role.description).ToList<string>();
                        
                    }
                }
                else
                {
                    log.Warn("username is null or empty!");
                }
            }
            catch (Exception ex)
            {
                log.Error("Error getting all Roles", ex);
                Debugger.Break();
            }

            return roleList.ToArray<string>();
        }

        /// <summary>
        /// Get all Users for Role
        /// </summary>
        /// <param name="roleName">Rolename</param>
        /// <returns>string array with usernames</returns>
        public override string[] GetUsersInRole(string roleName)
        {
            XmlConfigurator.Configure();

            log.Info("GetUsersInRole(string roleName)");

            string[] users = new string[100];

            try
            {
                if (!string.IsNullOrEmpty(roleName))
                {
                    using (var context = new innovations4austriaEntities())
                    {
                        return null;
                    }
                }
                else
                {
                    log.Warn("roleName is null or empty");
                    return users = null;
                }
            }
            catch (Exception ex)
            {
                log.Error("Error getting all users in role", ex);
                Debugger.Break();
            }

            return users;
        }

        /// <summary>
        /// Find out if User is Member of a specific Role
        /// e.g. is User member of innovation4austria?
        /// </summary>
        /// <param name="username">Email-Address</param>
        /// <param name="roleName">Role</param>
        /// <returns>true or false</returns>
        public override bool IsUserInRole(string username, string roleName)
        {
            log.Info("IsUserInRole(string username, string roleName)");

            bool exist = false;

            try
            {
                if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(roleName))
                {
                    using (var context = new innovations4austriaEntities())
                    {
                        foreach (var r in context.roles)
                        {
                            if (r.description == roleName)
                            {
                                foreach (var u in r.portalusers)
                                {
                                    if (username == u.email)
                                    {
                                        return exist = true;
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    return exist = false;
                }
            }
            catch (Exception ex)
            {
                log.Error("Error at IsUserInRole", ex);
                Debugger.Break();
            }

            return exist;
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
