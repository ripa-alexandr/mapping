using System.Reflection;

namespace Mapping.Core.MapperConfigurations
{
	public class MapperConfigurationItem
	{
		public MemberInfo Source { get; private set; }
		public MemberInfo Destination { get; private set; }

		public MapperConfigurationItem (MemberInfo source, MemberInfo destination)
		{
			this.Source = source;
			this.Destination = destination;
		}
	}
}
