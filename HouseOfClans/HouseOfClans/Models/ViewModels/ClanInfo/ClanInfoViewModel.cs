using HouseOfClans.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HouseOfClans.Models.ViewModels
{
	public class ClanInfoViewModel : ClanBaseViewModel
    {
		public ClanInfoViewModel()
		{
		}

		public ClanInfoViewModel(int id, bool editMode) : base(id)
		{
            this.IsEditMode = editMode;
		}

		#region Properties

        public string Description
        {
            get
            {
                return ClanInfo.description;
            }
        }

        public List<Rule> Rules
        {
            get
            {
                List<Rule> rulesList = new List<Rule>();
                rulesList = RuleManager.SelectAllByClanId(ClanInfo.id);
                return rulesList;
            }
        }

        public bool IsEditMode { get; set; }

		#endregion
	}
}