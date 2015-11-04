using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using HouseOfClans.Managers;

namespace HouseOfClans.Models.ViewModels.Blacklist
{
    public class UpsertBlacklistUserViewModel
    {
        public int Id { get; set; }
        public int ClanUserId { get; set; }
        
        [Required]
        [StringLength(250, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 5)]
        public string Notes { get; set; }
    }
}