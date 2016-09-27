using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using Mapping.Core.Extensions;

namespace Mapping.Core
{
	public class Map
	{
		public Type SourceType { get; private set; }
		public Type DestinationType { get; private set; }
		public ICollection<MapItem> MapItems { get; private set; }
		
		public Map (Type sourceType, Type destinationType)
		{
			this.SourceType = sourceType;
			this.DestinationType = destinationType;
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
	}
}
