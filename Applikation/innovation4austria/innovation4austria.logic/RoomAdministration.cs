﻿using System;
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
        /// <param name="fromDate">Date where Room should be available begins</param>
        /// <param name="toDate">Date where Room should be available ends</param>
        /// <returns></returns>
        public static List<room> GetAllRoomsByDate(DateTime fromDate, DateTime toDate)
        {
            log.Info("GetAllRoomsByDate(DateTime fromDate, DateTime toDate)");

            List<room> roomList = new List<room>();
            roomList = GetAllRooms();

            List<room> filteredList = new List<room>();

            try
            {
                if (roomList != null && roomList.Count > 0)
                {
                    foreach (var r in roomList)
                    {
                        foreach (var b in r.bookings)
                        {
                            foreach (var bd in b.bookingdetails)
                            {
                                if (bd.bookingdate >= fromDate && bd.bookingdate <= toDate)
                                {
                                    filteredList.Add(r);
                                }
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
            catch (Exception ex)
            {
                log.Error("Error getting all rooms by date", ex);
            }

            return filteredList;
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
    }
}
