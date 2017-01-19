using System;
using Mapping.Core.Api;

namespace Mapping.Core.Mappings
{
	internal class CustomMapping<TSource, TDestination> : IMapping<TSource, TDestination> where TDestination : new()
	{
		private readonly Func<TSource, TDestination> func;

		internal CustomMapping(Func<TSource, TDestination> func)
		{
			this.func = func;
		}

		#region IMapping

		public TDestination Map(TSource source)
		{
			return func(source);
		}

		#endregion
	}
}
