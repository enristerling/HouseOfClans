using HouseOfClans.Managers;
using HouseOfClans.Models;
using HouseOfClans.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using HouseOfClans.Models.ViewModels.ClanInfo;

namespace HouseOfClans.Controllers
{
    [Authorize]
    public class ClanInfoController : Controller
    {
        //
        // GET: ClanInfo
        // Displays the Clan Info Tab
        public ActionResult Index()
        {
            int? clanId = ClanManager.GetClanId(User.Identity.GetUserId());

            if (clanId != null && clanId != 0)
            {
                ClanInfoViewModel clanInfo = new ClanInfoViewModel((int)clanId, editMode: false);

                return View(clanInfo);
            }
            
            // Needs to be changed to redirect to a custom error page. ES
            return RedirectToAction("JoinOrRegister", "MyClan");
        }

        //
        // GET: EditMode
        // Displays Clan Info in edit mode
        public ActionResult EditMode()
        {
            int? clanId = ClanManager.GetClanId(User.Identity.GetUserId());

            ClanInfoViewModel model = new ClanInfoViewModel((int)clanId, editMode: true);

            return View("Index", model);
        }

        //
        // GET: EditDescription
        // Displays form to edit description
        [HttpGet]
        public ActionResult EditDescription(int clanId)
        {
            return View(new EditDescriptionViewModel(clanId));
        }

        //
        // POST: EditDescription
        // Updates a clan description
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditDescription(EditDescriptionViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Clan updateClan = ClanManager.SelectByClanId(model.ClanId);
                    
                    updateClan.description = model.Description;

                    ClanManager.Update(updateClan);
                }
            }
            catch
            {
                return View();
            }

            return RedirectToAction("Index");
        }


        //
        // GET: AddRule
        // Displays Rule form
        [HttpGet]
        public ActionResult AddRule()
        {
            return View(new UpsertRuleViewModel());
        }

        //
        // POST: AddRule
        // Creates a new Rule
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddRule(UpsertRuleViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Rule newRule = new Rule();

                    newRule.clanId = (int)ClanManager.GetClanId(User.Identity.GetUserId());
                    newRule.description = model.Description;

                    RuleManager.Insert(newRule);
                }
            }
            catch
            {
                return View();
            }

            return RedirectToAction("Index");
        }

        //
        // GET: Delete
        // Deletes a Rule
        public ActionResult Delete(int ruleId)
        {
            RuleManager.Delete(ruleId);

            return RedirectToAction("Index");
        }

        //
        // GET: EditRule
        // Displays form to edit a Rule
        [HttpGet]
        public ActionResult EditRule(int ruleId)
        {
            return View(new UpsertRuleViewModel(ruleId));
        }

        //
        // POST: EditRule
        // Updates a Rule
        [HttpPost]
        public ActionResult EditRule(UpsertRuleViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Rule newRule = RuleManager.SelectByRuleId(model.RuleId);
                    newRule.description = model.Description;
                    RuleManager.Update(newRule);
                }
            }
            catch
            {
                return View();
            }

            return RedirectToAction("Index");
        }
    }
}