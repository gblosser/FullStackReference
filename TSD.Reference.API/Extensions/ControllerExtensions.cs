using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;

namespace TSD.Reference.API.Extensions
{
	public static class ControllerExtensions
	{
		/// <summary>
		/// Gets the customer id from the ClaimsPrincipal sent in the api request
		/// </summary>
		/// <param name="theController">The controller that is being extended by this method</param>
		/// <returns>The customer id</returns>
		public static int GetCustomerId(this ApiController theController)
		{
			int aCustomerId;
			try
			{
				var aId = theController.User;

				var aCustomerClaim = ((ClaimsPrincipal)aId).Claims.FirstOrDefault(aItem => aItem.Type == "CustomerId");
				if (aCustomerClaim == null)
					throw new HttpResponseException(HttpStatusCode.Forbidden);

				var aCustomerValueString = aCustomerClaim.Value;

				if(!int.TryParse(aCustomerValueString, out aCustomerId))
					throw new HttpResponseException(HttpStatusCode.Forbidden);

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

		/// <summary>
		/// Gets the user id from the ClaimsPrincipal sent in the api request
		/// </summary>
		/// <param name="theController"></param>
		/// <returns></returns>
		public static int GetUserId(this ApiController theController)
		{
			int aUserId;
			try
			{
				var aId = theController.User;

				var aCustomerClaim = ((ClaimsPrincipal)aId).Claims.FirstOrDefault(aItem => aItem.Type == "UserId");
				if (aCustomerClaim == null)
					throw new HttpResponseException(HttpStatusCode.Forbidden);

				var aCustomerValueString = aCustomerClaim.Value;

				if (!int.TryParse(aCustomerValueString, out aUserId))
					throw new HttpResponseException(HttpStatusCode.Forbidden);

			}
			catch (NullReferenceException theException)
			{
				throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest)
				{
					ReasonPhrase = $"Request does not contain valid information to identify the user: {theException.Message}"
				});
			}
			catch (InvalidOperationException theInvalidOperationException)
			{
				throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest)
				{
					ReasonPhrase =
						$"Request does not contain valid information to identify the user: {theInvalidOperationException.Message}"
				});
			}
			return aUserId;
		}
	}
}
