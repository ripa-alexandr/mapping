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
	internal class ReflectionMapping<TSource, TDestination> : IInitializeMapping, IConfigurationMapping<TSource, TDestination>, IMapping where TDestination : new()
	{
		private readonly Dictionary<string, IMapping> mappings;
		private readonly Type sourceType;
		private readonly Type destinationType;
		private readonly ICollection<BaseMappingItem> mappingItems;
		private readonly ICollection<string> ignoreItems;
		private readonly IDictionary<string, Func<object, object>> converters; 

		internal ReflectionMapping (Dictionary<string, IMapping> mappings)
		{
			this.mappings = mappings;
			this.sourceType = typeof(TSource);
			this.destinationType = typeof(TDestination);
			this.mappingItems = new Collection<BaseMappingItem>();
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
				mappingItems.Add(new ConvertMappingItem(destination, converters[destination.Name]));
				return;
			}

			var sourceItemType = source.GetValueType();
			var destinationItemType = destination.GetValueType();

			if (sourceItemType == destinationItemType)
			{
				mappingItems.Add(new ReflectionMappingItem(source, destination));
				return;
			}

			var key = string.Concat(sourceItemType.FullName, destinationItemType.FullName);

			if (mappings.ContainsKey(key))
			{
				mappingItems.Add(new CustomMappingItem(source, destination, (IMapping)mappings[key]));
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

		private string GetMemberName<TMember> (Expression<Func<TDestination, TMember>> item)
		{
			return ((MemberExpression)item.Body).Member.Name; ;
		}

		#endregion

		#region IMapping

		public object Map (object source)
		{
			TDestination destination = new TDestination();

			foreach (var mapItem in mappingItems)
			{
				mapItem.FillDestination(source, destination);
			}

			return destination;
		}

		#endregion
	}
}
