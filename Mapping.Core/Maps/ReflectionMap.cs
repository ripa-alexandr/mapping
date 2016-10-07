using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using Mapping.Core.Api;
using Mapping.Core.Extensions;

namespace Mapping.Core.Maps
{
	public class ReflectionMap<TSource, TDestination> : IMap<TSource, TDestination> where TDestination : new()
	{
		public Type SourceType { get; private set; }
		public Type DestinationType { get; private set; }
		public ICollection<MapItem> MapItems { get; private set; }
		
		public ReflectionMap ()
		{
			this.SourceType = typeof(TSource);
			this.DestinationType = typeof(TDestination);
			this.MapItems = new Collection<MapItem>();

			this.CreateMap();
		}

		private void CreateMap ()
		{
			var bindingFlags = BindingFlags.Public | BindingFlags.Instance;

			var sources = SourceType.GetProperties(bindingFlags)
				.Where(i => i.CanRead)
				.Cast<MemberInfo>()
				.Concat(SourceType.GetFields(bindingFlags));

			var destinations = DestinationType.GetProperties(bindingFlags)
				.Where(i => i.CanWrite)
				.Cast<MemberInfo>()
				.Concat(DestinationType.GetFields(bindingFlags));

			foreach (MemberInfo destination in destinations)
			{
				var source = sources.FirstOrDefault(i => i.Name.Equals(destination.Name));

				// TODO: add logic for check convertible
				if (source != null && destination.GetValueType() == source.GetValueType())
				{
					MapItems.Add(new MapItem(source, destination));
				}
			}
		}

		#region IMap implementation

		public TDestination Map(TSource source)
		{
			TDestination destination = new TDestination();

			foreach (var mapItem in MapItems)
			{
				var sourceValue = mapItem.Source.GetValue(source);

				mapItem.Destination.SetValue(destination, sourceValue);
			}

			return destination;
		}

		#endregion
	}
}
