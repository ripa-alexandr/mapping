using System;
using System.Reflection;
using Mapping.Core.Extensions;

namespace Mapping.Core.Mappings.ReflectionMappings
{
	internal class CustomMappingItem : BaseMappingItem
	{
		private readonly MemberInfo sourceInfo;
		private readonly MemberInfo destinationInfo;
		private readonly Func<object, object> converter;

		internal CustomMappingItem(MemberInfo sourceInfo, MemberInfo destinationInfo, Func<object, object> converter)
		{
			this.sourceInfo = sourceInfo;
			this.destinationInfo = destinationInfo;
			this.converter = converter;
		}

		internal override void FillDestination (object source, object destination)
		{
			var originalSourceValue = sourceInfo.GetValue(source);
			var convertedSourceValue = converter(originalSourceValue);

			destinationInfo.SetValue(destination, convertedSourceValue);
		}
	}
}
