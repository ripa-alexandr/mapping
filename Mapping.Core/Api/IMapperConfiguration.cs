namespace Mapping.Core.Api
{
	public interface IMapperConfiguration
	{
		TDestination Map<TSource, TDestination> (TSource source) where TDestination : new();
	}
}
