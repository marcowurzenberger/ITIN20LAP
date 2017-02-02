using innovation4austria.dataAccess;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace innovation4austria.logic
{
    public class PortaluserAdministration
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Get all users from Database
        /// </summary>
        /// <returns>List of all users</returns>
        public static List<portaluser> GetAllUser()
        {
            log.Info("GetAllUser()");

            List<portaluser> allUsers = new List<portaluser>();

            try
            {
                using (var context = new innovations4austriaEntities())
                {
                    allUsers.AddRange(context.portalusers.Include("company").Include("role"));
                }
            }
            catch (Exception ex)
            {
                log.Error("Error getting all users", ex);
            }

            return allUsers;
        }

        /// <summary>
        /// Get all users from Database where company is xxx
        /// </summary>
        /// <param name="company">name of company</param>
        /// <returns>List of all users in one company</returns>
        public static List<portaluser> GetAllUserByCompany(string company)
        {
            log.Info("GetAllUserByCompany(string company)");

            List<portaluser> user = new List<portaluser>();
            user = GetAllUser();

            List<portaluser> filteredUsers = new List<portaluser>();

            try
            {
                using (var context = new innovations4austriaEntities())
                {
                    if (user != null && user.Count > 0)
                    {
                        foreach (var u in user)
                        {
                            if (u.company.name == company)
                            {
                                filteredUsers.Add(u);
                            }
                        } 
                    }
                    else
                    {
                        log.Warn("user is null or Count is 0");
                        return user;
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("Error getting all users by company", ex);
            }

            return filteredUsers;
        }

        /// <summary>
        /// Get Companyname from Database where useremail equals parameter
        /// </summary>
        /// <param name="email">email from actually logged in user</param>
        /// <returns>name of company</returns>
        public static string GetCompanyNameByUserMail(string email)
        {
            log.Info("GetCompanyNameByUserMail(string email)");

            string companyName = "";

            try
            {
                using (var context = new innovations4austriaEntities())
                {
                    foreach (var item in context.portalusers)
                    {
                        if (item.email == email)
                        {
                            companyName = item.company.name;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("Error getting company name by user email", ex);
            }

            return companyName;
        }
    }
}
