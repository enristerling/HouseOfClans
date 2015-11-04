using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HouseOfClans.Managers;
using HouseOfClans.Models;

namespace HouseOfClans.Models.ViewModels.Rankings
{
	public class RankingsUpsertViewModel// : ClanBaseViewModel
	{
		List<WarRanking> _warRankings = new List<WarRanking>();
		List<ClanMemberStatsViewModel> _clanMemberStats = new List<ClanMemberStatsViewModel>();
		ClanMemberStatsViewModel _clanWarStats = new ClanMemberStatsViewModel();
		Dictionary<int, string> _availableAttacks = new Dictionary<int, string>();

		public RankingsUpsertViewModel()
		{
		}

		public RankingsUpsertViewModel(int warId, int clanId) : this(warId, clanId, false)
		{
		}

		public RankingsUpsertViewModel(int warId, int clanId, bool isOverall)
		{
			_clanMemberStats = new List<ClanMemberStatsViewModel>();
			List<WarRanking> warRankings = new List<WarRanking>();

			if (isOverall)
			{
				warRankings = RankingsManager.SelectRankingsByClanId(clanId);

				if (warRankings.Any() && warRankings.Count > 0)
				{
					var rankingTotals = warRankings.GroupBy(g => g.clanUserId).Select(p => new ClanMemberStatsViewModel
																				{
																					ClanUserId = p.Key,
																					ClanId = clanId,
																					TotalAttacks = (short)p.Count(),
																					AttacksWon = (short)p.Count(r => r.attackWin),
																					TotalHeroicDefense = (short)p.Sum(r => Convert.ToInt16(r.heroicDefense)),
																					TotalHeroicAttack = (short)p.Sum(r => Convert.ToInt16(r.heroicAttack)),
																					AttacksDefended = (short)p.Sum(r => r.attacksDefended),
																					AttacksOn = (short)p.Sum(r => r.attacksOn),
																					Stars = (short)p.Sum(r => r.stars),
																					Xp = (short)p.Sum(r => r.xp)
																				});

					_clanMemberStats = rankingTotals.ToList();

					foreach(ClanMemberStatsViewModel memberStats in _clanMemberStats)
					{
						short wars = 0;

						wars = (short)warRankings.Where(p => p.clanUserId == memberStats.ClanUserId).Select(p => p.clanWarId).Distinct().Count();

						memberStats.WarsCount = wars;
					}

					int attacksCount = warRankings.Count();

					_clanWarStats = _clanMemberStats.GroupBy(g => g.ClanId).Select(p => new ClanMemberStatsViewModel
					{
						ClanId = p.Key,
						ClanWarId = 0,
						WarsCount = (short)p.Count(),
						TotalAttacks = (short)attacksCount,
						AttacksWon = (short)p.Sum(r => r.AttacksWon),
						TotalHeroicDefense = (short)p.Sum(r => Convert.ToInt16(r.TotalHeroicDefense)),
						TotalHeroicAttack = (short)p.Sum(r => Convert.ToInt16(r.TotalHeroicAttack)),
						AttacksDefended = (short)p.Sum(r => r.AttacksDefended),
						AttacksOn = (short)p.Sum(r => r.AttacksOn),
						Stars = (short)p.Sum(r => r.Stars),
						Xp = (short)p.Sum(r => r.Xp)
					}).Single();
				}
			}
			else
			{ 
				warRankings = RankingsManager.SelectRankingsByClanWarId(warId);

				foreach (WarRanking ranking in warRankings)
				{
					_clanMemberStats.Add(ClanMemberStatsViewModel.ConvertToClanMemberStats(ranking));
				}

				if (warRankings.Any() && warRankings.Count > 0)
				{
					_clanWarStats = _clanMemberStats.GroupBy(g => g.ClanWarId).Select(p => new ClanMemberStatsViewModel
					{
						ClanId = clanId,
						ClanWarId = p.Key,
						WarsCount = 1,
						AttacksWon = (short)p.Sum(r => r.AttacksWon),
						TotalAttacks = (short)p.Count(),
						TotalHeroicDefense = (short)p.Sum(r => Convert.ToInt16(r.TotalHeroicDefense)),
						TotalHeroicAttack = (short)p.Sum(r => Convert.ToInt16(r.TotalHeroicAttack)),
						AttacksDefended = (short)p.Sum(r => r.AttacksDefended),
						AttacksOn = (short)p.Sum(r => r.AttacksOn),
						Stars = (short)p.Sum(r => r.Stars),
						Xp = (short)p.Sum(r => r.Xp)
					}).Single();
				}

				List<ClanUser> clanUsers = ClanUserManager.SelectAllByClanId(clanId);
				List<Models.ClanWarPick> availableAttacks = RankingsManager.SelectAttacksAvailableForWar(warId);
				_availableAttacks = availableAttacks.ToDictionary(key => key.clanUserId, value => clanUsers.Where(p => p.id == value.clanUserId).Select(p => p.name).Single());
			}
		}

		public RankingsUpsertViewModel(List<ClanMemberStatsViewModel> clanMemberStatsViewModel)
		{
			_warRankings = new List<WarRanking>();

			foreach(ClanMemberStatsViewModel stats in clanMemberStatsViewModel)
			{
				_warRankings.Add(ClanMemberStatsViewModel.ConvertToWarRanking(stats));
			}
		}

		#region Properties

		public ClanMemberStatsViewModel ClanWarRankings
		{
			get
			{
				return _clanWarStats;
			}
		}

		public List<ClanMemberStatsViewModel> MemberRankings
		{
			get
			{
				return _clanMemberStats;
			}
		}

		public List<WarRanking> WarRankings
		{
			get
			{
				return _warRankings;
			}
		}

		public Dictionary<int, string> AvailableAttacks
		{
			get
			{
				return _availableAttacks;
			}
		}

		#endregion
	}
}