using System;
using System.Linq.Expressions;

namespace Mapping.Core.Api
{
	public interface IMappingConfiguration<TSource, TDestination> where TDestination : new()
	{
		IMappingConfiguration<TSource, TDestination> Ignore<TMember>(Expression<Func<TDestination, TMember>> item);

		IMappingConfiguration<TSource, TDestination> ForMember<TMember>(Expression<Func<TDestination, TMember>> item, Func<TSource, TMember> convert);
	}
}
