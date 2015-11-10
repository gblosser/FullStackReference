using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin.Security.OAuth;
using TSD.Reference.Core.Services.Interfaces;

namespace TSD.Reference.API.Providers
{
	public class OAuthProvider : OAuthAuthorizationServerProvider
	{
		IUserService _userService;

		public OAuthProvider(IUserService theUserService)
		{
			_userService = theUserService;
		}

		public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
		{
			string clientId, clientSecret;
			//gets the clientid and client secret from request body.If you are providing it in header as basic authentication then use
			//context.TryGetBasicCredentials
			context.TryGetFormCredentials(out clientId, out clientSecret);
			// validate clientid and clientsecret. You can omit validating client secret if none is provided in your request (as in sample client request above)
			if (clientId == "poc")
				context.Validated();
			else
				context.Rejected();
			return Task.FromResult(0);

		}

		/// <summary>
		/// Request will send credentials as form data
		/// username
		/// password
		/// grant_type: password
		/// customer_number
		/// client_id: poc
		/// </summary>
		/// <param name="context"></param>
		/// <returns></returns>
		public override Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
		{
			//we check if the passed username and password are correct.
			var aUserName = context.UserName;
			var aPassword = context.Password;

			if (string.IsNullOrEmpty(aUserName))
			{
				context.SetError("Incorrect Credentials");
				context.Rejected();
			}

			if (_userService.VerifyPasswordAsync(aUserName, aPassword).Result)
			{
				var aUser = _userService.GetUserByEmail(aUserName);

				// populate ClaimsIdentity wit user information
				ClaimsIdentity ci = new ClaimsIdentity("ci");

				var aClaims = new List<Claim>
				{
					new Claim("CustomerId", aUser.CustomerId.ToString()),
					new Claim("UserId", aUser.Id.ToString()),
					new Claim(ClaimTypes.GivenName, aUser.FirstName),
					new Claim(ClaimTypes.Surname, aUser.LastName),
				};
				ci.AddClaims(aClaims);
				if(aUser.IsEmployee)
					ci.AddClaim(new Claim(ClaimTypes.Role, "Employee"));


				//this indicates that user is valid one and can be issued a token.
				//it has several overloads ,you can take what fits for you.I have used it with ClaimsIdentity
				context.Validated(ci);
			}
			else
			{
				context.SetError("Incorrect Credentials");
				context.Rejected();
			}
			return Task.FromResult(0);
		}

		public override Task TokenEndpoint(OAuthTokenEndpointContext context)
		{
			// This is called last in pipeline when token is about to be issued successfully.
			foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
			{
				context.AdditionalResponseParameters.Add(property.Key, property.Value);
			}

			return Task.FromResult<object>(null);

		}
	}
}
