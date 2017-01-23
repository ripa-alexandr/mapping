using System.Reflection;
using Mapping.Core.Extensions;

namespace Mapping.Core.Mappings.ReflectionMappings
{
	internal class ReflectionMappingItem : BaseMappingItem
	{
		private readonly MemberInfo sourceInfo;
		private readonly MemberInfo destinationInfo;

		internal ReflectionMappingItem (MemberInfo sourceInfo, MemberInfo destinationInfo)
		{
			this.sourceInfo = sourceInfo;
			this.destinationInfo = destinationInfo;
		}

		internal override void FillDestination (object source, object destination)
		{
			var sourceValue = sourceInfo.GetValue(source);

			destinationInfo.SetValue(destination, sourceValue);
		}
	}
}
