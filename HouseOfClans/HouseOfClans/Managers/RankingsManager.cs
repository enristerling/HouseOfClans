using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using HouseOfClans.Models;
using HouseOfClans.Models.ViewModels.Rankings;

namespace HouseOfClans.Managers
{
	public class RankingsManager
	{
		public static List<WarRanking> SelectRankingsByClanWarId(int id)
		{
			return SelectRankingsByClanWarId(id, false);
		}

		public static List<WarRanking> SelectRankingsByClanWarId(int id, bool sortedByPick)
		{
			List<WarRanking> rankings = new List<WarRanking>();

			using (var dbContext = new HouseOfClansEntities())
			{
				rankings = dbContext.WarRankings.Where(p => p.clanWarId == id && !p.optOut).ToList();

				if (sortedByPick)
				{
					List<ClanWarPick> warMapMembers = ClanWarPicksManager.SelectAllByWarId(id).ToList();
					List<WarRanking> sortedRankings = new List<WarRanking>();

					warMapMembers.ForEach(delegate(ClanWarPick sortedPick)
					{
						WarRanking sortedRanking = rankings.Where(p => p.clanUserId == sortedPick.clanUserId).FirstOrDefault();

						if (sortedRanking != null && sortedRanking.clanWarId == id)
						{
							sortedRankings.Add(sortedRanking);
						}
					});

					rankings = new List<WarRanking>();
					rankings = sortedRankings;
				}
			}

			return rankings;
		}

		public static List<WarRanking> SelectRankingsByUserId(int userId)
		{
			List<WarRanking> rankings = new List<WarRanking>();

			using (var dbContext = new HouseOfClansEntities())
			{
				rankings = dbContext.WarRankings.Where(p => p.clanUserId == userId).Select(ranking => ranking).OrderByDescending(o => o.addedOn).ToList();
			}

			return rankings;
		}

		public static List<WarRanking> SelectRankingsByClanId(int clanId)
		{
			List<WarRanking> allClanMembersRankings = new List<WarRanking>();
			List<int> clanMembers = ClanUserManager.SelectAllByClanId(clanId).Where(p => (Enums.ClanRole)p.userRoleId != Enums.ClanRole.Unknown).OrderBy(o => o.userRoleId).Select(p => p.id).ToList();

			using (var dbContext = new HouseOfClansEntities())
			{
				allClanMembersRankings = dbContext.WarRankings.Where(p => clanMembers.Contains(p.clanUserId)).Select(ranking => ranking).ToList();
			}

			return allClanMembersRankings;
		}

		public static void Upsert(List<WarRanking> rankings)
		{
			using (var dbContext = new HouseOfClansEntities())
			{
				//vv The next line helps debugging. It will write to the Output window the sql statements
				//vv dbContext.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
				foreach (WarRanking ranking in rankings)
				{
					if (ranking.id == 0)
					{
						ranking.addedOn = DateTime.Now;
						dbContext.WarRankings.Add(ranking);
					}
					else
					{
						ranking.updatedOn = DateTime.Now;
						dbContext.Entry(ranking).State = EntityState.Modified;
					}
					
					dbContext.SaveChanges();
				}
			}
		}

		public static List<ClanWarPick> SelectAttacksAvailableForWar(int id)
		{
			List<ClanWarPick> memberList = ClanWarPicksManager.SelectAllByWarId(id);
			List<WarRanking> allMemberAttacks = RankingsManager.SelectRankingsByClanWarId(id);

			if (allMemberAttacks.Any())
			{
				List<int> membersWithBothAttacks = allMemberAttacks.GroupBy(g => g.clanUserId).Where(p => p.Count() == 2).Select(p => p.Key).ToList();
				memberList = memberList.Where(p => !membersWithBothAttacks.Contains(p.clanUserId)).ToList();
			}

			return memberList;
		}

		/// <summary>
		/// Deletes the Picks of the current Clan War
		/// </summary>
		public static bool Delete(int warId)
		{
			bool isDeleted = false;

			using (var dbContext = new HouseOfClansEntities())
			{
				dbContext.WarRankings.RemoveRange(dbContext.WarRankings.Where(p => p.clanWarId == warId));
				dbContext.SaveChanges();
			}

			isDeleted = !RankingsManager.SelectRankingsByClanWarId(warId).Any();

			return isDeleted;
		}
	}
}