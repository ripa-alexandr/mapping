using System;
using System.Collections.Generic;
using Mapping.Core.Api;

namespace Mapping.Core
{
	public static class Mapper
	{
		private static IDictionary<string, object> mappings;

		public static void Initialize(IMapperConfiguration configuration)
		{
			mappings = configuration.Initialize();
		}

		public static TDestination Map<TSource, TDestination> (TSource source) where TDestination : new() 
		{
			var key = GenerateKey<TSource, TDestination>();

			if (!mappings.ContainsKey(key))
			{
				throw new InvalidOperationException("Mapping does not exist. Call Initialize with appropriate configuration.");
			}

			var mapping = (IMap<TSource, TDestination>)mappings[key];
			var destination = mapping.Map(source);

			return destination;
		}

		private static string GenerateKey<TSource, TDestination> ()
		{
			return string.Concat(typeof(TSource).FullName, typeof(TDestination).FullName);
		}
	}
}
