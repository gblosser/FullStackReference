using System.Diagnostics.CodeAnalysis;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Owin;

[assembly: OwinStartup(typeof(TSD.Reference.API.Startup))]

namespace TSD.Reference.API
{
	[ExcludeFromCodeCoverage]
	public partial class Startup
	{
		public void Configuration(IAppBuilder app)
		{
			ConfigureAuth(app);
		}
	}
}
