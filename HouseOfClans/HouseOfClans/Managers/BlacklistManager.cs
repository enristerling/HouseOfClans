using HouseOfClans.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HouseOfClans.Managers
{
    public class BlacklistManager
    {
        /// <summary>
        /// Gets a UserBlacklist based on the blacklist id
        /// </summary>
        /// <param name="userBlacklistId">UserBlacklist Id</param>
        public static UserBlackList SelectByBlacklistId(int blacklistId)
        {
            UserBlackList blacklist = new UserBlackList();

            using (var dbContext = new HouseOfClansEntities())
            {
                blacklist = dbContext.UserBlackLists.Where(p => p.id == blacklistId).FirstOrDefault();
            }

            return blacklist;
        }
        
        /// <summary>
        /// Gets the list of UserBlackList based on the clan id
        /// </summary>
        /// <param name="clanId">Clan Id</param>
        public static List<UserBlackList> SelectBlackListsByClanId(int clanId)
        {
            List<UserBlackList> userBlacklists = new List<UserBlackList>();

            using (var dbContext = new HouseOfClansEntities())
            {
                userBlacklists = dbContext.UserBlackLists.Where(p => p.clanId == clanId).ToList();
            }

            return userBlacklists;
        }

        /// <summary>
        /// Inserts a new UserBlackList record
        /// </summary>
        public static void Insert(UserBlackList bUser)
        {
            using (var dbContext = new HouseOfClansEntities())
            {
                bUser.addedOn = DateTime.Now;
                dbContext.UserBlackLists.Add(bUser);
                dbContext.SaveChanges();
            }
        }

        /// <summary>
        /// Updates a UserBlackList
        /// </summary>
        public static void Update(UserBlackList updatedBUser)
        {
            UserBlackList bUser = BlacklistManager.SelectByBlacklistId(updatedBUser.id);
            using (var dbContext = new HouseOfClansEntities())
            {
                //TODO: Add updatedOn for the UserBlackList entity in the database
                //updatedBUser.updatedOn = DateTime.Now;
                dbContext.UserBlackLists.Attach(bUser);
                dbContext.Entry(bUser).CurrentValues.SetValues(updatedBUser);
                dbContext.SaveChanges();
            }
        }

        /// <summary>
        /// Deletes a UserBlackList
        /// </summary>
        public static bool Delete(int blacklistId)
        {
            bool isDeleted = false;
            UserBlackList deleteBlacklist = BlacklistManager.SelectByBlacklistId(blacklistId);

            using (var dbContext = new HouseOfClansEntities())
            {
                dbContext.UserBlackLists.Attach(deleteBlacklist);
                dbContext.UserBlackLists.Remove(deleteBlacklist);
                isDeleted = dbContext.SaveChanges() > 0;
            }

            return isDeleted;
        }
    }
}