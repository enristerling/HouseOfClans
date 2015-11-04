using HouseOfClans.Managers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HouseOfClans.Models.ViewModels.Blacklist
{
    public class BlacklistViewModel : ClanBaseViewModel
    {
        public class BlacklistUser
        {
            [Display(Name="Village Name")]
            public string VillageName { get; set; }
            public UserBlackList BlacklistEntity { get; set; }
        };
        
        
        public BlacklistViewModel()
        {
        }

        public BlacklistViewModel(int clanId, bool editMode = false) : base(clanId)
        {
            IsEditMode = editMode;
        }

        private List<BlacklistUser> _BlacklistedUsers { get; set; }
        public List<BlacklistUser> BlacklistedUsers
        { 
            get
            {
                if(_BlacklistedUsers == null)
                {
                    List<UserBlackList> list = BlacklistManager.SelectBlackListsByClanId(this.ClanId);
                    _BlacklistedUsers = new List<BlacklistUser>();

                    foreach (UserBlackList blUser in list)
                    {
                        BlacklistUser user = new BlacklistUser();
                        ClanUser clanUser = ClanUserManager.SelectByClanUserId(blUser.clanUserId);
                        user.VillageName = clanUser.name;
                        user.BlacklistEntity = blUser;

                        _BlacklistedUsers.Add(user);
                    }
                }

                return _BlacklistedUsers;
            } 
        }

        public bool IsEditMode { get; set; } 
    }
}