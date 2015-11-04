using HouseOfClans.Managers;
using HouseOfClans.Models;
using HouseOfClans.Models.ViewModels.Members;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using HouseOfClans.Enums;

namespace HouseOfClans.Controllers
{
    [Authorize]
    public class MembersController : Controller
    {
        //
        // GET: Members
        // Displays all the members of the clan
        public ActionResult Index()
        {
            int? clanId = ClanManager.GetClanId(User.Identity.GetUserId());

            MembersViewModel model = new MembersViewModel((int)clanId, editMode: false);
            
            return View(model);
        }

        //
        // GET: Members Edit Mode
        // Displays all the members of the clan in edit mode
        public ActionResult EditMode()
        {
            int? clanId = ClanManager.GetClanId(User.Identity.GetUserId());

            MembersViewModel model = new MembersViewModel((int)clanId, editMode: true);

            return View("Index", model);
        }

        //
        // GET: Create
        // Displays form to create member
        [HttpGet]
        public ActionResult Create()
        {
            return View(new CreateMemberViewModel());
        }

        //
        // POST: Create
        // Creates a Clan User
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateMemberViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ClanUser newMember = new ClanUser();

                    newMember.name = model.Name;
                    newMember.userRoleId = (short)ClanRole.Member;
                    newMember.clanId = ClanManager.GetClanId(User.Identity.GetUserId());

                    ClanUserManager.Insert(newMember);
                }
            }
            catch
            {
                return View(new CreateMemberViewModel());
            }

            return RedirectToAction("EditMode");
        }

        //
        // GET: Promote
        // Promotes a clan member
        public ActionResult Promote(int memberId)
        {
            ClanUser promoteUser = ClanUserManager.SelectByClanUserId(memberId);

            if(promoteUser.userRoleId == (short)ClanRole.Member)
            {
                promoteUser.userRoleId = (short)ClanRole.Elder;
            }
            else if (promoteUser.userRoleId == (short)ClanRole.Elder)
            {
                promoteUser.userRoleId = (short)ClanRole.CoLeader;
            }

            ClanUserManager.Update(promoteUser);

            return RedirectToAction("EditMode");
        }

        //
        // GET: Demote
        // Demotes a clan member
        public ActionResult Demote(int memberId)
        {
            ClanUser demoteUser = ClanUserManager.SelectByClanUserId(memberId);

            if (demoteUser.userRoleId == (short)ClanRole.CoLeader)
            {
                demoteUser.userRoleId = (short)ClanRole.Elder;
            }
            else if (demoteUser.userRoleId == (short)ClanRole.Elder)
            {
                demoteUser.userRoleId = (short)ClanRole.Member;
            }

            ClanUserManager.Update(demoteUser);

            return RedirectToAction("EditMode");
        }
        
        //
        // GET: Delete
        // Removes a clan member
        public ActionResult Remove(int memberId)
        {
            ClanUser removeUser = ClanUserManager.SelectByClanUserId(memberId);
            
            removeUser.clanId = null;
            ClanUserManager.Update(removeUser);

            return RedirectToAction("EditMode");
        }

        //
        // GET: Accept
        // Accepts a clan member
        public ActionResult AcceptMember(int reqId, int clanId)
        {
            Request req = RequestManager.SelectById(reqId);

            req.accepted = true;
            req.processed = true;

            RequestManager.Update(req);

            ClanUser user = ClanUserManager.SelectByAspNetUserId(req.aspnetUserId);

            user.clanId = clanId;
            user.userRoleId = (int)ClanRole.Member;

            ClanUserManager.Update(user);

            return RedirectToAction("EditMode");
        }

        //
        // GET: Reject
        // Rejects a request
        public ActionResult RejectMember(int reqId)
        {
            Request req = RequestManager.SelectById(reqId);

            req.accepted = false;
            req.processed = true;

            RequestManager.Update(req);

            return RedirectToAction("EditMode");
        }

    }
}