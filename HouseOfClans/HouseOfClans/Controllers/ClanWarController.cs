using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using HouseOfClans.Models;
using HouseOfClans.Models.ViewModels.ClanWar;
using HouseOfClans.Models.ViewModels.ClanWarPick;
using HouseOfClans.Managers;
using HouseOfClans.Controllers.Security;
using HouseOfClans.Enums;

namespace HouseOfClans.Controllers
{
	[Authorize]
    public class ClanWarController : Controller
    {
        // GET: ClanWar
        public ActionResult Index()
        {
			ClanUser clanUser = HttpContext.GetCurrentLoginInformation();
			int clanId = clanUser.clanId != null ? (int)clanUser.clanId : 0;

			ClanWarViewModel clanWarsInfo = new ClanWarViewModel(clanId);

			return View(clanWarsInfo);
        }

        // GET: ClanWar/Details/5
        [HttpGet]
		public ActionResult Details(int id)
        {
			ClanUser clanUser = HttpContext.GetCurrentLoginInformation();

			int clanId = clanUser.clanId != null ? (int)clanUser.clanId : 0;

			ClanWarViewModel warInfo = new ClanWarViewModel(id, clanId);

            return View(warInfo);
        }

		// POST: ClanWar/Details/5
		[AuthorizeUser(ClanRole.Leader, ClanRole.CoLeader)]
		[HttpPost]
		public ActionResult Details(int id, string warNote)
		{
			ActionResult result = View();
			ClanUser clanUser = HttpContext.GetCurrentLoginInformation();
			ClanWarUpsertViewModel clanWarInfo = ClanWarUpsertViewModel.Get(id);

			//The only updatable field when war started is the war notes.
			clanWarInfo.WarNote = warNote;
			ClanWarManager.Upsert(clanWarInfo, clanUser.id);
			result = RedirectToAction("Index");

			return result;
		}

        // GET: ClanWar/Create
		[AuthorizeUser(ClanRole.Leader, ClanRole.CoLeader)]
		[HttpGet]
        public ActionResult Create()
        {
			ClanUser clanUser = HttpContext.GetCurrentLoginInformation();
			int clanId = clanUser.clanId != null ? (int)clanUser.clanId : 0;

			ClanWarUpsertViewModel clanWar = new ClanWarUpsertViewModel();
			clanWar.ClanId = clanId;
			clanWar.WarStartedOn = DateTime.Now.AddDays(1);

            return View("Edit", clanWar);
        }

        // GET: ClanWar/Edit/5
		[AuthorizeUser(ClanRole.Leader, ClanRole.CoLeader)]
		[HttpGet]
        public ActionResult Edit(int id)
        {
			ClanWarUpsertViewModel clanWarInfo = ClanWarUpsertViewModel.Get(id);

			return View(clanWarInfo);
        }

        // POST: ClanWar/Edit/5
		[AuthorizeUser(ClanRole.Leader, ClanRole.CoLeader)]
        [HttpPost]
		public ActionResult Edit(ClanWarUpsertViewModel clanWar)
        {
			ActionResult result = View();

            try
            {
				ClanUser clanUser = HttpContext.GetCurrentLoginInformation();
				int clanWarId = ClanWarManager.Upsert(clanWar, clanUser.id);

				if (clanWarId > 0)
				{
					//Insert member picks
					//vv List<WarRanking> rankings = new List<WarRanking>();
					List<ClanWarPickViewModel> picks = new List<ClanWarPickViewModel>();
					List<ClanUser> clanMembers = ClanUserManager.SelectAllByClanId(clanWar.ClanId);
					short i = 1;

					foreach (ClanUser member in clanMembers)
					{
						WarRanking ranking = new WarRanking()
						{
							clanUserId = member.id,
							clanWarId = clanWarId
						};

						//vv rankings.Add(ranking);

						ClanWarPickViewModel pick = new ClanWarPickViewModel()
						{
							ClanUserId = member.id,
							ClanMemberWarPosition = i,
							ClanWarId = clanWarId,
							PickVS = i
						};

						picks.Add(pick);
						i++;
					}

					//vv RankingsManager.Upsert(rankings);
					ClanWarPicksManager.Upsert(picks);
				}
				//TODO //vv an Else here should display a message stating that the war was not inserted (and no exception apparently)

				result = RedirectToAction("Index");
            }
            catch
            {
				result = View(clanWar);
            }

			return result;
        }

        // GET: ClanWar/Delete/5
		[AuthorizeUser(ClanRole.Leader, ClanRole.CoLeader)]
		[HttpGet]
        public ActionResult Delete(int id)
        {
			//Delete WarPicks, WarRankings, then ClanWar
			ClanWarPicksManager.Delete(id);
			RankingsManager.Delete(id);
			ClanWarManager.Delete(id);

			//TODO //vv check if any of the Deletes fail.. then handle.

			return RedirectToAction("Index");
        }
		
		// POST: ClanWar/Details/5
		[AuthorizeUser(ClanRole.Leader, ClanRole.CoLeader)]
		[HttpGet]
		public ActionResult Finalize(int id)
		{
			ActionResult result = View();
			ClanUser clanUser = HttpContext.GetCurrentLoginInformation();
			ClanWarUpsertViewModel clanWarInfo = ClanWarUpsertViewModel.Get(id);

			//The only updatable field when war started is the war notes.
			clanWarInfo.IsFinalized = true;
			ClanWarManager.Upsert(clanWarInfo, clanUser.id);
			result = RedirectToAction("Index");

			return result;
		}
    }
}
