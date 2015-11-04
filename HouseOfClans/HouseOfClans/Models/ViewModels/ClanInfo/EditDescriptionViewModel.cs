using HouseOfClans.Managers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HouseOfClans.Models.ViewModels.ClanInfo
{
    public class EditDescriptionViewModel
    {

        public EditDescriptionViewModel()
        {
        }

        public EditDescriptionViewModel(int clanId)
        {
            Clan clan = ClanManager.SelectByClanId(clanId);

            this.ClanId = clan.id;
            this.Description = clan.description;
        }

        public int ClanId { get; set; }

        [Required]
        [Display(Name = "Clan Description")]
        [StringLength(500, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 25)]
        public string Description { get; set; }
    }
}