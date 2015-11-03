using System.Web.Mvc;
using TSD.Reference.API.Filters;

namespace TSD.Reference.API
{
	public class FilterConfig
	{
		public static void RegisterGlobalFilters(GlobalFilterCollection filters)
		{
			filters.Add(new HandleErrorAttribute());
		}
	}
}
