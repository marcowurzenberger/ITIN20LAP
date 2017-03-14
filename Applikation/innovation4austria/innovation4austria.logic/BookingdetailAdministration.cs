using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using innovation4austria.dataAccess;

namespace innovation4austria.logic
{
    public class BookingdetailAdministration
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Get all bookingdetails from database
        /// </summary>
        /// <returns>List of all bookingdetails</returns>
        public static List<bookingdetail> GetAllBookingdetails()
        {
            log.Info("GetAllBookingdetails()");

            List<bookingdetail> allBookingdetails = new List<bookingdetail>();

            try
            {
                using (var context = new innovations4austriaEntities())
                {
                    foreach (var item in context.bookingdetails.Include("booking"))
                    {
                        allBookingdetails.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("Error getting all bookingdetails", ex);
            }

            return allBookingdetails;
        }

        /// <summary>
        /// Get all bookingdetails from database where booking_id matches
        /// </summary>
        /// <param name="bookingId">the booking_id to match</param>
        /// <returns>List of all bookingdetails with matched booking_id</returns>
        public static List<bookingdetail> GetAllBookingdetailsByBookingId(int bookingId)
        {
            log.Info("GetAllBookingdetailsByBookingId(int bookingId)");

            List<bookingdetail> allBookingdetails = new List<bookingdetail>();

            try
            {
                using (var context = new innovations4austriaEntities())
                {
                    foreach (var item in context.bookings)
                    {
                        if (item.id == bookingId)
                        {
                            allBookingdetails.AddRange(item.bookingdetails);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("Error getting all bookingdetails by booking_id", ex);
            }

            return allBookingdetails;
        }

        /// <summary>
        /// Get all bookingdetails from database where company match
        /// </summary>
        /// <param name="company">name of company</param>
        /// <returns>List of filtered bookingdetails</returns>
        public static List<bookingdetail> GetAllBookingdetailsByCompany(string company)
        {
            log.Info("GetAllBookingdetailsByCompany(string company)");

            List<bookingdetail> allBookingdetails = new List<bookingdetail>();
            List<bookingdetail> filteredList = new List<bookingdetail>();

            try
            {
                using (var context = new innovations4austriaEntities())
                {
                    allBookingdetails = context.bookingdetails.ToList();

                    filteredList = context.bookingdetails.Include("booking").Include("bill").Where(x => x.booking.company.name == company).ToList();
                }

                if (filteredList == null || filteredList.Count == 0)
                {
                    return allBookingdetails;
                }
            }
            catch (Exception ex)
            {
                log.Error("Error getting all bookingdetails by company", ex);
            }

            return filteredList;
        }

        /// <summary>
        /// Get all bookingdetails from database between start- and enddate
        /// <param name="start">start date</param>
        /// <param name="end">end date</param>
        /// </summary>
        /// <returns>List of all bookingdetails between tow dates</returns>
        public static List<bookingdetail> GetAllBookingdetails(DateTime start, DateTime end)
        {
            log.Info("GetAllBookingdetails()");

            List<bookingdetail> allBookingdetails = new List<bookingdetail>();

            try
            {
                using (var context = new innovations4austriaEntities())
                {
                    foreach (var item in context.bookingdetails.Include("booking").Where(x => x.booking_date >= start && x.booking_date <= end))
                    {
                        allBookingdetails.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("Error getting all bookingdetails", ex);
            }

            return allBookingdetails;
        }

        /// <summary>
        /// Get all bookingdetails from database by company name and room id
        /// </summary>
        /// <param name="company">name of company</param>
        /// <param name="roomId">id of room</param>
        /// <param name="bookingId">id of booking</param>
        /// <returns>List of bookingdetails</returns>
        public static List<bookingdetail> GetBookingdetailsByCompanyRoomIdAndBookingId(string company, int roomId, int bookingId)
        {
            log.Info("BookingdetailAdministration - GetBookingdetailsByCompanyAndRoom(string company, int roomId)");

            List<bookingdetail> bookingdetails = new List<bookingdetail>();

            try
            {
                using (var context = new innovations4austriaEntities())
                {
                    bookingdetails = context.bookingdetails.Include("booking").Include("bill").Where(x => x.booking.company.name == company && x.booking.room_id == roomId && x.booking.id == bookingId).ToList();
                }
            }
            catch (Exception ex)
            {
                log.Error("Error getting bookingdetails by company and room", ex);
            }

            return bookingdetails;
        }
    }
}
