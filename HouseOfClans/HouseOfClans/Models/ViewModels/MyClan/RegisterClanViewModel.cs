using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HouseOfClans.Models.ViewModels
{
    public class RegisterClanViewModel
    {
        [Required]
        [Display(Name = "Clan Name")]
        [StringLength(25, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 4)]
        public string ClanName { get; set; }

        [Required]
        [Display(Name = "Clan Description")]
        [StringLength(500, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 25)]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Clan Shield")]
        public string Shield { get; set; }

        public List<SelectListItem> _ShieldLogos { get; set; }
        public List<SelectListItem> ShieldLogos
        {
            get
            {
                if(_ShieldLogos == null)
                {
                    _ShieldLogos = new List<SelectListItem>();
                    
                    for (int i = 1; i < 51; i++)
                    {
                        _ShieldLogos.Add(new SelectListItem() { Text = "Shield #" + i, Value = "../Content/Images/ClanShields/badge_" + i + ".png" });
                    }
                }
                
                return _ShieldLogos;
            }
        }
    }
}