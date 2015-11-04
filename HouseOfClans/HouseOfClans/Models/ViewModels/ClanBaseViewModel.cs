using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using HouseOfClans.Models;
using HouseOfClans.Managers;

namespace HouseOfClans.Models.ViewModels
{
	public class ClanBaseViewModel
	{
        protected Clan ClanInfo { get; set; } 

		public ClanBaseViewModel()
		{
		}

		public ClanBaseViewModel(int id)
		{
            ClanInfo = ClanManager.SelectByClanId(id);
		}

		#region Properties

		[Display(Name = "Clan Id")]
		public int ClanId
		{
			get
			{
				return ClanInfo.id;
			}
		}

		[Display(Name="Clan Name")]
		public string ClanName
		{
			get
			{
				return ClanInfo.name;
			}
		}

		[Display(Name = "Shield Logo")]
		public string ShieldLogoLocation
		{
			get
			{
				return ClanInfo.shieldLogoLocation;
			}
		}

		#endregion
	}
}