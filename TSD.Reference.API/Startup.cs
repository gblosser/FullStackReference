﻿using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(TSD.Reference.API.Startup))]

namespace TSD.Reference.API
{
	public partial class Startup
	{
		public void Configuration(IAppBuilder app)
		{
			ConfigureAuth(app);
		}
	}
}