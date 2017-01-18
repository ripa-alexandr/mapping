using System;
using System.Linq.Expressions;

namespace Mapping.Core.Api
{
	public interface IMap<TSource, TDestination> where TDestination : new()
	{
		TDestination Map (TSource source);

		IMap<TSource, TDestination> Ignore<TMember> (Expression<Func<TDestination, TMember>> expr);

		IMap<TSource, TDestination> ForMember<TMember> (Expression<Func<TDestination, TMember>> itemFunc, Func<TSource, TMember> convertFunc);
	}
}
