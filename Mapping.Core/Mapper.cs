using System;
using System.Collections.Generic;
using Mapping.Core.Extensions;

namespace Mapping.Core
{
	public sealed class Mapper
	{
		#region Singleton

		private static readonly Object syncObj = new Object();
		private static Mapper instance;
		private readonly Dictionary<string, Map> mappings;

		public static Mapper Instance
		{
			get
			{
				if (instance == null)
				{
					lock (syncObj)
					{
						if (instance == null)
						{
							instance = new Mapper();
						}
					}
				}

				return instance;
			}
		}

		private Mapper ()
		{
			mappings = new Dictionary<string, Map>();
		}

		#endregion

		public static void CreateMap<TSource, TDestination> ()
		{
			var sourceType = typeof (TSource);
			var destinationType = typeof (TDestination);
			string key = string.Concat(sourceType.FullName, destinationType.FullName);

			Instance.mappings.Add(key, new Map(sourceType, destinationType));
		}

		public static TDestination Map<TSource, TDestination> (TSource source) where TDestination : new() 
		{
			string key = string.Concat(typeof(TSource).FullName, typeof(TDestination).FullName);

			if (!Instance.mappings.ContainsKey(key))
			{
				throw new InvalidOperationException("Mapping does not exist. Call Initialize with appropriate configuration.");
			}

			TDestination destination = new TDestination();

			foreach (var mapItem in Instance.mappings[key].MapItems)
			{
				var sourceValue = mapItem.Source.GetValue(source);

				mapItem.Destination.SetValue(destination, sourceValue);
			}

			return destination;
		}
	}
}
