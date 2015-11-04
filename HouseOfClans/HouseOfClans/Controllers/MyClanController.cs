using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using HouseOfClans.Controllers.Security;
using HouseOfClans.Models;
using HouseOfClans.Models.ViewModels;
using HouseOfClans.Enums;
using HouseOfClans.Managers;

namespace HouseOfClans.Controllers
{
    [Authorize]
    public class MyClanController : Controller
    {
        public ActionResult Index()
        {
            int? clanId = ClanManager.GetClanId(User.Identity.GetUserId());

            if (clanId != null && clanId > 0)
            {
                return RedirectToAction("Index", "ClanInfo");
            }
            else 
            {
                Request req = RequestManager.SelectByAspnetUserId(User.Identity.GetUserId());

                if(req != null && req.processed == false)
                {
                    return RedirectToAction("RequestPending");
                }
                else if(req != null && req.processed == true && req.accepted == false)
                {
                    return RedirectToAction("RequestRejected", new { reqId = req.id } );
                }
                return RedirectToAction("JoinOrRegister");                   
            }
        }

        //
        // GET: /MyClan/JoinOrRegister
        // Let's the user choose between registering a clan or join an excisting clan
        public ActionResult JoinOrRegister()
        {
            return View();
        }

        //
        // GET: /MyClan/RequestPending
        // Let's the user choose between registering a clan or join an excisting clan
        public ActionResult RequestPending()
        {
            return View();
        }

        //
        // GET: /MyClan/RequestRejected
        // Infoms the user that request was rejected
        public ActionResult RequestRejected(int reqId)
        {
            bool deleted = RequestManager.Delete(reqId);

            return View();
        }

        //
        // GET: /MyClan/SearchClan
        // PartialView to show clan search results
        [HttpPost]
        public PartialViewResult SearchClan(string clanTag)
        {
            Clan clan = ClanManager.SelectByClanTagId(clanTag);

            return PartialView(clan);
        }

        //
        // GET: /MyClan/RequestJoin
        // Request to join a Clan
        [HttpGet]
        public ActionResult RequestJoin(int clanId)
        {
            Request req = new Request();

            req.aspnetUserId = User.Identity.GetUserId();
            req.clanId = clanId;
            req.processed = false;
            req.accepted = false;

            RequestManager.Insert(req);

            return RedirectToAction("Index");
        }

        //
        // GET: /MyClan/RegisterCLan
        // Form to register a Clan
        [HttpGet]
        public ActionResult RegisterClan()
        {
            return View(new RegisterClanViewModel());
        }
        
        //
        // POST: /MyClan/RegisterCLan 
        // Creates a Clan and a Clan User tied to the AspNetUser
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RegisterClan(RegisterClanViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Clan newClan = new Clan();

                    newClan.name = model.ClanName;
                    newClan.description = model.Description;
                    newClan.shieldLogoLocation = model.Shield;

                    ClanManager.Insert(newClan);
					ClanUser clanUser = HttpContext.GetCurrentLoginInformation();

                    if(clanUser != null)
                    {
                        clanUser.clanId = newClan.id;
                        clanUser.userRoleId = (int)ClanRole.Leader;
                        ClanUserManager.Update(clanUser);
                    }
                    else
                    {
                        return View(new RegisterClanViewModel());
                    }
                }
            }
            catch
            {
                return View(new RegisterClanViewModel());
            }

            return RedirectToAction("Index", "ClanInfo");
        }
    }
}