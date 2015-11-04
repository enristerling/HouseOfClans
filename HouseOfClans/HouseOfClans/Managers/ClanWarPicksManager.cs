using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using HouseOfClans.Models;
using HouseOfClans.Models.ViewModels.ClanWarPick;

namespace HouseOfClans.Managers
{
	public class ClanWarPicksManager
	{
		private const string _clanWarPickSortedName = "clanWarPick_";

		/// <summary>
		/// Gets a list of War Picks based on the war id
		/// </summary>
		/// <param name="warId">War Id</param>
		public static List<ClanWarPick> SelectAllByWarId(int warId)
		{
			List<ClanWarPick> warPicks = new List<ClanWarPick>();

			using (var dbContext = new HouseOfClansEntities())
			{
				warPicks = dbContext.ClanWarPicks.Where(p => p.clanWarId == warId).OrderBy(p => p.clanMemberWarPosition).ToList();
			}

			return warPicks;
		}

		/// <summary>
		/// Inserts or Updates the Clan War Picks depending on the parameters received.
		/// </summary>
		/// <param name="clanWarPickViewModel">A ClanWarPickViewModel object, if its Id is NULL then do an Insert, else Updates.</param>
		public static void Upsert(List<ClanWarPickViewModel> clanWarPicksList)
		{
			using (var dbContext = new HouseOfClansEntities())
			{
				foreach (ClanWarPickViewModel pick in clanWarPicksList)
				{
					ClanWarPick clanWarPick = ClanWarPicksManager.ConvertViewToModel(pick);

					if (pick.Id == null)
					{
						clanWarPick.addedOn = DateTime.Now;
						dbContext.ClanWarPicks.Add(clanWarPick);
					}
					else
					{
						clanWarPick.updatedOn = DateTime.Now;
						dbContext.Entry(clanWarPick).State = EntityState.Modified;
						dbContext.Entry(clanWarPick).Property(p => p.clanMemberWarPosition).IsModified = false;
					}

					dbContext.SaveChanges();
				}
			}
		}

		public static void UpdateMemberWarPosition(string[] sortedPicks)
		{
			string idParsed = string.Empty;

			using (var dbContext = new HouseOfClansEntities())
			{
				for (short i = 0; i < sortedPicks.Length; i++)
				{
					idParsed = sortedPicks[i].Replace(_clanWarPickSortedName, "");

					ClanWarPick pick = new ClanWarPick()
					{
						id = int.Parse(idParsed),
						clanMemberWarPosition = (short)(i + 1),
						updatedOn = DateTime.Now
					};

					dbContext.ClanWarPicks.Attach(pick);
					dbContext.Entry(pick).Property(p => p.clanMemberWarPosition).IsModified = true;
					dbContext.Entry(pick).Property(p => p.updatedOn).IsModified = true;
					dbContext.SaveChanges();
				}
			}
		}

		/// <summary>
		/// Deletes the Picks of the current Clan War
		/// </summary>
		public static bool Delete(int warId)
		{
			bool isDeleted = false;

			using (var dbContext = new HouseOfClansEntities())
			{
				dbContext.ClanWarPicks.RemoveRange(dbContext.ClanWarPicks.Where(p => p.clanWarId == warId));
				dbContext.SaveChanges();
			}

			isDeleted = !ClanWarPicksManager.SelectAllByWarId(warId).Any();

			return isDeleted;
		}

		private static ClanWarPick ConvertViewToModel(ClanWarPickViewModel viewModel)
		{
			ClanWarPick model = new ClanWarPick();

			model.id = viewModel.Id ?? 0;
			model.clanWarId = viewModel.ClanWarId;
			model.clanMemberWarPosition = viewModel.ClanMemberWarPosition;
			model.clanUserId = viewModel.ClanUserId;
			model.pickVS = viewModel.PickVS;
			model.note = viewModel.Note;
			model.addedOn = viewModel.AddedOn;
			model.updatedOn = viewModel.UpdatedOn;

			return model;
		}
	}
}