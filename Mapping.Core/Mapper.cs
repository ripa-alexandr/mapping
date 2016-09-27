using System;
using System.Collections.Generic;
using Mapping.Core.Extensions;

namespace Mapping.Core
{
	public static class Mapper
	{
		private static readonly Dictionary<string, Map> mappings;

		static Mapper ()
		{
			mappings = new Dictionary<string, Map>();
		}

		public static void CreateMap<TSource, TDestination> ()
		{
			string key = string.Concat(typeof(TSource).FullName, typeof(TDestination).FullName);

			mappings.Add(key, new Map(typeof(TSource), typeof(TDestination)));
		}

		public static TDestination Map<TSource, TDestination> (TSource source) where TDestination : new() 
		{
			string key = string.Concat(typeof(TSource).FullName, typeof(TDestination).FullName);

			if (!mappings.ContainsKey(key))
			{
				throw new InvalidOperationException("Mapping does not exist. Call Initialize with appropriate configuration.");
			}

			TDestination destination = new TDestination();

			foreach (var mapItem in mappings[key].MapItems)
			{
				var sourceValue = mapItem.Source.GetValue(source);

				mapItem.Destination.SetValue(destination, sourceValue);
			}

			return destination;
		}
	}
}
