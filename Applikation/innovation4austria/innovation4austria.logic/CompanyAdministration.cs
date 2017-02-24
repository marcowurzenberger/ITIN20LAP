using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using innovation4austria.dataAccess;

namespace innovation4austria.logic
{
    public class CompanyAdministration
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Get all companies from database
        /// </summary>
        /// <returns>List of all companies</returns>
        public static List<company> GetAllCompanies()
        {
            log.Info("GetAllCompanies()");

            List<company> allCompanies = new List<company>();

            try
            {
                using (var context = new innovations4austriaEntities())
                {
                    foreach (var c in context.companies.Include("bookings").Include("portalusers"))
                    {
                        if (c.active)
                        {
                            allCompanies.Add(c);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("Error getting all companies", ex);
            }

            return allCompanies;
        }

        /// <summary>
        /// Create new company with parametres from Dashboard
        /// </summary>
        /// <param name="newName">new companyname</param>
        /// <param name="newZip">new zip of company</param>
        /// <param name="newCity">new city of company</param>
        /// <param name="newStreet">new street of company</param>
        /// <param name="newNumber">new number of company</param>
        /// <returns>true if success, false if anything went wrong</returns>
        public static bool CreateNewCompany(string newName, string newZip, string newCity, string newStreet, string newNumber)
        {
            log.Info("CreateNewCompany(string name, string zip, string city, string street, string number)");

            bool success = false;

            try
            {
                if (!string.IsNullOrEmpty(newName) && !string.IsNullOrEmpty(newZip) && !string.IsNullOrEmpty(newCity) && !string.IsNullOrEmpty(newStreet) && !string.IsNullOrEmpty(newNumber))
                {
                    using (var context = new innovations4austriaEntities())
                    {
                        context.companies.Add(new company()
                        {
                            name = newName,
                            zip = newZip,
                            city = newCity,
                            street = newStreet,
                            number = newNumber,
                            active = true
                        });

                        context.SaveChanges();
                        return success = true;
                    }
                }
                else
                {
                    log.Warn("A parameter is null or empty");
                    return success;
                }
            }
            catch (Exception ex)
            {
                log.Error("Error creating new company", ex);
            }

            return success;
        }

        /// <summary>
        /// Edit company with id
        /// </summary>
        /// <param name="id">id from company</param>
        /// <param name="name">new or old name</param>
        /// <param name="zip">new or old zip</param>
        /// <param name="city">new or old city</param>
        /// <param name="street">new or old street</param>
        /// <param name="number">new or old number</param>
        /// <returns>true if successfully edited, false if anything went wrong</returns>
        public static bool EditCompany(int id, string name, string zip, string city, string street, string number)
        {
            log.Info("EditCompany(int id, string name, string zip, string city, string street, string number)");

            bool success = false;

            try
            {
                if (id > 0 && !string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(zip) && !string.IsNullOrEmpty(city) && !string.IsNullOrEmpty(street) && !string.IsNullOrEmpty(number))
                {
                    using (var context = new innovations4austriaEntities())
                    {
                        company comp = context.companies.Where(x => x.id == id && x.active == true).FirstOrDefault();

                        if (comp != null)
                        {
                            comp.name = name;
                            comp.city = city;
                            comp.zip = zip;
                            comp.street = street;
                            comp.number = number; 
                        }

                        context.SaveChanges();
                    }
                    return success = true;
                }
                else
                {
                    return success;
                }
            }
            catch (Exception ex)
            {
                log.Error("Error editing company", ex);
            }

            return success;
        }

        /// <summary>
        /// Get company by id from database
        /// </summary>
        /// <param name="id">id of company</param>
        /// <returns>company object</returns>
        public static company GetCompanyById(int id)
        {
            log.Info("GetCompanyById(int id)");

            company comp = new company();

            try
            {
                if (id > 0)
                {
                    using (var context = new innovations4austriaEntities())
                    {
                        comp = context.companies.Where(x => x.id == id && x.active == true).FirstOrDefault();
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("Error getting company by id", ex);
            }

            return comp;
        }

        /// <summary>
        /// Delete company from database by id
        /// </summary>
        /// <param name="id">id of company</param>
        /// <returns>true if successfully, false if anything went wrong</returns>
        public static bool DeleteCompanyById(int id)
        {
            log.Info("DeleteCompanyById(int id)");

            bool success = false;

            try
            {
                if (id > 0)
                {
                    using (var context = new innovations4austriaEntities())
                    {
                        foreach (var item in context.companies)
                        {
                            if (item.id == id)
                            {
                                item.active = false;
                            }
                        }

                        context.SaveChanges();
                        return success = true;
                    }
                }
                else
                {
                    log.Warn("id is 0 or empty");
                }
            }
            catch (Exception ex)
            {
                log.Error("Error deleting company", ex);
            }

            return success;
        }

        /// <summary>
        /// Get Company Id by company name
        /// </summary>
        /// <param name="compName">name of company</param>
        /// <returns>id of company</returns>
        public static int GetCompanyIdByName(string compName)
        {
            log.Info("CompanyAdministration - GetCompanyIdByName(string compName)");

            int companyId = 0;

            try
            {
                using (var context = new innovations4austriaEntities())
                {
                    companyId = context.companies.Where(x => x.name == compName).Select(x => x.id).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                log.Error("Error getting company id by useremail", ex);
            }

            return companyId;
        }
    }
}
