using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Microsoft.Practices.Unity;
using Owin;
using TSD.Reference.API.App_Start;
using TSD.Reference.API.Providers;
using TSD.Reference.Core.Services;
using TSD.Reference.Core.Services.Interfaces;

namespace TSD.Reference.API
{
    public partial class Startup
    {
		public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }

		public static string PublicClientId { get; private set; }


		public void ConfigureAuth(IAppBuilder app)
		{
			var aContainer = UnityConfig.GetConfiguredContainer();

			app.UseOAuthAuthorizationServer(new OAuthAuthorizationServerOptions
			{
				// the endpoint path which will be consumed via HTTP. e.g. http://website[:port]/api/token
				// it should begin with a leading slash so /token is also ok but I have kept it /api/token to match my default route in Web-API
				TokenEndpointPath = new PathString("/api/token"),

				//Provider is a class which inherits from OAuthAuthorizationServerProvider.Will be covered next.
				Provider = new OAuthProvider(aContainer.Resolve<UserService>()),
				// mark true if you are not on https channel
				AllowInsecureHttp = true,
				AccessTokenExpireTimeSpan = TimeSpan.FromHours(1)
			});
			// indicate our intent to use bearer authentication
			app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions
			{
				AuthenticationType = "Bearer",
				AuthenticationMode = Microsoft.Owin.Security.AuthenticationMode.Active,
			});

		}
	}
}
