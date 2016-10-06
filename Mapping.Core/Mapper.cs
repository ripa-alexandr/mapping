using System;
using System.Collections.Generic;
using Mapping.Core.Api;
using Mapping.Core.Extensions;
using Mapping.Core.MapperConfigurations;

namespace Mapping.Core
{
	public sealed class Mapper
	{
		#region Singleton

		private static readonly Object syncObj = new Object();
		private static Mapper instance;
		private readonly Dictionary<string, IMapperConfiguration> mappings;

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
			mappings = new Dictionary<string, IMapperConfiguration>();
		}

		#endregion

		public static void CreateMap<TSource, TDestination> ()
		{
			var sourceType = typeof (TSource);
			var destinationType = typeof (TDestination);
			string key = string.Concat(sourceType.FullName, destinationType.FullName);

			Instance.mappings.Add(key, new ReflectionMapperConfiguration(sourceType, destinationType));
		}

		public static void ConvertUsing<TSource, TDestination> (Func<TSource, TDestination> func)
		{
			string key = string.Concat(typeof(TSource).FullName, typeof(TDestination).FullName);

			Instance.mappings.Add(key, new CustomMapperConfiguration(func));
		}

		public static TDestination Map<TSource, TDestination> (TSource source) where TDestination : new() 
		{
			string key = string.Concat(typeof(TSource).FullName, typeof(TDestination).FullName);

			if (!Instance.mappings.ContainsKey(key))
			{
				throw new InvalidOperationException("Mapping does not exist. Call Initialize with appropriate configuration.");
			}

			var destination = Instance.mappings[key].Map<TSource, TDestination>(source);

			return destination;
		}
	}
}
