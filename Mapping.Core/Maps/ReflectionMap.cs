using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Mapping.Core.Api;
using Mapping.Core.Extensions;

namespace Mapping.Core.Maps
{
	public class ReflectionMap<TSource, TDestination> : IMapInitialize, IMap<TSource, TDestination> where TDestination : new()
	{
		private readonly Type sourceType;
		private readonly Type destinationType;
		private readonly ICollection<MapItem> mapItems;
		
		public ReflectionMap ()
		{
			this.sourceType = typeof(TSource);
			this.destinationType = typeof(TDestination);
			this.mapItems = new Collection<MapItem>();
		}

		#region IMapInitialize implementation

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
					mapItems.Add(new MapItem(source, destination));
				}
			}
		}

		#endregion

		#region IMap implementation

		public TDestination Map(TSource source)
		{
			TDestination destination = new TDestination();

			foreach (var mapItem in mapItems)
			{
				mapItem.FillDestination(source, destination);
			}

			return destination;
		}

		public IMap<TSource, TDestination> Ignore<TMember>(Expression<Func<TDestination, TMember>> expr)
		{
			//var memberName = GetMemberName(expr);

			//this.CreateMap();
			//ignoreList.Add(memberName);

			return this;
		}

		public IMap<TSource, TDestination> ForMember<TMember>(Expression<Func<TDestination, TMember>> itemFunc, Func<TSource, TMember> convertFunc)
		{
			return this;
		}


		#endregion
	}
}
