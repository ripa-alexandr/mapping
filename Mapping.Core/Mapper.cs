using System;
using System.Collections.Generic;
using Mapping.Core.Api;
using Mapping.Core.Maps;

namespace Mapping.Core
{
	public static class Mapper
	{
		private static readonly Dictionary<string, object> mappings;

		static Mapper()
		{
			mappings = new Dictionary<string, object>();
		}

		public static void CreateMap<TSource, TDestination> () where TDestination : new()
		{
			var key = GenerateKey<TSource, TDestination>();

			mappings.Add(key, new ReflectionMap<TSource, TDestination>());
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
