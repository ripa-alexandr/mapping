using System.Reflection;

namespace Mapping.Core
{
	public class MapItem
	{
		public MemberInfo Source { get; private set; }
		public MemberInfo Destination { get; private set; }

		public MapItem (MemberInfo source, MemberInfo destination)
		{
			this.Source = source;
			this.Destination = destination;
		}
	}
}
