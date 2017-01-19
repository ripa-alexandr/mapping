using System;
using System.Linq.Expressions;

namespace Mapping.Core.Api
{
	public interface IConfigurationMapping<TSource, TDestination> where TDestination : new()
	{
		IConfigurationMapping<TSource, TDestination> Ignore<TMember>(Expression<Func<TDestination, TMember>> item);

		IConfigurationMapping<TSource, TDestination> ForMember<TMember>(Expression<Func<TDestination, TMember>> item, Func<TSource, TMember> convert);
	}
}
