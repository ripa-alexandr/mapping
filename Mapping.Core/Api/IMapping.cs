namespace Mapping.Core.Api
{
	public interface IMapping<TSource, TDestination> where TDestination : new()
	{
		TDestination Map (TSource source);
	}
}
