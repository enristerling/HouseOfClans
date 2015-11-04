using HouseOfClans.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HouseOfClans.Models.ViewModels.Groups
{
    public class MoveMemberViewModel
    {
        public MoveMemberViewModel()
        {
        }

        public MoveMemberViewModel(int clanId, int moveMemberId, int? groupId = null)
        {
            this.ClanId = clanId;
            this.CurrentGroupId = groupId;
            this.MoveMemberId = moveMemberId;
        }

        public int ClanId { get; set; }
        public int? CurrentGroupId { get; set; }
        public int MoveMemberId { get; set; }

        public string NewGroupId { get; set; }

        private List<SelectListItem> _MoveToGroups { get; set; }
        public List<SelectListItem> MoveToGroups
        {
            get
            {
                if(_MoveToGroups == null)
                {
                    _MoveToGroups = new List<SelectListItem>();
                    List<ClanGroup> groups = ClanGroupManager.SelectAllByClanId(this.ClanId);

                    if (this.CurrentGroupId != null)
                    {
                        groups.RemoveAll(group => group.id == this.CurrentGroupId);
                    }

                    foreach (var group in groups)
                    {
                        _MoveToGroups.Add(new SelectListItem() { Text = group.name, Value = group.id.ToString() });
                    }
                }

                return _MoveToGroups;
            }
        }
    }
}