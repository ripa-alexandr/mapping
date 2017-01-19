using System;
using Mapping.Core.Api;

namespace Mapping.Core.Mappings
{
	public class CustomMapping<TSource, TDestination> : IMapping<TSource, TDestination> where TDestination : new()
	{
		private readonly Func<TSource, TDestination> func;

		public CustomMapping(Func<TSource, TDestination> func)
		{
			this.func = func;
		}

		#region IMapping implementation

		public TDestination Map(TSource source)
		{
			return func(source);
		}

		#endregion
	}
}
