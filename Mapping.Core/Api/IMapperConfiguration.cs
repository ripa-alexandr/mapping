using System.Collections.Generic;

namespace Mapping.Core.Api
{
	public interface IMapperConfiguration
	{
		IDictionary<string, object> Initialize ();

		IMap<TSource, TDestination> CreateMap<TSource, TDestination> () where TDestination : new();
	}
}
