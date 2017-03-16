using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using innovation4austria.dataAccess;

namespace innovation4austria.logic
{
    public class RoomAdministration
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Get all rooms from Database
        /// </summary>
        /// <returns>List of all Rooms</returns>
        public static List<room> GetAllRooms()
        {
            log.Info("GetAllRooms()");

            List<room> allRooms = new List<room>();

            try
            {
                using (var context = new innovations4austriaEntities())
                {
                    allRooms.AddRange(context.rooms.Include("bookings").Include("facility").Include("roomfurnishments"));
                }
            }
            catch (Exception ex)
            {
                log.Error("Error getting all rooms", ex);
            }

            return allRooms;
        }

        /// <summary>
        /// Get all rooms from Database by from and to a specific Date
        /// </summary>
        /// <param name="start">Date where Room should be available begins</param>
        /// <param name="end">Date where Room should be available ends</param>
        /// <returns></returns>
        public static List<room> GetAllRoomsByDate(DateTime start, DateTime end)
        {
            log.Info("GetAllRoomsByDate(DateTime fromDate, DateTime toDate)");

            List<room> filteredRooms = new List<room>();

            try
            {
                using (var context = new innovations4austriaEntities())
                {
                    List<int?> roomIds = context.sp_getFilteredRoomIds(start, end).ToList();

                    foreach (var item in roomIds)
                    {
                        filteredRooms.Add(context.rooms.Where(x => x.id == item).FirstOrDefault());
                    }

                    return filteredRooms;
                }
            }
            catch (Exception ex)
            {
                log.Error("Error filtering rooms", ex);
            }

            return filteredRooms;
        }

        /// <summary>
        /// Get all rooms from Database by a specific furnishment
        /// </summary>
        /// <param name="furnishment">description of furnishment</param>
        /// <returns>filtered List of Rooms</returns>
        public static List<room> GetAllRoomsByFurnishment(string furnishment)
        {
            log.Info("GetAllRoomsByFurnishment(string furnishment)");

            List<room> roomList = new List<room>();
            roomList = GetAllRooms();

            List<room> filteredList = new List<room>();

            try
            {
                if (roomList != null && roomList.Count > 0)
                {
                    foreach (var r in roomList)
                    {
                        foreach (var f in r.roomfurnishments)
                        {
                            if (f.furnishment.description == furnishment)
                            {
                                filteredList.Add(r);
                            }
                        }
                    }
                }
                else
                {
                    log.Warn("roomlist is null or count is 0");
                    return roomList;
                }
            }
            catch (Exception ex)
            {
                log.Error("Error getting all rooms by furnishment", ex);
            }

            return filteredList;
        }

