﻿using System;
using System.Collections.Generic;
using Mapping.Core.Api;
using Mapping.Core.Mappings.CustomMappings;
using Mapping.Core.Mappings.ReflectionMappings;

namespace Mapping.Core
{
	public class MapperConfiguration
	{
		private readonly Dictionary<string, IMapping> mappings;

		public MapperConfiguration()
		{
			mappings = new Dictionary<string, IMapping>();
		}

		public IConfigurationMapping<TSource, TDestination> CreateMap<TSource, TDestination>() where TDestination : new()
		{
			var key = GenerateKey<TSource, TDestination>();
			var mapping = new ReflectionMapping<TSource, TDestination>(mappings);

			mappings.Add(key, mapping);

			return mapping;
		}

		public void CreateCustomMap<TSource, TDestination>(Func<TSource, TDestination> func) where TDestination : new()
		{
			var key = GenerateKey<TSource, TDestination>();
			var mapping = new CustomMapping<TSource, TDestination>(func);

			mappings.Add(key, mapping);
		}

		internal IDictionary<string, IMapping> Initialize()
		{
			foreach (var mapping in mappings)
			{
				(mapping.Value as IInitializeMapping)?.Initialize();
			}

			return mappings;
		}

		private string GenerateKey<TSource, TDestination>()
		{
			return string.Concat(typeof(TSource).FullName, typeof(TDestination).FullName);
		}
	}
}
