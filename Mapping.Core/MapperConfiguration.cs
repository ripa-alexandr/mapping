﻿using System.Collections.Generic;
using Mapping.Core.Api;
using Mapping.Core.Mappings;

namespace Mapping.Core
{
	public class MapperConfiguration
	{
		private readonly Dictionary<string, object> mappings;

		public MapperConfiguration()
		{
			mappings = new Dictionary<string, object>();
		}

		public IMappingConfiguration<TSource, TDestination> CreateMap<TSource, TDestination>() where TDestination : new()
		{
			var key = GenerateKey<TSource, TDestination>();
			var mapping = new ReflectionMapping<TSource, TDestination>();

			mappings.Add(key, mapping);

			return mapping;
		}

		internal IDictionary<string, object> Initialize()
		{
			foreach (var mapping in mappings)
			{
				((IMappingInitialize)mapping.Value).Initialize();
			}

			return mappings;
		}

		private string GenerateKey<TSource, TDestination>()
		{
			return string.Concat(typeof(TSource).FullName, typeof(TDestination).FullName);
		}
	}
}
