using System;
using System.Linq.Expressions;
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

		public IMap<TSource, TDestination> Ignore<TMember> (Expression<Func<TDestination, TMember>> expr)
		{
			throw new NotImplementedException();
		}

		public IMap<TSource, TDestination> ForMember<TMember> (Expression<Func<TDestination, TMember>> itemFunc, Func<TSource, TMember> convertFunc)
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}
