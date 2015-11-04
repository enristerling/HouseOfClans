using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HouseOfClans.Managers;
using HouseOfClans.Models;

namespace HouseOfClans.Models.ViewModels.ClanWar
{
	public class ClanWarViewModel : ClanBaseViewModel
	{
        public List<Models.ClanWar> ClanWarList	{ get; set; }
		private Models.ClanWar		_clanWar		{ get; set; }

		public ClanWarViewModel()
		{
		}

		public ClanWarViewModel(int clanId) : this(null, clanId)
		{
		}

		public ClanWarViewModel(int? id, int clanId) : base(clanId)
		{
			if (id != null)
			{
				_clanWar = ClanWarManager.SelectByClanWarId((int)id);
			}

			ClanWarList = ClanWarManager.SelectClanWarsByClanId(clanId);
		}

		public string GetWarType(int typeId)
		{
			return WarTypeManager.Get(typeId).name;
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