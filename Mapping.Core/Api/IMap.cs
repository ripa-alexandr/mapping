namespace Mapping.Core.Api
{
	public interface IMap
	{
		TDestination Map<TSource, TDestination> (TSource source) where TDestination : new();
	}
}
