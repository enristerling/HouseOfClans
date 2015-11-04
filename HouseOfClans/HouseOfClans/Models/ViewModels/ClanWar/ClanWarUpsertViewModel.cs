using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HouseOfClans.Managers;
using HouseOfClans.Models;
using HouseOfClans.Models.ViewModels.ClanWar;

namespace HouseOfClans.Models.ViewModels.ClanWar
{
	public class ClanWarUpsertViewModel
	{
		public int?		Id			{ get; set; }
		public short	WarTypeId	{ get; set; }
		public int		ClanId		{ get; set; }
		public string	ClanVS		{ get; set; }
		public string	Note		{ get; set; }
		public DateTime WarStartedOn { get; set; }
		public DateTime AddedOn		{ get; set; }
		public DateTime? UpdatedOn	{ get; set; }
		public string	ClanVSTag	{ get; set; }
		public string	WarNote		{ get; set; }
		public bool		IsFinalized { get; set; }

		public ClanWarUpsertViewModel()
		{ }

		public static ClanWarUpsertViewModel Get(int warId)
		{
			Models.ClanWar clanWarInfo = ClanWarManager.SelectByClanWarId(warId);
			ClanWarUpsertViewModel updateWar = new ClanWarUpsertViewModel();
			updateWar.FillModelView(clanWarInfo);

			return updateWar;
		}

		private void FillModelView(Models.ClanWar model)
		{
			Id				= model.id;
			WarTypeId		= model.warTypeId;
			ClanId			= model.clanId;
			ClanVS			= model.clanVS;
			ClanVSTag		= model.clanVSTag;
			Note			= model.note;
			WarStartedOn	= model.warStartedOn;
			WarNote			= model.warNote;
			IsFinalized		= model.isFinalized;
			AddedOn			= model.addedOn;
			UpdatedOn		= model.updatedOn;
		}
	}
}