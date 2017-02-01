using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;
using System.Security;
using System.Web.Security;
using log4net.Config;
using log4net;
using innovation4austria.dataAccess;
using System.Security.Cryptography;
using System.Diagnostics;

namespace innovation4austria.authentication
{
    public class i4aMembershipProvider : MembershipProvider
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

        public override bool EnablePasswordReset
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override bool EnablePasswordRetrieval
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override int MaxInvalidPasswordAttempts
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override int MinRequiredNonAlphanumericCharacters
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override int MinRequiredPasswordLength
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override int PasswordAttemptWindow
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override MembershipPasswordFormat PasswordFormat
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override string PasswordStrengthRegularExpression
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override bool RequiresQuestionAndAnswer
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override bool RequiresUniqueEmail
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override int GetNumberOfUsersOnline()
        {
            throw new NotImplementedException();
        }

        public override string GetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            throw new NotImplementedException();
        }

        public override string GetUserNameByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public override string ResetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override bool UnlockUser(string userName)
        {
            throw new NotImplementedException();
        }

        public override void UpdateUser(MembershipUser user)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Validate User for Login
        /// </summary>
        /// <param name="username">Email-Address</param>
        /// <param name="password">Password</param>
        /// <returns>true or false</returns>
        public override bool ValidateUser(string username, string password)
        {
            log.Info("ValidateUser(username, password)");

            bool validate = false;

            try
            {
                using (var context = new innovations4austriaEntities())
                {
                    portaluser user = context.portalusers.Where(x => x.email == username).FirstOrDefault();

                    if (user != null)
                    {
                        if (user.password.SequenceEqual(HashPassword(password)))
                        {
                            return validate = true;
                        }
                    }
                    else
                    {
                        log.Warn("Email is null");
                        return validate = false;
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("Error at Validating User", ex);
                return validate = false;
            }

            return validate;
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
                Debugger.Break();
            }

            return hashedPassword;
        }
    }
}
