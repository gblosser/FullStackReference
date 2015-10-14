using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace TSD.Reference.API.Extensions
{
	public static class ControllerExtensions
	{
		public static int GetCustomerId(this ApiController theController)
		{
			int aCustomerId;
			try
			{
				aCustomerId = Convert.ToInt32(theController.ActionContext.Request.Properties["custid"]);
			}
			catch (NullReferenceException theException)
			{
				throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest)
				{
					ReasonPhrase = $"Request does not contain valid information to identify the customer: {theException.Message}"
				});
			}
			catch (InvalidOperationException theInvalidOperationException)
			{
				throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest)
				{
					ReasonPhrase =
						$"Request does not contain valid information to identify the customer: {theInvalidOperationException.Message}"
				});
			}
			return aCustomerId;
		}
	}
}
