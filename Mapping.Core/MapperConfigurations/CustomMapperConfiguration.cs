using System;
using Mapping.Core.Api;

namespace Mapping.Core.MapperConfigurations
{
	public class CustomMapperConfiguration : IMapperConfiguration
	{
		private readonly object func;

		public CustomMapperConfiguration(object func)
		{
			this.func = func;
		}

		public TDestination Map<TSource, TDestination>(TSource source) where TDestination : new()
		{
			Func<TSource, TDestination> mappingFunction = (Func<TSource, TDestination>)func;

			return mappingFunction(source);
		}
	}
}
