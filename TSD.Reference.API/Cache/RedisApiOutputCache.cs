using System;
using System.Collections.Generic;
using System.Configuration;
using StackExchange.Redis;
using WebApi.OutputCache.Core.Cache;

namespace TSD.Reference.API.Cache
{
	/// <summary>
	/// An implementation of WebApi.OutputCache.Core.Cache.IApiOutputCache that uses Redis as the cache.
	/// For more information on Asp.NET Web API CacheOutput see https://github.com/filipw/AspNetWebApi-OutputCache
	/// </summary>
	public class RedisApiOutputCache : IApiOutputCache
	{
		private static readonly Lazy<ConnectionMultiplexer> LazyConnection =
			new Lazy<ConnectionMultiplexer>(
				() =>
				{
					return
						ConnectionMultiplexer.Connect(
							ConfigurationManager.ConnectionStrings["RedisCache"].ConnectionString);
				});

		/// <summary>
		/// Lazily gets a singleton instance of ConnectionMultiplexer.
		/// This can hang around for the lifetime of the application.
		/// </summary>
		private static ConnectionMultiplexer Connection
		{
			get { return LazyConnection.Value; }
		}

		public void RemoveStartsWith(string key)
		{
			Remove(key);
		}

		public T Get<T>(string key) where T : class
		{
			var redisValue = Connection.GetDatabase().StringGet(key);
			var result = Convert.ChangeType(redisValue, typeof(T)) as T;

			return result;
		}

		// IFN - this overload is obsolete.
		public object Get(string key)
		{
			throw new NotSupportedException();
		}

		public void Remove(string key)
		{
			Connection.GetDatabase().KeyDelete(key);
		}

		public bool Contains(string key)
		{
			var result = Connection.GetDatabase().KeyExists(key);
			return result;
		}

		public void Add(string key, object o, DateTimeOffset expiration, string dependsOnKey = null)
		{
			var cache = Connection.GetDatabase();
			var expiry = expiration - DateTime.Now;

			// From inspection of the Web API Output Cache source code, it always caches objects that are either string or byte[].
			RedisValue redisValue = RedisValue.Null;
			if (o is byte[])
			{
				redisValue = (byte[])o;
			}
			else if (o is string)
			{
				redisValue = (string)o;
			}

			if (redisValue != default(RedisValue))
			{
				cache.StringSet(key, redisValue, expiry);
			}
		}

		// IFN - this member appears to only be used as a test hook, is not invoked from the production code at present.
		public IEnumerable<string> AllKeys
		{
			get { return new string[] { }; }
		}
	}
}
