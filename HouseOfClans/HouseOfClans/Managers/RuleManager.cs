using HouseOfClans.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HouseOfClans.Managers
{
    public class RuleManager
	{
		/// <summary>
		/// Gets a list of rules based on the clan id
		/// </summary>
		/// <param name="clanId">Clan Id</param>
        public static List<Rule> SelectAllByClanId(int? clanId)
        {
            List<Rule> rules = new List<Rule>();

            using (var dbContext = new HouseOfClansEntities())
            {
                rules = dbContext.Rules.Where(p => p.clanId == clanId).ToList();
            }

            return rules; 
        }

        /// <summary>
		/// Gets a rule based on the rule id
		/// </summary>
		/// <param name="clanId">Clan Id</param>
        public static Rule SelectByRuleId(int ruleId)
        {
            Rule rule = new Rule();

            using (var dbContext = new HouseOfClansEntities())
            {
                rule = dbContext.Rules.Where(p => p.id == ruleId).FirstOrDefault();
            }

            return rule; 
        }
		/// <summary>
		/// Inserts a new Rule
		/// </summary>
        public static void Insert(Rule rule)
        {
            using (var dbContext = new HouseOfClansEntities())
            {
                rule.addedOn = DateTime.Now;
                dbContext.Rules.Add(rule);
                dbContext.SaveChanges();
            }
        }
		
		/// <summary>
		/// Updates a Rule
		/// </summary>
        public static void Update(Rule rule)
        {
            using (var dbContext = new HouseOfClansEntities())
            {
                Rule ruleInfo = RuleManager.SelectByRuleId(rule.id);
                ruleInfo.updatedOn = DateTime.Now;

                dbContext.Rules.Attach(ruleInfo);
                dbContext.Entry(ruleInfo).CurrentValues.SetValues(rule);
                dbContext.SaveChanges();
            }
        }

		/// <summary>
		/// Deletes a rule
		/// </summary>
        public static bool Delete(int ruleId)
        {
            bool isDeleted = false;
            Rule deleteRule = RuleManager.SelectByRuleId(ruleId);

            using (var dbContext = new HouseOfClansEntities())
            {
                dbContext.Rules.Attach(deleteRule);
                dbContext.Rules.Remove(deleteRule);
                isDeleted = dbContext.SaveChanges() > 0;
            }

            return isDeleted;
        }
	}
}
