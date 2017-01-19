﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Mapping.Core.Api;
using Mapping.Core.Extensions;

namespace Mapping.Core.Mappings
{
	internal class ReflectionMapping<TSource, TDestination> : IInitializeMapping, IConfigurationMapping<TSource, TDestination>, IMapping<TSource, TDestination> where TDestination : new()
	{
		private readonly Type sourceType;
		private readonly Type destinationType;
		private readonly ICollection<MappingItem> mappingItems;

		internal ReflectionMapping ()
		{
			this.sourceType = typeof(TSource);
			this.destinationType = typeof(TDestination);
			this.mappingItems = new Collection<MappingItem>();
		}

		#region IMappingInitialize

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

				// TODO: add logic for check convertible
				if (source != null && destination.GetValueType() == source.GetValueType())
				{
					mappingItems.Add(new MappingItem(source, destination));
				}
			}
		}

		#endregion

		#region IMappingConfiguration

		public IConfigurationMapping<TSource, TDestination> Ignore<TMember>(Expression<Func<TDestination, TMember>> expr)
		{
			return this;
		}

		public IConfigurationMapping<TSource, TDestination> ForMember<TMember>(Expression<Func<TDestination, TMember>> itemFunc, Func<TSource, TMember> convertFunc)
		{
			return this;
		}

		#endregion

		#region IMapping

		public TDestination Map(TSource source)
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
