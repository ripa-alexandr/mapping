using System.Reflection;
using Mapping.Core.Api;
using Mapping.Core.Extensions;

namespace Mapping.Core.Mappings.ReflectionMappings
{
	internal class CustomMappingItem : BaseMappingItem
	{
		private readonly MemberInfo sourceInfo;
		private readonly MemberInfo destinationInfo;
		private readonly IMapping mapping;

		internal CustomMappingItem (MemberInfo sourceInfo, MemberInfo destinationInfo, IMapping mapping)
		{
			this.sourceInfo = sourceInfo;
			this.destinationInfo = destinationInfo;
			this.mapping = mapping;
		}

		internal override void FillDestination (object source, object destination)
		{
			var sourceValue = sourceInfo.GetValue(source);
			var mappedSourceValue = mapping.Map(sourceValue);

			destinationInfo.SetValue(destination, mappedSourceValue);
		}
	}
}
