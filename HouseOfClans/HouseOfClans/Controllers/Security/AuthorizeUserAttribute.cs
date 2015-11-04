using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using HouseOfClans.Managers;
using HouseOfClans.Enums;

namespace HouseOfClans.Controllers.Security
{
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
	public class AuthorizeUserAttribute : AuthorizeAttribute, IAuthorizationFilter
	{
		private const string _aspRoleAdmin = "Admin";
		private bool _adminOnly = false;

		public string[] RequiredPermission { get; set; }

		public AuthorizeUserAttribute(bool adminOnly) : this(new object[] { adminOnly })
		{
		}

		public AuthorizeUserAttribute(ClanRole clanUserRole) : this(new object[] { clanUserRole })
		{
		}

		public AuthorizeUserAttribute(params object[] clanUserRole)
		{
			if (clanUserRole.Any(p => p.GetType().BaseType != typeof(Enum)) && clanUserRole.Any(p => p.GetType() != typeof(bool)))
				throw new ArgumentException("RequiredPermission");

			if (clanUserRole.Any(p => p.GetType() == typeof(Boolean)))
			{
				_adminOnly = (bool)clanUserRole.First();
			}
			else
			{
				this.RequiredPermission = clanUserRole.Select(p => Enum.GetName(p.GetType(), p)).ToArray();
			}
		}

        override public void OnAuthorization(AuthorizationContext filterContext)
		{
			if (HttpContext.Current.User.Identity.IsAuthenticated)
			{
				bool authorized = false;

				if (_adminOnly && HttpContext.Current.User.IsInRole(_aspRoleAdmin))
				{
					authorized = true;
				}
				else if (this.RequiredPermission != null && this.RequiredPermission.Any() && this.RequiredPermission.Length > 0)
				{
					ClanRole userRole = filterContext.HttpContext.GetCurrentLoginClanRole();

					foreach (var role in this.RequiredPermission)
						//TODO //vv can we get the roles from AspNetRoles? so we can check it here, instead of being hardcoded
						if (HttpContext.Current.User.IsInRole(_aspRoleAdmin) || role == userRole.ToString())
							authorized = true;
				}
				
				//	//TODO //vv filterContext.Result = redirect to some custom unauthorize page
				if (!authorized)
				{
					var url = new UrlHelper(filterContext.RequestContext);
					var logonUrl = url.Action("Http", "Error", new { Id = 401, Area = "" });
					filterContext.Result = new RedirectResult(logonUrl);
				}
			}
			else
			{
				filterContext.Result = new HttpUnauthorizedResult();
			}
		}
	}
}