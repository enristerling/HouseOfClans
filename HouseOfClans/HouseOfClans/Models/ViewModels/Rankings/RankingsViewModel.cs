using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HouseOfClans.Managers;
using HouseOfClans.Models;

namespace HouseOfClans.Models.ViewModels.Rankings
{
	public class RankingsViewModel : ClanBaseViewModel
	{
		public List<Models.ClanUser> MemberList { get; set; }
		private Models.ClanWar _clanWar { get; set; }

		public RankingsViewModel()
		{
		}

		#region Properties

		public Models.ClanWar War
		{
			get
			{
				return _clanWar;
			}
		}

		#endregion
	}
}