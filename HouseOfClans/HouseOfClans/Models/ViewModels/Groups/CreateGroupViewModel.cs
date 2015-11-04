using HouseOfClans.Managers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using HouseOfClans.Enums;

namespace HouseOfClans.Models.ViewModels.Groups
{
    public class CreateGroupViewModel
    {
        public CreateGroupViewModel()
        {
        }

        public CreateGroupViewModel(int clanId)
        {
            this.ClanId = clanId;
        }

        public int ClanId { get; set; }

        [Required]
        [Display(Name = "Group Name")]
        [StringLength(25, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 4)]
        public string GroupName { get; set; }

        [Required]
        [Display(Name = "Group Leader")]
        public string GroupLeaderId { get; set; }

        [Display(Name = "Group Members")]
        public List<string> GroupMemberIds { get; set; }

        public List<SelectListItem> _AvailableColeaders { get; set; }
        public List<SelectListItem> AvailableColeaders
        {
            get
            {
                if (_AvailableColeaders == null)
                {
                    _AvailableColeaders = new List<SelectListItem>();
                    List<ClanUser> Coleaders = new List<ClanUser>();

                    Coleaders = ClanUserManager.SelectClanCoLeadersByClanId(this.ClanId);
                    Coleaders.RemoveAll(c => c.clanGroupId != null);

                    foreach(var coleader in Coleaders)
                    {
                        _AvailableColeaders.Add(new SelectListItem() { Text = coleader.name, Value = coleader.id.ToString() });
                    }
                }

                return _AvailableColeaders;
            }
        }

        public List<SelectListItem> _FreeAgents { get; set; }
        public List<SelectListItem> FreeAgents
        {
            get
            {
                if (_FreeAgents == null)
                {
                    _FreeAgents = new List<SelectListItem>();
                    List<ClanUser> freeAgents = new List<ClanUser>();

                    freeAgents = ClanUserManager.SelectFreeAgentsByClanId(this.ClanId);
                    freeAgents.RemoveAll(c => c.userRoleId == (short)ClanRole.CoLeader);

                    foreach (var agent in freeAgents)
                    {
                        _FreeAgents.Add(new SelectListItem() { Text = agent.name, Value = agent.id.ToString() });
                    }
                }

                return _FreeAgents;
            }
        }
    }
}