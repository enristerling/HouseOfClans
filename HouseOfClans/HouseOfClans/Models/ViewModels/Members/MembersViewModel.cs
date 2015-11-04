using HouseOfClans.Managers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HouseOfClans.Models.ViewModels.Members
{
    public class MembersViewModel : ClanBaseViewModel
    {
        public MembersViewModel() 
        { 
        }

        public MembersViewModel(int clanId, bool editMode) : base(clanId)
        {
            this.IsEditMode = editMode;
        }

        private ClanUser _Leader { get; set; }
        public ClanUser Leader
        {
            get 
            {
                if(_Leader == null)
                {
                    _Leader = new ClanUser();
                    _Leader = ClanUserManager.SelectClanLeaderByClanId(ClanId);
                }
                
                return _Leader;
            }
        }

        public List<ClanUser> _CoLeaders { get; set; }
        
        [Display(Name = "Co-Leaders")]
        public List<ClanUser> CoLeaders 
        { 
            get
            {
                if(_CoLeaders == null)
                {
                    _CoLeaders = new List<ClanUser>();
                    _CoLeaders = ClanUserManager.SelectClanCoLeadersByClanId(ClanId);
                }
                
                return _CoLeaders;
            }   
        }

        private List<ClanUser> _Elders { get; set; }
        public List<ClanUser> Elders 
        { 
            get
            {
                if (_Elders == null)
                {
                    _Elders = new List<ClanUser>();
                    _Elders = ClanUserManager.SelectClanEldersByClanId(ClanId);
                } 
                
                return _Elders;
            }
        }

        private List<ClanUser> _RegularMembers { get; set; }
        
        [Display(Name = "Members")]
        public List<ClanUser> RegularMembers 
        { 
            get
            {
                if (_RegularMembers == null)
                {
                    _RegularMembers = new List<ClanUser>();
                    _RegularMembers = ClanUserManager.SelectClanRegularMembersByClanId(ClanId);
                }
                
                return _RegularMembers;
            }
        }

        private List<Request> _Requests { get; set; }

        [Display(Name = "Requests")]
        public List<Request> Requests
        {
            get
            {
                if (_Requests == null)
                {
                    _Requests = new List<Request>();
                    _Requests = RequestManager.SelectUnprocessedRequestsByClanId(ClanId);
                }

                return _Requests;
            }
        }

        public string GetName(string aspnetId) 
        {
            ClanUser user = ClanUserManager.SelectByAspNetUserId(aspnetId);

            if(user != null)
            {
                return user.name;
            }
            else
            {
                return "No Name";
            }
        }

        public bool IsEditMode { get; set; }
    }
}