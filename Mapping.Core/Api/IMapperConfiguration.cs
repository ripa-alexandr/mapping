using System.Collections.Generic;

namespace Mapping.Core.Api
{
	public interface IMapperConfiguration
	{
		Dictionary<string, object> Initialize ();

		void CreateMap<TSource, TDestination> () where TDestination : new();
	}
}
