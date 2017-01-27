using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Mapping.Core.Api;
using Mapping.Core.Extensions;

namespace Mapping.Core.Mappings.ReflectionMappings
{
	internal class ReflectionMapping<TSource, TDestination> : IInitializeMapping, IConfigurationMapping<TSource, TDestination>, IMapping<TSource, TDestination> where TDestination : new()
	{
		private readonly Type sourceType;
		private readonly Type destinationType;
		private readonly ICollection<BaseMappingItem> mappings;
		private readonly ICollection<string> ignoreItems;
		private readonly IDictionary<string, Func<object, object>> converters; 

		internal ReflectionMapping ()
		{
			this.sourceType = typeof(TSource);
			this.destinationType = typeof(TDestination);
			this.mappings = new Collection<BaseMappingItem>();
			this.ignoreItems = new Collection<string>();
			this.converters = new Dictionary<string, Func<object, object>>();
		}

		#region IInitializeMapping

		public void Initialize ()
		{
			var bindingFlags = BindingFlags.Public | BindingFlags.Instance;

			var sources = sourceType.GetProperties(bindingFlags)
				.Where(i => i.CanRead)
				.Cast<MemberInfo>()
				.Concat(sourceType.GetFields(bindingFlags));

			var destinations = destinationType.GetProperties(bindingFlags)
				.Where(i => i.CanWrite)
				.Cast<MemberInfo>()
				.Concat(destinationType.GetFields(bindingFlags));

			foreach (MemberInfo destination in destinations)
			{
				var source = sources.FirstOrDefault(i => i.Name.Equals(destination.Name));

				AddMapping(source, destination);
			}
		}

		private void AddMapping (MemberInfo source, MemberInfo destination)
		{
			// check on null and ignore
			if (source == null || ignoreItems.Contains(destination.Name))
			{
				return;
			}

			if (converters.ContainsKey(destination.Name))
			{
				mappings.Add(new CustomMappingItem(destination, converters[destination.Name]));
			}
			else if (destination.GetValueType() == source.GetValueType())
			{
				mappings.Add(new ReflectionMappingItem(source, destination));
			}
		}

		#endregion

		#region IConfigurationMapping

		public IConfigurationMapping<TSource, TDestination> Ignore<TMember> (Expression<Func<TDestination, TMember>> item)
		{
			var memberName = GetMemberName(item);

			ignoreItems.Add(memberName);

			return this;
		}

		public IConfigurationMapping<TSource, TDestination> ForMember<TMember> (Expression<Func<TDestination, TMember>> item, Func<TSource, TMember> converter)
		{
			var memberName = GetMemberName(item);

			converters.Add(memberName, i => converter((TSource)i));

			return this;
		}

		private string GetMemberName<TMamber> (Expression<Func<TDestination, TMamber>> item)
		{
			return ((MemberExpression)item.Body).Member.Name; ;
		}

		#endregion

		#region IMapping

		public TDestination Map(TSource source)
		{
			TDestination destination = new TDestination();

			foreach (var mapItem in mappings)
			{
				mapItem.FillDestination(source, destination);
			}

			return destination;
		}

		#endregion
	}
}
