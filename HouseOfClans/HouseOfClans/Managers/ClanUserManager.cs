using HouseOfClans.Enums;
using HouseOfClans.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HouseOfClans.Managers
{
    public class ClanUserManager
    {
        /// <summary>
        /// Gets the clan user information based on the aspnet user id
        /// </summary>
        /// <param name="aspnetUserId">AspNetUser Id</param>
        public static ClanUser SelectByAspNetUserId(string aspnetUserId)
        {
            ClanUser clanUser = new ClanUser();
            using (var dbContext = new HouseOfClansEntities())
            {
                clanUser = dbContext.ClanUsers.Where(p => p.aspnetUserId == aspnetUserId).Select(user => user).FirstOrDefault();
            }

            return clanUser;
        }

        /// <summary>
        /// Gets the clan user information based on the clan user id
        /// </summary>
        /// <param name="clanUserId">ClanUser Id</param>
        public static ClanUser SelectByClanUserId(int userId)
        {
            ClanUser clanUser = new ClanUser();
            using (var dbContext = new HouseOfClansEntities())
            {
                clanUser = dbContext.ClanUsers.Where(p => p.id == userId).Select(user => user).FirstOrDefault();
            }

            return clanUser;
             
        }

        /// <summary>
        /// Gets the Clan Leader based on the clan id
        /// </summary>
        /// <param name="clanId">Clan Id</param>
        public static ClanUser SelectClanLeaderByClanId(int clanId)
        {
            ClanUser clanLeader = new ClanUser();
            using (var dbContext = new HouseOfClansEntities())
            {
                clanLeader = dbContext.ClanUsers.Where(p => p.clanId == clanId && (ClanRole)p.userRoleId == ClanRole.Leader).Select(user => user).FirstOrDefault();
            }

            return clanLeader;
        }

        /// <summary>
        /// Gets the Clan CoLeaders based on the clan id
        /// </summary>
        /// <param name="clanId">Clan Id</param>
        public static List<ClanUser> SelectClanCoLeadersByClanId(int clanId)
        {
            List<ClanUser> clanCoLeaders = new List<ClanUser>();
            using (var dbContext = new HouseOfClansEntities())
            {
                clanCoLeaders = dbContext.ClanUsers.Where(p => p.clanId == clanId && (ClanRole)p.userRoleId == ClanRole.CoLeader).ToList();
            }

            return clanCoLeaders;
        }

        /// <summary>
        /// Gets the Clan Elders based on the clan id
        /// </summary>
        /// <param name="clanId">Clan Id</param>
        public static List<ClanUser> SelectClanEldersByClanId(int clanId)
        {
            List<ClanUser> clanElders = new List<ClanUser>();
            using (var dbContext = new HouseOfClansEntities())
            {
                clanElders = dbContext.ClanUsers.Where(p => p.clanId == clanId && (ClanRole)p.userRoleId == ClanRole.Elder).ToList();
            }

            return clanElders;
        }

        /// <summary>
        /// Gets the Clan Regular Members based on the clan id
        /// </summary>
        /// <param name="clanId">Clan Id</param>
        public static List<ClanUser> SelectClanRegularMembersByClanId(int clanId)
        {
            List<ClanUser> clanMembers = new List<ClanUser>();
            using (var dbContext = new HouseOfClansEntities())
            {
                clanMembers = dbContext.ClanUsers.Where(p => p.clanId == clanId && (ClanRole)p.userRoleId == ClanRole.Member).ToList();
            }

            return clanMembers;
        }

        /// <summary>
        /// Gets the all group members(except Leader) based on the group id
        /// </summary>
        /// <param name="clanId">Clan Id</param>
        public static List<ClanUser> SelectGroupMembersByGroupId(int groupId)
        {
            List<ClanUser> groupMembers = new List<ClanUser>();
            using (var dbContext = new HouseOfClansEntities())
            {
                groupMembers = dbContext.ClanUsers.Where(p => p.clanGroupId == groupId).ToList();
            }

            ClanGroup group = ClanGroupManager.SelectByClanGroupId(groupId);

            groupMembers.RemoveAll(member => member.id == group.groupLeaderId);

            return groupMembers;
        }

        /// <summary>
        /// Gets the all the freeagents by clan id
        /// </summary>
        /// <param name="clanId">Clan Id</param>
        public static List<ClanUser> SelectFreeAgentsByClanId(int clanId)
        {
            List<ClanUser> freeAgents = new List<ClanUser>();
            using (var dbContext = new HouseOfClansEntities())
            {
                freeAgents = dbContext.ClanUsers.Where(p => p.clanId == clanId && p.clanGroupId == null && p.userRoleId != (short)ClanRole.Leader).ToList();
            }

            return freeAgents;
        }

		/// <summary>
		/// Gets the all members based on the clan id
		/// </summary>
		/// <param name="clanId">Clan Id</param>
		public static List<ClanUser> SelectAllByClanId(int clanId)
		{
			List<ClanUser> clanMembers = new List<ClanUser>();

			using (var dbContext = new HouseOfClansEntities())
			{
				clanMembers = dbContext.ClanUsers.Where(p => p.clanId == clanId).Select(user => user).ToList();
			}

			return clanMembers;
		}

		/// <summary>
		/// Get all users of the website. Rarely used. Right now only used on the 'hidden' logs page. Might be deprecated soon or moved to its ActionLog Manager/ModelView. //vv
		/// </summary>
		/// <returns>A list of all ClanUsers of the website</returns>
		public static List<ClanUser> SelectAll()
		{
			List<ClanUser> clanMembers = new List<ClanUser>();

			using (var dbContext = new HouseOfClansEntities())
			{
				clanMembers = dbContext.ClanUsers.ToList();
			}

			return clanMembers;
		}

        /// <summary>
        /// Inserts a new Clan User record
        /// </summary>
        public static void Insert(ClanUser clanUser)
        {
            using (var dbContext = new HouseOfClansEntities()) 
            {
                clanUser.addedOn = DateTime.Now;
                dbContext.ClanUsers.Add(clanUser);
                dbContext.SaveChanges();
            }
        }

        /// <summary>
        /// Updates the current Clan User
        /// </summary>
        public static void Update(ClanUser updatedClanUser)
        {
            ClanUser clanUser = ClanUserManager.SelectByClanUserId(updatedClanUser.id);
            using (var dbContext = new HouseOfClansEntities())
            {
                
                updatedClanUser.updatedOn = DateTime.Now;
                dbContext.ClanUsers.Attach(clanUser);
                dbContext.Entry(clanUser).CurrentValues.SetValues(updatedClanUser);
                dbContext.SaveChanges();
            }
        }

        /// <summary>
        /// Deletes the current Clan User
        /// </summary>
        public static bool Delete(int clanUserId)
        {
            bool isDeleted = false;
            ClanUser deleteCLanUser = ClanUserManager.SelectByClanUserId(clanUserId);

            using (var dbContext = new HouseOfClansEntities())
            {
                dbContext.ClanUsers.Attach(deleteCLanUser);
                dbContext.ClanUsers.Remove(deleteCLanUser);
                isDeleted = dbContext.SaveChanges() > 0;
            }

            return isDeleted;
        }
    }
}