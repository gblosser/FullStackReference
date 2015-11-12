using System.Diagnostics.CodeAnalysis;
using System.Web.Http;
using Microsoft.Practices.Unity.WebApi;
using TSD.Reference.API.Cache;
using WebApi.OutputCache.Core.Cache;
using WebApi.OutputCache.V2;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(TSD.Reference.API.App_Start.UnityWebApiActivator), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethod(typeof(TSD.Reference.API.App_Start.UnityWebApiActivator), "Shutdown")]

namespace TSD.Reference.API.App_Start
{
	/// <summary>Provides the bootstrapping for integrating Unity with WebApi when it is hosted in ASP.NET</summary>
	[ExcludeFromCodeCoverage]
	public static class UnityWebApiActivator
	{
		/// <summary>Integrates Unity when the application starts.</summary>
		public static void Start()
		{
			// Use UnityHierarchicalDependencyResolver if you want to use a new child container for each IHttpController resolution.
			// var resolver = new UnityHierarchicalDependencyResolver(UnityConfig.GetConfiguredContainer());
			var resolver = new UnityDependencyResolver(UnityConfig.GetConfiguredContainer());

			GlobalConfiguration.Configuration.DependencyResolver = resolver;

			// added for WebApi.OutputCache cache output provider
			GlobalConfiguration.Configuration.CacheOutputConfiguration()
				.RegisterCacheOutputProvider(() => new MemoryCacheDefault());
			//GlobalConfiguration.Configuration.CacheOutputConfiguration()
			//	.RegisterCacheOutputProvider(() => new RedisApiOutputCache());
			GlobalConfiguration.Configuration.CacheOutputConfiguration()
				.RegisterDefaultCacheKeyGeneratorProvider(() => new DefaultCacheKeyGenerator());
		}

		/// <summary>Disposes the Unity container when the application is shut down.</summary>
		public static void Shutdown()
		{
			var container = UnityConfig.GetConfiguredContainer();
			container.Dispose();
		}
	}
}