        /// <summary>
        /// Get all rooms booked by a company
        /// </summary>
        /// <param name="company">name of the company</param>
        /// <returns>List of all rooms, which booked by the company</returns>
        public static List<room> GetAllRoomsByCompany(string company)
        {
            log.Info("GetAllRoomsByCompany(string company)");

            List<room> roomList = new List<room>();
            List<room> filteredList = new List<room>();
            List<booking> allBookings = new List<booking>();

            try
            {
                using (var context = new innovations4austriaEntities())
                {
                    roomList = GetAllRooms();
                    allBookings = BookingAdministration.GetAllBookingsByCompany(company);

                    if (roomList != null && roomList.Count > 0)
                    {
                        foreach (var r in roomList)
                        {
                            foreach (var b in allBookings)
                            {
                                if (r.id == b.room_id)
                                {
                                    filteredList.Add(r);
                                }
                            }
                        }
                    }
                    else
                    {
                        log.Warn("roomList is null or Count is 0");
                        return roomList;
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("Error getting all Rooms by Company", ex);
            }

            return filteredList;
        }

        /// <summary>
        /// Get all rooms filtered by furnishments, start and enddate
        /// </summary>
        /// <param name="furnishmentIDs">List of furnishment-IDs</param>
        /// <param name="fromDate">startdate</param>
        /// <param name="toDate">enddate</param>
        /// <returns>List of rooms</returns>
        public static List<room> GetFilteredRooms(List<int> furnishmentIDs, DateTime fromDate, DateTime toDate)
        {
            log.Info("GetFilteredRooms(...)");

            List<room> filteredRooms = new List<room>();

            try
            {
                using (var context = new innovations4austriaEntities())
                {
                    #region Old Bad Filter
                    //List<int?> roomIds = context.sp_getFilteredRoomIds(fromDate, toDate).ToList();

                    //foreach (var item in roomIds)
                    //{
                    //    filteredRooms.Add(context.rooms.Where(x => x.id == item).FirstOrDefault());
                    //}

                    //tempRooms.AddRange(filteredRooms);

                    //foreach (var f in furnishmentIDs)
                    //{
                    //    foreach (var r in tempRooms)
                    //    {
                    //        if (r.roomfurnishments.Any(x => x.furnishment_id == f))
                    //        {
                    //            // Nimm keinen Raum weg, wenn gesuchte Ausstattung vorhanden ist
                    //        }
                    //        else
                    //        {
                    //            filteredRooms.Remove(r);
                    //        }
                    //    }
                    //} 
                    #endregion

                    #region Good New Filter

                    // Get all booked rooms filtered by Date
                    List<room> bookedRooms = context.bookingdetails.Where(x => x.booking_date >= fromDate && x.booking_date <= toDate).Select(x => x.booking.room).ToList();

                    //Exclude booked rooms from all Rooms
                    List<room> availableRooms = context.rooms.ToList();
                    availableRooms = availableRooms.Except(bookedRooms).ToList();

                    //Filter rooms by given Furnishment Criteria
                    filteredRooms = availableRooms.Where(x => furnishmentIDs.All(y => x.roomfurnishments.Any(z => z.furnishment_id == y))).ToList();

                    #endregion
                }
            }
            catch (Exception ex)
            {
                log.Error("Error getting filtered rooms", ex);
            }

            return filteredRooms;
        }

        /// <summary>
        /// Get room by room-id
        /// </summary>
        /// <param name="id">room id</param>
        /// <returns>room object</returns>
        public static room GetRoomById(int id)
        {
            log.Info("RoomAdministration - GetRoomById(int id)");

            room retRoom = new room();

            try
            {
                using (var context = new innovations4austriaEntities())
                {
                    retRoom = context.rooms.Include("bookings").Include("facility").Include("roomfurnishments").Where(x => x.id == id).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                log.Error("Error getting Room by Id", ex);
            }

            return retRoom;
        }

        /// <summary>
        /// Booking Room and insert into database 
        /// </summary>
        /// <param name="id">id of room</param>
        /// <param name="start">startdate of booking</param>
        /// <param name="end">enddate of booking</param>
        /// <param name="email">email of current loggedin user</param>
        /// <returns>true if succeed, false if anything went wrong</returns>
        public static bool BookingRoom(int id, DateTime start, DateTime end, string email)
        {
            log.Info("RoomAdministration - BookingRoom(int id, DateTime start, DateTime end)");

            bool success = false;

            try
            {
                using (var context = new innovations4austriaEntities())
                {
                    company comp = CompanyAdministration.GetCompanyByUserEmail(email);

                    room r = GetRoomById(id);

                    booking b = new booking() { company_id = comp.id, room_id = id };

                    context.SaveChanges();

                    TimeSpan range = end.Subtract(start);

                    List<bookingdetail> bDetails = new List<bookingdetail>();

                    for (int i = 0; i <= range.Days; i++)
                    {
                        bDetails.Add(new bookingdetail()
                        {
                            booking_id = b.id,
                            booking_date = start.AddDays(i),
                            price = r.price
                        });
                    }

                    b.bookingdetails = bDetails;

                    context.bookings.Add(b);
                    context.bookingdetails.AddRange(bDetails);

                    context.SaveChanges();
                    return success = true;
                }
            }
            catch (Exception ex)
            {
                log.Error("Error booking room", ex);
            }

            return success;
        }

        /// <summary>
        /// Get room from database by booking id
        /// </summary>
        /// <param name="bookingId">id of booking</param>
        /// <returns>room object</returns>
        public static room GetRoomByBookingId(int bookingId)
        {
            log.Info("RoomAdministration - GetRoomByBookingId(int bookingId)");

            room r = new room();

            try
            {
                using (var context = new innovations4austriaEntities())
                {
                    r = context.rooms.Include("bookings").Include("facility").Include("roomfurnishments").Where(x => x.bookings.Select(y => y.id == bookingId).FirstOrDefault()).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                log.Error("Error getting room by booking id", ex);
            }

            return r;
        }

        /// <summary>
        /// Update room in database
        /// </summary>
        /// <param name="updatedRoom">updated room</param>
        /// <param name="updatedFurnishments">updated roomfurnishments</param>
        /// <returns>true if success, false if anything went wrong</returns>
        public static bool UpdateRoom(room updatedRoom, List<roomfurnishment> updatedFurnishments)
        {
            log.Info("RoomAdministration - UpdateRoom()");

            bool success = false;

            try
            {
                using (var context = new innovations4austriaEntities())
                {
                    context.sp_UpdateRoom(updatedRoom.id, updatedRoom.description, updatedRoom.facility_id, updatedRoom.price);

                    room oldRoom = context.rooms.Include("bookings").Include("facility").Include("roomfurnishments").Where(x => x.id == updatedRoom.id).FirstOrDefault();

                    foreach (var item in oldRoom.roomfurnishments)
                    {
                        foreach (var f in updatedFurnishments)
                        {
                            if (item.furnishment_id != f.furnishment_id)
                            {
                                context.sp_UpdateRoomfurnishments(oldRoom.id, item.furnishment_id, f.furnishment_id);
                            }
                        }
                    }

                    context.SaveChanges();
                    return success = true;
                }
            }
            catch (Exception ex)
            {
                log.Error("Error updating room", ex);
            }

            return success;
        }

        /// <summary>
        /// Get all booked rooms from database
        /// </summary>
        /// <returns>List of all booked rooms</returns>
        public static List<room> GetAllBookedRooms()
        {
            log.Info("RoomAdministration - GetAllBookedRooms()");

            List<room> bookedRooms = new List<room>();

            try
            {
                using (var context = new innovations4austriaEntities())
                {
                    bookedRooms = context.rooms.Where(x => x.bookings == null || x.bookings.Count < 1).ToList();
                }
            }
            catch (Exception ex)
            {
                log.Error("Error getting all booked rooms", ex);
            }

            return bookedRooms;
        }

        /// <summary>
        /// Get all booked Rooms between two dates
        /// </summary>
        /// <param name="start">startdate</param>
        /// <param name="end">enddate</param>
        /// <returns>List of rooms</returns>
        public static List<room> GetAllBookedRoomsByDate(DateTime start, DateTime end)
        {
            log.Info("RoomAdministration - GetAllBookedRoomsByDate(DateTime start, DateTime end)");

            List<room> filteredRooms = new List<room>();

            try
            {
                using (var context = new innovations4austriaEntities())
                {
                    List<int?> roomIds = context.sp_getRoomIdsBetweenDates(start, end).ToList();

                    foreach (var item in roomIds)
                    {
                        filteredRooms.Add(context.rooms.Include("bookings").Include("facility").Include("roomfurnishments").Where(x => x.id == item && x.bookings.Count > 0).FirstOrDefault());
                    }

                    return filteredRooms;
                }
            }
            catch (Exception ex)
            {
                log.Error("Error filtering rooms", ex);
            }

            return filteredRooms;
        }
    }
}
