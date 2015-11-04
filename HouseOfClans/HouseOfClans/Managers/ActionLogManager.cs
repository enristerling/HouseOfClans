using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HouseOfClans.Models;

namespace HouseOfClans.Managers
{
	public static class ActionLogManager
	{
		/// <summary>
		/// Inserts a new Action Log entry
		/// </summary>
		private static void Log(Enums.Action action, int userId, string objectChanged, string propertyChanged, string oldValue, string newValue)
		{
			ActionLog entry = new ActionLog();
			entry.actionId = (short)action;
			entry.userClanId = userId;
			entry.objectChanged = objectChanged;
			entry.propertyChanged = propertyChanged;
			entry.oldValue = oldValue;
			entry.newValue = newValue;
			entry.addedOn = DateTime.Now;

			using (var dbContext = new HouseOfClansEntities())
			{
				dbContext.ActionLogs.Add(entry);
				dbContext.SaveChanges();
			}
		}

		public static void Log<T>(Enums.Action action, int userId, string propertyChanged, string oldValue, string newValue)
		{
			string objectChanged = typeof(T).Name.ToString();
			string friendlyObjectChangeName = objectChanged;

			//vv switch-case statement doesn't work with variable values in the cases, so we'll use If-then statement, unless we found something better.
			if (string.Compare(objectChanged, typeof(Models.ClanWar).Name.ToString(), true) == 0
				|| string.Compare(objectChanged, typeof(Models.ViewModels.ClanWar.ClanWarViewModel).Name.ToString(), true) == 0
				|| string.Compare(objectChanged, typeof(Models.ViewModels.ClanWar.ClanWarUpsertViewModel).Name.ToString(), true) == 0
				|| string.Compare(objectChanged, typeof(Managers.ClanWarManager).Name.ToString(), true) == 0)
			{
					friendlyObjectChangeName = "ClanWar";
			};

			if (string.Compare(objectChanged, typeof(Models.ClanWarPick).Name.ToString(), true) == 0
				|| string.Compare(objectChanged, typeof(Models.ViewModels.ClanWarPick.ClanWarPickUpsertViewModel).Name.ToString(), true) == 0
				|| string.Compare(objectChanged, typeof(Models.ViewModels.ClanWarPick.ClanWarPickViewModel).Name.ToString(), true) == 0)
			{
				friendlyObjectChangeName = "ClanWarPicks";
			};

			if (string.Compare(objectChanged, typeof(Models.WarRanking).Name.ToString(), true) == 0
				|| string.Compare(objectChanged, typeof(Models.ViewModels.Rankings.ClanMemberStatsViewModel).Name.ToString(), true) == 0
				|| string.Compare(objectChanged, typeof(Models.ViewModels.Rankings.RankingsUpsertViewModel).Name.ToString(), true) == 0
				|| string.Compare(objectChanged, typeof(Models.ViewModels.Rankings.RankingsViewModel).Name.ToString(), true) == 0
				|| string.Compare(objectChanged, typeof(Managers.RankingsManager).Name.ToString(), true) == 0)
			{
				friendlyObjectChangeName = "ClanRankings";
			};

			//TODO //vv add the rest of the ElseIf statements to log the specific object

			Log(action, userId, friendlyObjectChangeName, propertyChanged, oldValue, newValue);
		}

		public static void Log<T>(Enums.Action action, int userId, List<ActionLog> changes)
		{
			foreach (ActionLog change in changes)
			{
				ActionLogManager.Log<T>(action, userId, change.propertyChanged, change.oldValue, change.newValue);
			}
		}

		/// <summary>
		/// Get all logs from db (raw from table). This is meant to be a debug/audit tool for Admins, we don't want it accessible or used by anything/anybody else.
		/// Rarely used. Right now only used on the 'hidden' logs page. Might be deprecated soon or moved to its ActionLog Manager/ModelView. //vv
		/// </summary>
		public static List<ActionLog> GetAll()
		{
			List<ActionLog> logs = new List<ActionLog>();

			using(var dbContext = new HouseOfClansEntities())
			{
				logs = dbContext.ActionLogs.ToList();
			}

			return logs;
		}

	}
}