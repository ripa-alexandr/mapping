using System;
using Mapping.Core.Api;

namespace Mapping.Core.Maps
{
	public class CustomMap : IMap
	{
		private readonly object func;

		public CustomMap(object func)
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
