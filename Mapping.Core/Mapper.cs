using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Mapping.Core
{
	public static class Mapper
	{
		public static TDestination Map<TSource, TDestination> (TSource source) where TDestination : new() 
		{
			Type sourceType = typeof (TSource);
			Type destinationType = typeof (TDestination);

			TDestination destination = new TDestination();

			foreach (PropertyInfo destinationProperty in destinationType.GetProperties())
			{
				var sourceProperty = sourceType.GetProperties().FirstOrDefault(i => i.Name.Equals(destinationProperty.Name));

				if (sourceProperty != null && destinationProperty.PropertyType == sourceProperty.PropertyType)
				{
					var sourceValue = sourceProperty.GetValue(source);
					destinationProperty.SetValue(destination, sourceValue);
				}
			}

			return destination;
		}
	}
}
