using innovation4austria.dataAccess;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
        /// Get all users from Database with Company ID
        /// </summary>
        /// <param name="companyId">id of company</param>
        /// <returns>List of all portalusers in one company</returns>
        public static List<portaluser> GetAllUserByCompanyId(int companyId)
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
                            if (u.company_id == companyId)
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

        /// <summary>
        /// Get Portaluser from Database by Id
        /// </summary>
        /// <param name="id">Id of User</param>
        /// <returns>Portaluser-object</returns>
        public static portaluser GetUserById(int id)
        {
            log.Info("GetUserById(int id)");

            portaluser user = new portaluser();

            try
            {
                using (var context = new innovations4austriaEntities())
                {
                    foreach (var pu in context.portalusers.Include("role").Include("company"))
                    {
                        if (pu.id == id)
                        {
                            user = pu;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("Error getting user by id", ex);
            }

            return user;
        }

        /// <summary>
        /// Get Id from Database by Email
        /// </summary>
        /// <param name="email">email of user</param>
        /// <returns>id of user</returns>
        public static int GetIdFromUser(string email)
        {
            log.Info("GetIdFromUser(string email)");

            int id = 0;

            try
            {
                using (var context = new innovations4austriaEntities())
                {
                    foreach (var item in context.portalusers)
                    {
                        if (item.email == email)
                        {
                            id = item.id;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("Error getting id from user", ex);
            }

            return id;
        }

        /// <summary>
        /// Change firstname from user by email
        /// </summary>
        /// <param name="email">email of user</param>
        /// <param name="newFirstname">new firstname of user</param>
        /// <returns>true when success, false if anything went wrong</returns>
        public static bool ChangeFirstname(string email, string newFirstname)
        {
            log.Info("ChangeFirstname(string email, string newFirstname)");

            bool success = false;

            try
            {
                using (var context = new innovations4austriaEntities())
                {
                    if (!string.IsNullOrEmpty(newFirstname) && !string.IsNullOrEmpty(email))
                    {
                        foreach (var item in context.portalusers)
                        {
                            if (item.email == email)
                            {
                                item.firstname = newFirstname;
                                success = true;
                            }
                        }
                    }
                    else
                    {
                        return success = false;
                    }

                    context.SaveChanges();
                    return success;
                }
            }
            catch (Exception ex)
            {
                log.Error("Error changing firstname", ex);
            }

            return success;
        }

        /// <summary>
        /// Change lastname from user by email
        /// </summary>
        /// <param name="email">email of user</param>
        /// <param name="newLastname">new lastname of user</param>
        /// <returns>true when success, false if anything went wrong</returns>
        public static bool ChangeLastname(string email, string newLastname)
        {
            log.Info("ChangeLastname(string email, string newLastname)");

            bool success = false;

            try
            {
                using (var context = new innovations4austriaEntities())
                {
                    if (!string.IsNullOrEmpty(newLastname) && !string.IsNullOrEmpty(email))
                    {
                        foreach (var item in context.portalusers)
                        {
                            if (item.email == email)
                            {
                                item.lastname = newLastname;
                                success = true;
                            }
                        }
                    }
                    else
                    {
                        return success = false;
                    }

                    context.SaveChanges();
                    return success;
                }
            }
            catch (Exception ex)
            {
                log.Error("Error changing lastname", ex);
            }

            return success;
        }

        /// <summary>
        /// Change password from user by email and old password
        /// </summary>
        /// <param name="email">email of user</param>
        /// <param name="oldPassword">old password</param>
        /// <param name="newPassword">new password</param>
        /// <returns>true when success, false if anything went wrong</returns>
        public static bool ChangePassword(string email, string oldPassword, string newPassword)
        {
            log.Info("ChangePassword(string email, string oldPassword, string newPassword)");

            bool success = false;

            try
            {
                using (var context = new innovations4austriaEntities())
                {
                    if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(oldPassword) && !string.IsNullOrEmpty(newPassword))
                    {
                        foreach (var item in context.portalusers)
                        {
                            if (item.email == email)
                            {
                                if (item.password.SequenceEqual(HashPassword(oldPassword)))
                                {
                                    item.password = HashPassword(newPassword);
                                    success = true;
                                }
                                else
                                {
                                    return success;
                                }
                            }
                        }
                    }
                    else
                    {
                        return success;
                    }

                    context.SaveChanges();
                    return success;
                }
            }
            catch (Exception ex)
            {
                log.Error("Error changing password", ex);
            }

            return success;
        }

        /// <summary>
        /// Hashes a password into a byte-Array
        /// </summary>
        /// <param name="password">Password in clear text</param>
        /// <returns>Hashed Password as Byte-Array</returns>
        public static byte[] HashPassword(string password)
        {
            log.Info("HashPassword(string password)");

            byte[] hashedPassword = null;

            try
            {

                if (string.IsNullOrEmpty(password))
                {
                    log.Warn("Password is null or empty");
                    return hashedPassword;
                }

                SHA256 hash = SHA256.Create();

                hashedPassword = hash.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
            catch (Exception ex)
            {
                log.Error("Error at hashing password", ex);
            }

            return hashedPassword;
        }
    }
}
