using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using HouseOfClans.Controllers.Security;
using HouseOfClans.Enums;
using HouseOfClans.Managers;
using HouseOfClans.Models;
using HouseOfClans.Models.ViewModels.Rankings;

namespace HouseOfClans.Controllers
{
    [Authorize]
    public class RankingsController : Controller
    {
        // GET: Rankings
		// At first the list of current clan members in the current war will be listed (seasonal), an option to select overall will exist on page
		// will display main stats, clicking on member will display his/her details
		[HttpGet]
        public ActionResult Index()
        {
			ActionResult result = View();

			ClanUser clanUser = HttpContext.GetCurrentLoginInformation();

			//TODO //vv If current user was able to get to this point then it belongs to a Clan, but should it be checked still?

			int clanId = (int)clanUser.clanId;
			int currentWarId = 0;
			List<ClanWar> clanWars = ClanWarManager.SelectClanWarsByClanId(clanId);

			if (clanWars != null && clanWars.Any() && clanWars.Count > 0)
			{
				currentWarId = ClanWarManager.SelectClanWarsByClanId(clanId).OrderByDescending(p => p.id).First().id;
				RankingsUpsertViewModel rankings = new RankingsUpsertViewModel(currentWarId, clanId);
				result = View(rankings);
			}

			ViewBag.IsOverall = false;

			return result;
        }

		/// <summary>
		/// Display the Clan members rankings, by season (current war) or overall
		/// </summary>
		/// <param name="isOverall">Current War or Overall</param>
		/// <returns></returns>
		[HttpPost]
		public ActionResult Index(bool isOverall)
		{
			ActionResult result = View();

			ClanUser clanUser = HttpContext.GetCurrentLoginInformation();

			//TODO //vv If current user was able to get to this point then it belongs to a Clan, but should it be checked still?

			int clanId = (int)clanUser.clanId;
			int currentWarId = ClanWarManager.SelectClanWarsByClanId(clanId).OrderByDescending(p => p.id).First().id;

			RankingsUpsertViewModel rankings = new RankingsUpsertViewModel(currentWarId, clanId, isOverall);
			ViewBag.IsOverall = isOverall;
			result = View(rankings);

			return result;
		}

		[AuthorizeUser(ClanRole.Leader, ClanRole.CoLeader)]
		[HttpGet]
		public ActionResult Edit(int id)
		{
			ActionResult result = View();
			ClanUser clanUser = HttpContext.GetCurrentLoginInformation();

			//TODO //vv If current user was able to get to this point then it belongs to a Clan, but should it be checked still?

			int clanId = (int)clanUser.clanId;
			ViewBag.WarId = id;
			ViewBag.IsWarFinalized = ClanWarManager.SelectByClanWarId(id).isFinalized;
			RankingsUpsertViewModel rankings = new RankingsUpsertViewModel(id, clanId);

			result = View(rankings);

			return result;
		}

		[AuthorizeUser(ClanRole.Leader, ClanRole.CoLeader)]
		[HttpPost]
		public ActionResult Edit(int id, IEnumerable<ClanMemberStatsViewModel> memberRankingsFromForm)
		{
			ActionResult result = View();

			RankingsUpsertViewModel rankingsModelView = new RankingsUpsertViewModel(memberRankingsFromForm.ToList());
			RankingsManager.Upsert(rankingsModelView.WarRankings);

			rankingsModelView = new RankingsUpsertViewModel(id, memberRankingsFromForm.Select(p => p.ClanId).First());
			ViewBag.WarId = id;
			result = View(rankingsModelView);

			return result;
		}

		[AuthorizeUser(ClanRole.Leader, ClanRole.CoLeader)]
		public ActionResult AddAttack(int id, int warId)
		{
			ClanMemberStatsViewModel addSingle = new ClanMemberStatsViewModel()
			{
				ClanWarId = warId
			};

			List<ClanUser> clanUsers = ClanUserManager.SelectAllByClanId(id);

			ViewBag.AvailableAttacks = RankingsManager.SelectAttacksAvailableForWar(warId).ToDictionary(key => key.clanUserId, value => clanUsers.Where(p => p.id == value.clanUserId).Select(p => p.name).Single());

			return PartialView("_ClanMemberStatsViewModel", addSingle);
		}
    }
}