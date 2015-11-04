using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HouseOfClans.Models.ViewModels.ClanWarPick;
using HouseOfClans.Managers;

namespace HouseOfClans.Controllers
{
    public class WarPicksController : Controller
    {
        // GET: WarPicks/Edit/5
        public ActionResult Edit(int id)
        {
			List<ClanWarPickViewModel> picksList = new List<ClanWarPickViewModel>();
			picksList = ClanWarPickViewModel.GetAllByWar(id);

			return View(picksList);
        }

        // POST: WarPicks/Edit/5
        [HttpPost]
		public ActionResult Edit(int id, IEnumerable<ClanWarPickViewModel> warPicks)
        {
            try
            {
				ClanWarPicksManager.Upsert(warPicks.ToList());
                return RedirectToAction("Index", "ClanWar");
            }
            catch
            {
                return View();
            }
        }

		[HttpPost]
		public ActionResult SaveSortablePicks(string[] sortedPicks)
		{
			//The order received of the array is the current order. The value of each item is the id of the warpick
			ClanWarPicksManager.UpdateMemberWarPosition(sortedPicks);

			return View();
		}
    }
}
