using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace innovations4austria.logic
{
    /// <summary>
    /// Helperclass which provide helpful Methods
    /// e.g. to validate Login,...
    /// </summary>
    public class Tools
    {
        /// <summary>
        /// Validate email and password with the values in database
        /// </summary>
        /// <param name="email">email from login</param>
        /// <param name="password">password in cleartext</param>
        /// <returns>true or false</returns>
        public static bool ValidateLogin(string email, string password)
        {
            Debug.WriteLine("Tools - ValidateLogin(string email, string password)");
            Debug.Indent();

            try
            {
                if (string.IsNullOrEmpty(email))
                {
                    Debug.WriteLine("email is null or empty!");
                    Debugger.Break();
                }
                else if (string.IsNullOrEmpty(password))
                {
                    Debug.WriteLine("password is null or empty!");
                    Debugger.Break();
                }

                using (var context = new innovations4austriaEntities())
                {
                    foreach (var p in context.portalusers)
                    {
                        if (p.email == email && p.password == HashPassword(password))
                        {
                            return true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error at Validate Login");
                Debug.WriteLine(ex.Message);
                Debugger.Break();
            }

            Debug.Unindent();
            return false;
        }

        /// <summary>
        /// Hashing Password for DB
        /// </summary>
        /// <param name="password">Password in clear text</param>
        /// <returns>Bytearray (or hashed password)</returns>
        public static byte[] HashPassword(string password)
        {
            Debug.WriteLine("Tools - HashPassword(string password)");
            Debug.Indent();

            byte[] hashPassword = null;

            try
            {
                SHA256 sha = SHA256.Create();

                hashPassword = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error at Hashing Password");
                Debug.WriteLine(ex.Message);
                Debugger.Break();
            }

            Debug.Unindent();
            return hashPassword;
        }
    }
}
