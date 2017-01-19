using System.Reflection;
using Mapping.Core.Extensions;

namespace Mapping.Core.Mappings
{
	internal class MappingItem
	{
		private readonly MemberInfo sourceInfo;
		private readonly MemberInfo destinationInfo;

		internal MappingItem (MemberInfo sourceInfo, MemberInfo destinationInfo)
		{
			this.sourceInfo = sourceInfo;
			this.destinationInfo = destinationInfo;
		}

		internal void FillDestination (object source, object destination)
		{
			var sourceValue = sourceInfo.GetValue(source);

			destinationInfo.SetValue(destination, sourceValue);
		}
	}
}
