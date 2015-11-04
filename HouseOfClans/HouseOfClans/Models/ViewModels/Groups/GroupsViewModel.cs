using HouseOfClans.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HouseOfClans.Models.ViewModels.Groups
{
    public class GroupsViewModel : ClanBaseViewModel
    {
        public GroupsViewModel()
        {
        }

        public GroupsViewModel(int clanId, bool editMode = false) : base(clanId) 
        {
            this.IsEditMode = editMode;
        }

        public List<ClanGroup> _Groups { get; set; }
        public List<ClanGroup> Groups 
        { 
            get
            {
                if(_Groups == null)
                {
                    _Groups = new List<ClanGroup>();
                    _Groups = ClanGroupManager.SelectAllByClanId(this.ClanId); 
                }

                return _Groups;
            }
        }

        
        public ClanUser GetGroupLeader(int groupLeaderId)
        {
            ClanUser leader = ClanUserManager.SelectByClanUserId(groupLeaderId);

            return leader;
        }

        public List<ClanUser> GetGroupMembers(int groupId)
        {
            List<ClanUser> groupMembers = ClanUserManager.SelectGroupMembersByGroupId(groupId);

            return groupMembers; 
        }

        public List<ClanUser> GetFreeAgents()
        {
            List<ClanUser> freeAgents = ClanUserManager.SelectFreeAgentsByClanId(this.ClanId);

            return freeAgents;
        }

        public bool IsEditMode { get; set; }
    }
}