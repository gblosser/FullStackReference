using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;
using TSD.Reference.Core.Services;
using TSD.Reference.Core.Services.Interfaces;

namespace TSD.Reference.API.Filters
{
	public sealed class POCAuthorizeAttribute : AuthorizeAttribute  
	{
		private readonly ILoginService _loginService;

		public POCAuthorizeAttribute()
		{
			_loginService = new LoginService();
		}

		public override void OnAuthorization(AuthorizationContext filterContext)
		{
			if (!CheckAuthToken(filterContext.HttpContext.Request.Headers))
				HandleUnauthorizedRequest(filterContext);

				base.OnAuthorization(filterContext);
		}

		protected override bool AuthorizeCore(HttpContextBase httpContext)
		{
			if (!CheckAuthToken(httpContext.Request.Headers))
				return false;

			return base.AuthorizeCore(httpContext);
		}

		/// <summary>
		/// Checks for authorization header and then validates the token in the header
		/// </summary>
		/// <param name="theHeaders"></param>
		/// <returns></returns>
		private bool CheckAuthToken(HttpHeaders theHeaders)
		{
			var aValues = Enumerable.Empty<string>();

			theHeaders.TryGetValues("Authorization", out aValues);

			if (aValues == null)
				return false;

			var aValueList = aValues as IList<string> ?? aValues.ToList();
			if (!aValueList.Any())
				return false;

			var aValue = aValueList.FirstOrDefault();
			if (string.IsNullOrEmpty(aValue))
				return false;

			return _loginService.ValidateToken(aValue);
		}

		/// <summary>
		/// Checks for authorization header and then validates the token in the header
		/// </summary>
		/// <param name="theHeaders"></param>
		/// <returns></returns>
		private bool CheckAuthToken(NameValueCollection theHeaders)
		{
			//var aValues = Enumerable.Empty<string>();

			var aHeader = theHeaders["Authorization"];

			//theHeaders.TryGetValues("Authorization", out aValues);

			if (aHeader == null)
				return false;

			//var aValueList = aValues as IList<string> ?? aValues.ToList();
			//if (!aValueList.Any())
			//	return false;

			//var aValue = aValueList.FirstOrDefault();
			//if (string.IsNullOrEmpty(aValue))
			//	return false;

			return _loginService.ValidateToken(aHeader);
		}
	}

}

