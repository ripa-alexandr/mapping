namespace Mapping.Core.Api
{
	public interface IMap<TSource, TDestination> where TDestination : new()
	{
		TDestination Map (TSource source);
	}
}
