using System;
using System.Reflection;
using Mapping.Core.Extensions;

namespace Mapping.Core.Mappings.ReflectionMappings
{
	internal class ConvertMappingItem : BaseMappingItem
	{
		private readonly MemberInfo destinationInfo;
		private readonly Func<object, object> converter;

		internal ConvertMappingItem(MemberInfo destinationInfo, Func<object, object> converter)
		{
			this.destinationInfo = destinationInfo;
			this.converter = converter;
		}

		internal override void FillDestination (object source, object destination)
		{
			var sourceValue = converter(source);

			destinationInfo.SetValue(destination, sourceValue);
		}
	}
}
