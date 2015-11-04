using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using HouseOfClans.Models;

namespace HouseOfClans.Managers
{
	public class ClanManager
	{
		/// <summary>
		/// Gets the clan information based on the clan id
		/// </summary>
		/// <param name="clanId">Clan Id</param>
        public static Clan SelectByClanId(int? clanId)
        {
            Clan clanInfo = new Clan();

            using (var dbContext = new HouseOfClansEntities())
            {
                clanInfo = dbContext.Clans.Where(p => p.id == clanId).Select(clan => clan).FirstOrDefault();
            }

            return clanInfo; 
        }

        /// <summary>
        /// Gets clan information based on the clan tag
        /// </summary>
        /// <param name="clanId">Clan Tag</param>
        public static Clan SelectByClanTagId(string clanTag)
        {
            Clan clanInfo = new Clan();

            using (var dbContext = new HouseOfClansEntities())
            {
                clanInfo = dbContext.Clans.Where(p => p.tag == clanTag).Select(clan => clan).FirstOrDefault();
            }

            return clanInfo;
        }

        ///<summar>
        /// Gets the clan id based on a AspNet User Id
        /// </summar>
        public static int? GetClanId(string aspnetId)
        {
            ClanUser clanUser;
            using (var dbContext = new HouseOfClansEntities())
            {
                clanUser = dbContext.ClanUsers.Where(p => p.aspnetUserId == aspnetId).Select(clan => clan).FirstOrDefault();
            }
            if (clanUser == null)
            {
                return null;
            }
            else
            {
                return clanUser.clanId;
            }
        }

		/// <summary>
		/// Inserts a new Clan record
		/// </summary>
        public static void Insert(Clan clan)
        {
            using (var dbContext = new HouseOfClansEntities())
            {
                clan.addedOn = DateTime.Now;
                dbContext.Clans.Add(clan);
                dbContext.SaveChanges();
            }
        }
		
		/// <summary>
		/// Updates the current Clan
		/// </summary>
        public static void Update(Clan clan)
        {
            using (var dbContext = new HouseOfClansEntities())
            {
                Clan clanInfo = ClanManager.SelectByClanId(clan.id);
                clanInfo.updatedOn = DateTime.Now;

                dbContext.Clans.Attach(clanInfo);
                dbContext.Entry(clanInfo).CurrentValues.SetValues(clan);
                dbContext.SaveChanges();
            }
        }

		/// <summary>
		/// Deletes the current Clan
		/// </summary>
        public static bool Delete(int clanId)
        {
            bool isDeleted = false;
            Clan deleteClan = ClanManager.SelectByClanId(clanId);

            using (var dbContext = new HouseOfClansEntities())
            {
                dbContext.Clans.Attach(deleteClan);
                dbContext.Clans.Remove(deleteClan);
                isDeleted = dbContext.SaveChanges() > 0;
            }

            return isDeleted;
        }
	}
}