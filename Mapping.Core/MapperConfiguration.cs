using System.Collections.Generic;
using Mapping.Core.Api;
using Mapping.Core.Maps;

namespace Mapping.Core
{
	public class MapperConfiguration : IMapperConfiguration
	{
		private readonly Dictionary<string, object> mappings;

		public MapperConfiguration()
		{
			mappings = new Dictionary<string, object>();
		}
		
		private string GenerateKey<TSource, TDestination>()
		{
			return string.Concat(typeof(TSource).FullName, typeof(TDestination).FullName);
		}

		#region IMapperConfiguration implementation

		public IDictionary<string, object> Initialize()
		{
			foreach (var mapping in mappings)
			{
				((IMapInitialize)mapping.Value).Initialize();
			}

			return mappings;
		}

		public IMap<TSource, TDestination> CreateMap<TSource, TDestination>() where TDestination : new()
		{
			var key = GenerateKey<TSource, TDestination>();
			var map = new ReflectionMap<TSource, TDestination>();

			mappings.Add(key, map);

			return map;
		}

		#endregion
	}
}
