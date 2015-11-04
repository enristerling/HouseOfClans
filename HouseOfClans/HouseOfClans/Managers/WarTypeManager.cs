using HouseOfClans.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HouseOfClans.Managers
{
	public class WarTypeManager
	{
		/// <summary>
		/// Gets the list of all available war types
		/// </summary>
		/// <returns>A WarType object list</returns>
		public static List<WarType> GetAll()
		{
			List<WarType> warTypes = new List<WarType>();

			using (var dbContext = new HouseOfClansEntities())
			{
				warTypes = dbContext.WarTypes.Select(warType => warType).OrderBy(p => p.id).ToList();
			}

			return warTypes;
		}

		public static WarType Get(int id)
		{
			WarType warType = new WarType();

			using (var dbContext = new HouseOfClansEntities())
			{
				warType = dbContext.WarTypes.Where(p => p.id == id).FirstOrDefault();
			}

			return warType;
		}
	}
}