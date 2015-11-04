using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using HouseOfClans.Enums;
using HouseOfClans.Managers;
using HouseOfClans.Models;

namespace HouseOfClans.Controllers.Security
{
	public static class CurrentLoginHttpContextBaseExtensions
	{
		private const string _sessionMemberInfo = "CurrentClanMemberInfo";
		private const string _sessionMemberRole = "CurrentClanMemberRole";

		public static ClanRole GetCurrentLoginClanRole(this HttpContextBase context)
		{
			ClanRole clanUserRole = ClanRole.Unknown;

			if (context.Session[_sessionMemberRole] != null)
			{
				clanUserRole = (ClanRole)context.Session[_sessionMemberRole];
			}

			if (clanUserRole == ClanRole.Unknown)
			{
				var clanUser = GetCurrentLoginInformation(context);

				if (clanUser != null && clanUser.id > 0)
				{
					context.Session.Add(_sessionMemberRole, clanUser.userRoleId);
					clanUserRole = (ClanRole)clanUser.userRoleId;
				}
			}

			return clanUserRole;
		}

		public static ClanUser GetCurrentLoginInformation(this HttpContextBase context)
		{
			ClanUser clanUser = new ClanUser();

			if (context.Session[_sessionMemberInfo] != null)
			{
				clanUser = context.Session[_sessionMemberInfo] as ClanUser;
			}
			else
			{
				clanUser = ClanUserManager.SelectByAspNetUserId(context.User.Identity.GetUserId());
				context.Session.Add(_sessionMemberInfo, clanUser);
			}

			return clanUser;
		}
	}
}