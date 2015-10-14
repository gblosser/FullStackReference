using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace TSD.Reference.API.Filters
{
	public class BogusIdentityFilter : ActionFilterAttribute
	{
		public override async Task OnActionExecutingAsync(HttpActionContext actionContext, CancellationToken cancellationToken)
		{
			actionContext.Request.Properties["custid"] = 1;

			await base.OnActionExecutingAsync(actionContext, cancellationToken);
		}
	}
}