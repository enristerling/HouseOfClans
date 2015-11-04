using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HouseOfClans.Controllers.Security;
using HouseOfClans.Models;
using HouseOfClans.Managers;
using HouseOfClans.Enums;

namespace HouseOfClans.Controllers
{
    [Authorize]
    public class EventLogController : Controller
    {
        // GET: EventLog
		[AuthorizeUser(adminOnly: true)]
        public ActionResult Index()
        {
			ClanUser clanUser = HttpContext.GetCurrentLoginInformation();
			List<ActionLog> logs = ActionLogManager.GetAll().OrderByDescending(p => p.addedOn).ToList();

            return View(logs);
        }
    }
}