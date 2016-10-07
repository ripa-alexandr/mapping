using System;
using Mapping.Core.Api;

namespace Mapping.Core.Maps
{
	public class CustomMap<TSource, TDestination> : IMap<TSource, TDestination> where TDestination : new()
	{
		private readonly Func<TSource, TDestination> func;

		public CustomMap(Func<TSource, TDestination> func)
		{
			this.func = func;
		}

		#region IMap implementation

		public TDestination Map(TSource source)
		{
			return func(source);
		}

		#endregion
	}
}
