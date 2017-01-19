using System.Reflection;
using Mapping.Core.Extensions;

namespace Mapping.Core.Mappings
{
	public class MappingItem
	{
		private readonly MemberInfo sourceInfo;
		private readonly MemberInfo destinationInfo;

		public MappingItem (MemberInfo sourceInfo, MemberInfo destinationInfo)
		{
			this.sourceInfo = sourceInfo;
			this.destinationInfo = destinationInfo;
		}

		public void FillDestination (object source, object destination)
		{
			var sourceValue = sourceInfo.GetValue(source);

			destinationInfo.SetValue(destination, sourceValue);
		}
	}
}
