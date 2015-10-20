using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Filters;
using log4net;
using TSD.Reference.Core.Exceptions;

namespace TSD.Reference.API.Filters
{
	/// <summary>
	/// Custom Exception Filter Attribute for my API.  This is where error handling behavior is determined.
	/// To utilize this throughout your API add this line to the Register(...) method in WebApiConfig.cs:
	/// filters.Add(new APIExceptionFilterAttribute());
	/// </summary>
	public sealed class APIExceptionFilterAttribute : ExceptionFilterAttribute
	{
		// get a log4net logger
		// log4net is avaiable via NuGet
		private static readonly ILog _logger = LogManager.GetLogger("errorlog");

		public override void OnException(HttpActionExecutedContext actionExecutedContext)
		{
			// decide how to handle the exception
			HandleException(actionExecutedContext.Exception);

			base.OnException(actionExecutedContext);
		}

		public async override Task OnExceptionAsync(HttpActionExecutedContext actionExecutedContext, CancellationToken cancellationToken)
		{
			// decide how to handle the exception
			HandleException(actionExecutedContext.Exception);

			await base.OnExceptionAsync(actionExecutedContext, cancellationToken);
		}

		private static void HandleException(Exception theException)
		{
			// cast as InvalidPermissionsException
			var aInvalidPermissionsException = theException as InvalidPermissionsException;
			if (aInvalidPermissionsException != null)
			{
				// log the error and notify the user
				_logger.Error("InvalidPermissionsException", aInvalidPermissionsException);
				throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Forbidden) {Content = new StringContent(aInvalidPermissionsException.Message)});
			}

			// case as InvalidRentalAgreementException
			var aInvalidRentalAgreementException = theException as InvalidRentalAgreementException;
			if (aInvalidRentalAgreementException != null)
			{
				// log the error and notify the user
				_logger.Error("InvalidRentalAgreementException", aInvalidRentalAgreementException);
				throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest) { Content = new StringContent(aInvalidRentalAgreementException.Message) });
			}
			
			// handle generic Exception type here
			_logger.Error("General Exception", theException);
			throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError) {Content=new StringContent("Internal server error")});
		}
	}
}
