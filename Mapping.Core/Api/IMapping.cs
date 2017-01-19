namespace Mapping.Core.Api
{
	internal interface IMapping<TSource, TDestination> where TDestination : new()
	{
		TDestination Map (TSource source);
	}
}
