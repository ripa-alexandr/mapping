using System;
using System.Collections.Generic;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Mapping.Core;
using Mapping.Core.Mappings;

namespace Mapping.Console
{
	class Program
	{
		static void Main (string[] args)
		{
			var a = new A
			{
				FirstName = "Gregory",
				LastName = "House",
				Age = 39
			};
			
			var config = new MapperConfiguration();
			config.CreateMap<A, B>()
				.Ignore(i => i.Age)
				.ForMember(i => i.MiddleName, i => i.Age.ToString());

			Mapper.Initialize(config);
			var b = Mapper.Map<A, B>(a);

			System.Console.ReadLine();
		}
	}
}
