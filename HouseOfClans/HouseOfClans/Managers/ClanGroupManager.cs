using HouseOfClans.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HouseOfClans.Managers
{
    public class ClanGroupManager
    {
        /// <summary>
        /// Gets a list of ClanGroups based on the clan id
        /// </summary>
        /// <param name="clanId">Clan Id</param>
        public static List<ClanGroup> SelectAllByClanId(int? clanId)
        {
            List<ClanGroup> clanGroups = new List<ClanGroup>();

            using (var dbContext = new HouseOfClansEntities())
            {
                clanGroups = dbContext.ClanGroups.Where(p => p.clanId == clanId).ToList();
            }

            return clanGroups;
        }

        /// <summary>
        /// Gets a ClanGroup based on the ClanGroup id
        /// </summary>
        /// <param name="clanId">Clan Id</param>
        public static ClanGroup SelectByClanGroupId(int clanGroupId)
        {
            ClanGroup clanGroup = new ClanGroup();

            using (var dbContext = new HouseOfClansEntities())
            {
                clanGroup = dbContext.ClanGroups.Where(p => p.id == clanGroupId).FirstOrDefault();
            }

            return clanGroup;
        }

        /// <summary>
        /// Inserts a new ClanGroup
        /// </summary>
        public static void Insert(ClanGroup clanGroup)
        {
            using (var dbContext = new HouseOfClansEntities())
            {
                clanGroup.addedOn = DateTime.Now;
                dbContext.ClanGroups.Add(clanGroup);
                dbContext.SaveChanges();
            }
        }

        /// <summary>
        /// Updates a ClanGroup
        /// </summary>
        public static void Update(ClanGroup clanGroup)
        {
            using (var dbContext = new HouseOfClansEntities())
            {
                ClanGroup clanGroupInfo = ClanGroupManager.SelectByClanGroupId(clanGroup.id);
                clanGroupInfo.updatedOn = DateTime.Now;

                dbContext.ClanGroups.Attach(clanGroupInfo);
                dbContext.Entry(clanGroupInfo).CurrentValues.SetValues(clanGroup);
                dbContext.SaveChanges();
            }
        }

        /// <summary>
        /// Deletes a ClanGroup
        /// </summary>
        public static bool Delete(int clanGroupId)
        {
            bool isDeleted = false;
            ClanGroup deleteClanGroup = ClanGroupManager.SelectByClanGroupId(clanGroupId);

            using (var dbContext = new HouseOfClansEntities())
            {
                dbContext.ClanGroups.Attach(deleteClanGroup);
                dbContext.ClanGroups.Remove(deleteClanGroup);
                isDeleted = dbContext.SaveChanges() > 0;
            }

            return isDeleted;
        }
    }
}
