using HouseOfClans.Managers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HouseOfClans.Models.ViewModels.ClanInfo
{
    public class UpsertRuleViewModel
    {
        public UpsertRuleViewModel()
        {
        }

        public UpsertRuleViewModel(int ruleId)
        {
            Rule rule = RuleManager.SelectByRuleId(ruleId);

            this.RuleId = rule.id;
            this.Description = rule.description;
            
        }

        public int RuleId { get; set; }

        [Required]
        [Display(Name = "Rule Description")]
        [StringLength(250, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 5)]
        public string Description { get; set; }
    }
}