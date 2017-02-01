using innovation4austria.dataAccess;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace innovation4austria.logic
{
    public class BookingAdministration
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Get all bookings from Database
        /// </summary>
        /// <returns>List of all bookings</returns>
        public static List<booking> GetAllBookings()
        {
            log.Info("GetAllBookings");

            List<booking> allBookings = new List<booking>();

            try
            {
                using (var context = new innovations4austriaEntities())
                {
                    allBookings.AddRange(context.bookings.Include("bookingdetails").Include("company").Include("room"));
                }
            }
            catch (Exception ex)
            {
                log.Error("Error getting all bookings", ex);
            }

            return allBookings;
        }

        /// <summary>
        /// Get all bookings from Database per User
        /// </summary>
        /// <param name="email">Email-Address from User</param>
        /// <returns>List of all Bookings per User</returns>
        public static List<booking> GetAllBookingsByUser(string email)
        {
            log.Info("GetAllBookingsByUser(string email)");

            List<booking> allBookings = new List<booking>();

            try
            {
                using (var context = new innovations4austriaEntities())
                {

                    foreach (var u in context.portalusers)
                    {
                        if (u.email == email)
                        {
                            allBookings.AddRange(u.company.bookings);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("Error getting all bookings for user", ex);
            }

            return allBookings;
        }

        /// <summary>
        /// Get all bookings from Database by company
        /// </summary>
        /// <param name="company">name of company</param>
        /// <returns>List of all Bookings by Company</returns>
        public static List<booking> GetAllBookingsByCompany(string company)
        {
            log.Info("GetAllBookingsByCompany(string company)");

            List<booking> bookings = new List<booking>();

            try
            {
                using (var context = new innovations4austriaEntities())
                {
                    foreach (var item in context.bookings)
                    {
                        if (item.company.name == company)
                        {
                            bookings.Add(item);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("Error getting all bookings by company", ex);
            }

            return bookings;
        }
    }
}
