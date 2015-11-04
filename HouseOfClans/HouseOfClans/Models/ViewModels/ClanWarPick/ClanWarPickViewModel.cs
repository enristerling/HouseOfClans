using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HouseOfClans.Managers;
using HouseOfClans.Models;

namespace HouseOfClans.Models.ViewModels.ClanWarPick
{
	public class ClanWarPickViewModel
	{
		public int?		Id					{ get; set; }
		public int		ClanWarId			{ get; set; }
		public short	ClanMemberWarPosition { get; set; }
		public int		ClanUserId			{ get; set; }
		private string	_clanUserName		{ get; set; }
		public short?	PickVS				{ get; set; }
		public string	Note				{ get; set; }
		public DateTime AddedOn				{ get; set; }
		public DateTime? UpdatedOn			{ get; set; }
		private byte	_basesCount			{ get; set; }

		public ClanWarPickViewModel()
		{
		}

		public static List<ClanWarPickViewModel> GetAllByWar(int warId)
		{
			List<ClanWarPickViewModel> clanWarPickList = new List<ClanWarPickViewModel>();
			int warTypeId = ClanWarManager.SelectByClanWarId(warId).warTypeId;
			byte count = WarTypeManager.Get(warTypeId).value;

			foreach (Models.ClanWarPick warPick in ClanWarPicksManager.SelectAllByWarId(warId))
			{
				clanWarPickList.Add(new ClanWarPickViewModel()
				{
					Id = warPick.id,
					ClanWarId = warPick.clanWarId,
					ClanMemberWarPosition = warPick.clanMemberWarPosition,
					ClanUserId = warPick.clanUserId,
					PickVS = warPick.pickVS,
					Note = warPick.note,
					UpdatedOn = warPick.updatedOn,
					AddedOn = warPick.addedOn,
					_clanUserName = warPick.clanUserId > 0 ? ClanUserManager.SelectByClanUserId(warPick.clanUserId).name : string.Empty,
					_basesCount = count
				});
			}

			return clanWarPickList;
		}

		#region Properties

		public string ClanUserName
		{
			get
			{
				return _clanUserName;
			}

			private set
			{
				_clanUserName = value;
			}
		}

		public byte BasesCount
		{
			get
			{
				return _basesCount;
			}
		}

		#endregion
	}
}