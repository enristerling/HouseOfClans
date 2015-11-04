using HouseOfClans.Managers;
using HouseOfClans.Models.ViewModels.Blacklist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using HouseOfClans.Models;

namespace HouseOfClans.Controllers
{
    public class BlacklistController : Controller
    {
        //
        // GET: Blacklist
        public ActionResult Index()
        {
            int? clanId = ClanManager.GetClanId(User.Identity.GetUserId());
            
            if(clanId != null && clanId > 0)
            {
                BlacklistViewModel model = new BlacklistViewModel((int)clanId);
                return View(model);
            }

            //TODO: Return error page
            return View();
        }

        //
        // GET: Create
        // Displays form to add a member to the Blacklist
        [HttpGet]
        public ActionResult Create(int memberId)
        {
            ClanUser blUser = ClanUserManager.SelectByClanUserId(memberId);

            UpsertBlacklistUserViewModel model = new UpsertBlacklistUserViewModel();
            model.ClanUserId = memberId;
            
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(UpsertBlacklistUserViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UserBlackList newBlacklistUser = new UserBlackList();

                    newBlacklistUser.clanUserId = model.ClanUserId;
                    newBlacklistUser.clanId = (int)ClanManager.GetClanId(User.Identity.GetUserId());
                    newBlacklistUser.note = model.Notes;

                    BlacklistManager.Insert(newBlacklistUser);

                    ClanUser blUser = ClanUserManager.SelectByClanUserId(model.ClanUserId);
                    blUser.clanId = null;
                    ClanUserManager.Update(blUser);
                }
            }
            catch
            {
                return View(new UpsertBlacklistUserViewModel());
            }

            return RedirectToAction("EditMode","Members");
        }
    }
}