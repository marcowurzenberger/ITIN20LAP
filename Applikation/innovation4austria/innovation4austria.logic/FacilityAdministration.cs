using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using innovation4austria.dataAccess;

namespace innovation4austria.logic
{
    public class FacilityAdministration
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Get all Facilities from Database
        /// </summary>
        /// <returns>List of all Facilities</returns>
        public static List<facility> GetAllFacilities()
        {
            log.Info("GetAllFacilities()");

            List<facility> allFacilities = new List<facility>();

            try
            {
                using (var context = new innovations4austriaEntities())
                {
                    allFacilities.AddRange(context.facilities.Include("rooms"));
                }
            }
            catch (Exception ex)
            {
                log.Error("Error getting all facilities", ex);
            }

            return allFacilities;
        }

        /// <summary>
        /// Creates new facility in database
        /// </summary>
        /// <param name="name">name of new facility</param>
        /// <param name="zip">zip of facility</param>
        /// <param name="city">city where facility is</param>
        /// <param name="street">street where facility is</param>
        /// <param name="number">number of street</param>
        /// <returns>true if succeed, false if anything went wrong</returns>
        public static bool CreateFacility(string name, string zip, string city, string street, string number)
        {
            log.Info("FacilityAdministration - CreateFacility(string name, string zip, string city, string street, string number)");

            bool success = false;

            try
            {
                using (var context = new innovations4austriaEntities())
                {
                    context.facilities.Add(new facility()
                    {
                        name = name,
                        zip = zip,
                        city = city,
                        street = street,
                        number = number
                    });

                    context.SaveChanges();
                    return success = true;
                }
            }
            catch (Exception ex)
            {
                log.Error("Error creating facility", ex);
            }

            return success;
        }

        /// <summary>
        /// Get facility from database by id
        /// </summary>
        /// <param name="id">id of facility</param>
        /// <returns>facility object</returns>
        public static facility GetFacilityById(int id)
        {
            log.Info("FacilityAdministration - GetFacilityById(int id)");

            facility f = new facility();

            try
            {
                using (var context = new innovations4austriaEntities())
                {
                    f = context.facilities.Where(x => x.id == id).FirstOrDefault();
                    return f;
                }
            }
            catch (Exception ex)
            {
                log.Error("Error getting facility by id", ex);
            }

            return f;
        }

        /// <summary>
        /// Edit facility with all parameters
        /// </summary>
        /// <param name="id">if of facility</param>
        /// <param name="name">name of facility</param>
        /// <param name="zip">zip where facility is</param>
        /// <param name="city">city where facility is</param>
        /// <param name="street">street where facility is</param>
        /// <param name="number">number of street</param>
        /// <returns>true if succeed, false if anything went wrong</returns>
        public static bool EditFacility(int id, string name, string zip, string city, string street, string number)
        {
            log.Info("FacilityAdministration - EditFacility(int id, string name, string zip, string city, string street, string number)");

            bool success = false;

            try
            {
                using (var context = new innovations4austriaEntities())
                {
                    facility f = context.facilities.Where(x => x.id == id).FirstOrDefault();

                    f.city = city;
                    f.name = name;
                    f.zip = zip;
                    f.street = street;
                    f.number = number;

                    context.SaveChanges();
                    return success = true;
                }
            }
            catch (Exception ex)
            {
                log.Error("Error editing facility", ex);
            }

            return success;
        }

        /// <summary>
        /// Get facility from database by room id
        /// </summary>
        /// <param name="id">id of room</param>
        /// <returns>facility object</returns>
        public static facility GetFacilityByRoomId(int id)
        {
            log.Info("FacilityAdministration - GetFacilityByRoomId(int id)");

            facility f = new facility();

            try
            {
                using (var context = new innovations4austriaEntities())
                {
                    f = context.facilities.Where(x => x.rooms.Any(y => y.id == id)).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                log.Error("Error getting facility by room id", ex);
            }

            return f;
        }
    }
}
