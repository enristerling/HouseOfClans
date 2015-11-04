using HouseOfClans.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using HouseOfClans.Models.ViewModels.Groups;
using HouseOfClans.Models;

namespace HouseOfClans.Controllers
{
    [Authorize]
    public class GroupsController : Controller
    {
        //
        // GET: /Groups/Index
        // Displays all the groups of the clan
        public ActionResult Index()
        {
            int? clanId = ClanManager.GetClanId(User.Identity.GetUserId());

            GroupsViewModel model = new GroupsViewModel((int)clanId, editMode: false);

            return View(model);
        }

        //
        // GET: /Groups/EditMode
        // Displays all the groups of the clan in edit mode
        public ActionResult EditMode()
        {
            int? clanId = ClanManager.GetClanId(User.Identity.GetUserId());

            GroupsViewModel model = new GroupsViewModel((int)clanId, editMode: true);

            return View("Index", model);
        }

        //
        // GET: /Groups/Create
        // Displays form to create a group
        [HttpGet]
        public ActionResult Create()
        {
            int? clanId = ClanManager.GetClanId(User.Identity.GetUserId());

            return View(new CreateGroupViewModel((int)clanId));
        }

        //
        // POST: /Groups/Create
        // Creates a Clan User
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateGroupViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ClanGroup newGroup = new ClanGroup();
                    
                    int leaderId = 0;
                    int.TryParse(model.GroupLeaderId, out leaderId);
                    
                    newGroup.name = model.GroupName;
                    newGroup.groupLeaderId = leaderId;
                    newGroup.clanId = model.ClanId;

                    ClanGroupManager.Insert(newGroup);

                    ClanUser groupLeader = ClanUserManager.SelectByClanUserId(leaderId);
                    groupLeader.clanGroupId = newGroup.id;

                    ClanUserManager.Update(groupLeader);

                    if(model.GroupMemberIds != null && model.GroupMemberIds.Count > 0)
                    {
                        foreach(var memberId in model.GroupMemberIds)
                        {
                            int id = 0;
                            int.TryParse(memberId, out id);

                            ClanUser member = ClanUserManager.SelectByClanUserId(id);
                            member.clanGroupId = newGroup.id;

                            ClanUserManager.Update(member);
                        }
                    }
                }
            }
            catch
            {
                int? clanId = ClanManager.GetClanId(User.Identity.GetUserId());

                return View(new CreateGroupViewModel((int)clanId));
            }

            return RedirectToAction("EditMode");
        }

        //
        // GET: /Groups/Remove
        // Removes a member from a group
        public ActionResult Remove(int memberId)
        {
            ClanUser removeUser = ClanUserManager.SelectByClanUserId(memberId);

            removeUser.clanGroupId = null;
            ClanUserManager.Update(removeUser);

            return RedirectToAction("EditMode");
        }

        //
        // GET: /Groups/Move
        // Moves a member to a group
        [HttpGet]
        public ActionResult Move(int memberId, int? groupId = null)
        {
            int? clanId = ClanManager.GetClanId(User.Identity.GetUserId());

            return View(new MoveMemberViewModel((int)clanId, memberId, groupId));
        }

        //
        // POST: /Groups/Move
        // Moves a member to a group
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Move(MoveMemberViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int newGroupId = 0;
                    int.TryParse(model.NewGroupId, out newGroupId);

                    ClanUser moveMember = ClanUserManager.SelectByClanUserId(model.MoveMemberId);
                    moveMember.clanGroupId = newGroupId;

                    ClanUserManager.Update(moveMember);
                }
            }
            catch
            {
                return View();
            }

            return RedirectToAction("EditMode");
        }
    }
}