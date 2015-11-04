using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HouseOfClans.Models.ViewModels.Rankings
{
	public class ClanMemberStatsViewModel
	{
		public int			Id				{ get; set; }
		public int			ClanWarId		{ get; set; }
		public int?			ClanGroupId		{ get; set; }
		public bool			HasHeroicDefense { get; set; }
		public bool			HasHeroicAttack	{ get; set; }
		public short		AttacksDefended { get; set; }
		public short		AttacksOn		{ get; set; }
		public string		Note			{ get; set; }
		public DateTime?		AttackOn		{ get; set; }
		public DateTime		AddedOn			{ get; set; }
		public DateTime?	UpdatedOn		{ get; set; }
		public int			ClanUserId		{ get; set; }
		public short		Stars			{ get; set; }
		public int			ClanId			{ get; set; }
		public short		Xp				{ get; set; }
		public Boolean		OptOut			{ get; set; }
		private Boolean		AttackWin		{ get; set; }
		private short		_totalHeroicDefense { get; set; }
		private short		_totalHeroicAttack	{ get; set; }
		private short		_warsCount		{ get; set; }
		private short		_totalAttacks	{ get; set; }
		private short		_attacksWon		{ get; set; }

		public ClanMemberStatsViewModel()
		{
		}

		public static ClanMemberStatsViewModel ConvertToClanMemberStats(WarRanking ranking)
		{
			ClanMemberStatsViewModel stats = new ClanMemberStatsViewModel();
			stats.Id				= ranking.id;
			stats.ClanWarId			= ranking.clanWarId;
			stats.ClanGroupId		= ranking.clanGroupId;
			stats.HasHeroicDefense	= ranking.heroicDefense;
			stats.HasHeroicAttack	= ranking.heroicAttack;
			stats.AttacksDefended	= ranking.attacksDefended;
			stats.AttacksOn			= ranking.attacksOn;
			stats.Note				= ranking.note;
			stats.AddedOn			= ranking.addedOn;
			stats.UpdatedOn			= ranking.updatedOn;
			stats.ClanUserId		= ranking.clanUserId;
			stats.Stars				= ranking.stars;
			stats.OptOut			= ranking.optOut;
			stats.Xp				= ranking.xp;
			stats.AttackWin			= ranking.attackWin;
			stats.AttackOn			= ranking.attackOn;

			stats.TotalHeroicDefense	= Convert.ToInt16(ranking.heroicDefense);
			stats.TotalHeroicAttack		= Convert.ToInt16(ranking.heroicAttack);

			return stats;
		}

		public static WarRanking ConvertToWarRanking(ClanMemberStatsViewModel stats)
		{
			WarRanking ranking = new WarRanking();
			ranking.id				= stats.Id;
			ranking.clanWarId		= stats.ClanWarId;
			ranking.clanGroupId		= stats.ClanGroupId;
			ranking.heroicDefense	= stats.HasHeroicDefense;
			ranking.heroicAttack	= stats.HasHeroicAttack;
			ranking.attacksDefended = stats.AttacksDefended;
			ranking.attacksOn		= stats.AttacksOn;
			ranking.note			= stats.Note;
			ranking.addedOn			= stats.AddedOn;
			ranking.updatedOn		= stats.UpdatedOn;
			ranking.clanUserId		= stats.ClanUserId;
			ranking.stars			= stats.Stars;
			ranking.optOut			= stats.OptOut;
			ranking.xp				= stats.Xp;
			ranking.attackWin		= stats.AttackWin;
			ranking.attackOn		= stats.AttackOn;

			return ranking;
		}

		#region Properties

		public String MemberName
		{
			get
			{
				string name = string.Empty;

				if (ClanUserId > 0)
				{
					ClanUser clanUser = Managers.ClanUserManager.SelectByClanUserId(ClanUserId);
					name = clanUser.name;
				}

				return name;
			}
		}

		public short TotalHeroicDefense
		{
			get
			{
				return _totalHeroicDefense;
			}
			set
			{
				_totalHeroicDefense = value;
			}
		}

		public short TotalHeroicAttack
		{
			get
			{
				return _totalHeroicAttack;
			}
			set
			{
				_totalHeroicAttack = value;
			}
		}

		public short WarsCount
		{
			get
			{
				return _warsCount;
			}
			set
			{
				_warsCount = value;
			}
		}

		public short TotalAttacks
		{
			get
			{
				return _totalAttacks;
			}
			set
			{
				_totalAttacks = value;
			}
		}

		public short AttacksWon
		{
			get
			{
				return _attacksWon;
			}
			set
			{
				_attacksWon = value;
			}
		}

		#endregion
	}
}