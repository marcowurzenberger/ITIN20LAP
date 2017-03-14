using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using innovation4austria.dataAccess;

namespace innovation4austria.logic
{
    public class FurnishmentAdministration
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Get all furnishments from database
        /// </summary>
        /// <returns>List of Furnishments</returns>
        public static List<furnishment> GetAllFurnishments()
        {
            log.Info("GetAllFurnishments()");

            List<furnishment> allFurnishments = new List<furnishment>();

            try
            {
                using (var context = new innovations4austriaEntities())
                {
                    allFurnishments.AddRange(context.furnishments.Include("roomfurnishments"));
                }
            }
            catch (Exception ex)
            {
                log.Error("Error getting all furnishments", ex);
            }

            return allFurnishments;
        }

        /// <summary>
        /// Get all Ids from Furnishments
        /// </summary>
        /// <returns>List of Integer</returns>
        public static List<int> GetAllIDs()
        {
            log.Info("GetAllIDs()");

            List<int> furnishmentIDs = new List<int>();

            try
            {
                List<furnishment> allFurn = new List<furnishment>();

                using (var context = new innovations4austriaEntities())
                {
                    allFurn = GetAllFurnishments();

                    foreach (var item in allFurn)
                    {
                        furnishmentIDs.Add(item.id);
                    }
                }

                return furnishmentIDs;
            }
            catch (Exception ex)
            {
                log.Error("Error getting all IDs", ex);
            }

            return furnishmentIDs;
        }

        /// <summary>
        /// Get all furnishments from database by a list of Ids
        /// and by a specific room Id
        /// </summary>
        /// <param name="ids">List of Ids (List<Int>)</param>
        /// <param name="roomId">Room Id (Int)</param>
        /// <returns>List of Furnishments</returns>
        public static List<furnishment> GetFurnishmentsByIdsAndRoomId(List<int> ids, int roomId)
        {
            log.Info("FurnishmentAdministration - GetFurnishmentsByIds(List<int> ids)");

            List<furnishment> fList = new List<furnishment>();

            List<furnishment> temp = new List<furnishment>();

            try
            {
                using (var context = new innovations4austriaEntities())
                {
                    foreach (var item in context.furnishments)
                    {
                        temp.Add(new furnishment() { id = item.id, description = item.description, images = item.images, roomfurnishments = item.roomfurnishments });
                    }

                    foreach (var item in ids)
                    {
                        fList.Add(temp.Where(x => x.id == item && x.roomfurnishments.Any(y => y.room_id == roomId)).FirstOrDefault()); 
                    }

                    fList.Distinct();
                }
            }
            catch (Exception ex)
            {
                log.Error("Error getting furnishments by ids", ex);
            }

            return fList;
        }

        /// <summary>
        /// Get all furnishments from database by room Id
        /// </summary>
        /// <param name="roomId">room id</param>
        /// <returns>List of Furnishments</returns>
        public static List<furnishment> GetFurnishmentsByRoomId(int roomId)
        {
            log.Info("FurnishmentAdministration - GetFurnishmentsByRoomId(int roomId)");

            List<furnishment> fList = new List<furnishment>();

            try
            {
                using (var context = new innovations4austriaEntities())
                {
                    fList = context.furnishments.Where(x => x.roomfurnishments.Any(y => y.room_id == roomId)).ToList();
                }
            }
            catch (Exception ex)
            {
                log.Error("Error getting furnishments by room id", ex);
            }

            return fList;
        }
    }
}
