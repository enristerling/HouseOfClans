using HouseOfClans.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HouseOfClans.Managers
{
    public class UserRoleManager
    {
        /// <summary>
        /// Gets the list of all available User Roles
        /// </summary>
        /// <returns>A UserRole object list</returns>
        public static List<UserRole> GetAll()
        {
            List<UserRole> userRoles = new List<UserRole>();

            using (var dbContext = new HouseOfClansEntities())
            {
                userRoles = dbContext.UserRoles.Select(userRole => userRole).ToList();
            }

            return userRoles;
        }

        /// <summary>
        /// Gets the User Role by id
        /// </summary>
        /// <returns>A UserRole object</returns>
        public static UserRole Get(int id)
        {
            UserRole userRole = new UserRole();

            using (var dbContext = new HouseOfClansEntities())
            {
                userRole = dbContext.UserRoles.Where(p => p.id == id).FirstOrDefault();
            }

            return userRole;
        }
    }
}