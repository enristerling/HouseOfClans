using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HouseOfClans.Managers;
using HouseOfClans.Models;
using HouseOfClans.Models.ViewModels.ClanWarPick;

namespace HouseOfClans.Models.ViewModels.ClanWarPick
{
	public class ClanWarPickUpsertViewModel
	{
		public int			id					{ get; set; }
		public int			clanWarId			{ get; set; }
		public short		clanMemberPosition	{ get; set; }
		public short?		pickVS				{ get; set; }
		public string		note				{ get; set; }
		public DateTime		addedOn				{ get; set; }
		public DateTime?	updatedOn			{ get; set; }

		public ClanWarPickUpsertViewModel()
		{
		}
	}
}