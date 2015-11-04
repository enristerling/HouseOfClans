using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Data.Entity;
using HouseOfClans.Models;
using HouseOfClans.Models.ViewModels.ClanWar;

namespace HouseOfClans.Managers
{
	public class ClanWarManager
	{
		/// <summary>
		/// Gets the clan war information based on the clan war id
		/// </summary>
		/// <param name="clanWarId">Clan War Id</param>
		public static ClanWar SelectByClanWarId(int clanWarId)
		{
			ClanWar clanWarInfo = new ClanWar();

			using (var dbContext = new HouseOfClansEntities())
			{
				clanWarInfo = dbContext.ClanWars.Where(p => p.id == clanWarId).Select(clanWar => clanWar).FirstOrDefault();
			}

            return clanWarInfo;
		}

        /// <summary>
        /// Gets the list of clan wars based on the clan id
        /// </summary>
        /// <param name="clanId">Clan Id</param>
		public static List<ClanWar> SelectClanWarsByClanId(int clanId)
        {
            List<ClanWar> clanWars = new List<ClanWar>();

            using (var dbContext = new HouseOfClansEntities())
            {
                clanWars = dbContext.ClanWars.Where(p => p.clanId == clanId).Select(wars => wars).ToList();
            }

            return clanWars;
        }

		/// <summary>
		/// Inserts or Updates a Clan War depending on the parameters received.
		/// </summary>
		/// <param name="clanWarViewModel">A ClanWarViewModel object, if its Id is NULL then do an Insert, else Updates.</param>
		public static int Upsert(ClanWarUpsertViewModel clanWarViewModel, int userId)
		{
			Enums.Action action = Enums.Action.Create;
			ClanWar clanWar = ClanWarManager.ConvertViewToModel(clanWarViewModel);

			using (var dbContext = new HouseOfClansEntities())
			{
				if (clanWarViewModel.Id == null)
				{
					clanWar.addedOn = DateTime.Now;
					dbContext.ClanWars.Add(clanWar);

					if (dbContext.SaveChanges() > 0)
					{
						ActionLogManager.Log<ClanWarUpsertViewModel>(action, userId, null, null, string.Format("Clan War Id: {0}", clanWar.id));
					}
				}
				else
				{
					ClanWar original = ClanWarManager.SelectByClanWarId(clanWar.id);
					action = Enums.Action.Update;
					clanWar.updatedOn = DateTime.Now;
					dbContext.Entry(clanWar).State = EntityState.Modified;

					if (dbContext.SaveChanges() > 0)
					{	
						List<ActionLog> changes = GetChanges(clanWar, original);
						ActionLogManager.Log<ClanWarUpsertViewModel>(action, userId, changes);
					}
				}
			}

			return clanWar.id;
		}

		/// <summary>
		/// Deletes the current Clan War
		/// </summary>
		public static bool Delete(int warId)
		{
			bool isDeleted = false;
            ClanWar deleteWar = ClanWarManager.SelectByClanWarId(warId);

            using (var dbContext = new HouseOfClansEntities())
            {
                dbContext.ClanWars.Attach(deleteWar);
                dbContext.ClanWars.Remove(deleteWar);
                isDeleted = dbContext.SaveChanges() > 0;
            }

			return isDeleted;
		}

		private static ClanWar ConvertViewToModel(ClanWarUpsertViewModel viewModel)
		{
			ClanWar model = new ClanWar();

			model.id			= viewModel.Id ?? 0;
			model.warTypeId		= viewModel.WarTypeId;
			model.clanId		= viewModel.ClanId;
			model.clanVS		= viewModel.ClanVS;
			model.clanVSTag		= viewModel.ClanVSTag;
			model.note			= viewModel.Note;
			model.warStartedOn	= viewModel.WarStartedOn;
			model.warNote		= viewModel.WarNote;
			model.isFinalized	= viewModel.IsFinalized;
			model.addedOn		= viewModel.AddedOn;
			model.updatedOn		= viewModel.UpdatedOn;

			return model;
		}

		/// <summary>
		/// Returns a list ActionLog objects of the changed values for the passed object.
		/// </summary>
		/// <param name="current">A ClanWar object that has been updated.</param>
		/// <param name="original">A ClanWar object with the original values.</param>
		/// <returns>List of ActionLog</returns>
		private static List<ActionLog> GetChanges(ClanWar current, ClanWar original)
		{
			//vv very inefficient way, but for now should be ok since we are behind schedule!
			Type objectType = current.GetType();
			List<ActionLog> changes = new List<ActionLog>();
			ActionLog changedValue = new ActionLog();

			if (current.warTypeId != original.warTypeId)
			{
				var propName = Nameof<ClanWar>.Property(e => e.warTypeId);
				changedValue.propertyChanged = propName;
				changedValue.newValue = current.warTypeId.ToString();
				changedValue.oldValue = original.warTypeId.ToString();

				changes.Add(changedValue);
			}

			if (current.clanVS != original.clanVS)
			{
				changedValue = new ActionLog();
				var propName = Nameof<ClanWar>.Property(e => e.clanVS);
				changedValue.propertyChanged = propName;
				changedValue.newValue = current.clanVS.ToString();
				changedValue.oldValue = original.clanVS.ToString();

				changes.Add(changedValue);
			}

			if (current.warStartedOn != original.warStartedOn)
			{
				changedValue = new ActionLog();
				var propName = Nameof<ClanWar>.Property(e => e.warStartedOn);
				changedValue.propertyChanged = propName;
				changedValue.newValue = current.warStartedOn.ToString();
				changedValue.oldValue = original.warStartedOn.ToString();

				changes.Add(changedValue);
			}

			if (current.note != original.note)
			{
				changedValue = new ActionLog();
				var propName = Nameof<ClanWar>.Property(e => e.note);
				changedValue.propertyChanged = propName;
				changedValue.newValue = current.note.ToString();
				changedValue.oldValue = original.note.ToString();

				changes.Add(changedValue);
			}

			return changes;
		}
	}

	public class Nameof<T>
	{
		public static string Property<TProp>(Expression<Func<T, TProp>> expression)
		{
			var body = expression.Body as MemberExpression;
			if (body == null)
				throw new ArgumentException("'expression' should be a member expression");
			return body.Member.Name;
		}
	}
}