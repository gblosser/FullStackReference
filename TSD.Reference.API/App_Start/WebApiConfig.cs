using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Http.Cors;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;
using TSD.Reference.API.Filters;

namespace TSD.Reference.API
{
	public static class WebApiConfig
	{
		public static void Register(HttpConfiguration config)
		{
			// Enable Cross Origin Resource Sharing
			var cors = new EnableCorsAttribute("http://localhost:56225", "*", "*");
			config.EnableCors(cors);

			// Web API configuration and services
			// Configure Web API to use only bearer token authentication.
			config.SuppressDefaultHostAuthentication();
			config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));
			config.Filters.Add(new BogusIdentityFilter());
			config.Filters.Add(new APIExceptionFilterAttribute());

			// Web API routes
			config.MapHttpAttributeRoutes();

			config.Routes.MapHttpRoute(
				name: "DefaultApi",
				routeTemplate: "api/{controller}/{id}",
				defaults: new { id = RouteParameter.Optional }
			);

			var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
			jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
		}
	}
}
