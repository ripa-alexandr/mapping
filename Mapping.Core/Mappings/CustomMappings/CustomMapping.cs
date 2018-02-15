using System;
using Mapping.Core.Api;

namespace Mapping.Core.Mappings.CustomMappings
{
	internal class CustomMapping<TSource, TDestination> : IMapping where TDestination : new()
	{
		private readonly Func<TSource, TDestination> func;

		internal CustomMapping(Func<TSource, TDestination> func)
		{
			this.func = func;
		}

		#region IMapping

		public object Map(object source)
		{
			return func((TSource)source);
		}

		#endregion
	}
}
