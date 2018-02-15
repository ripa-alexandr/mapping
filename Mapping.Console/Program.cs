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
				Age = "39",
				SecondAge = new C() { Age = 99 }
			};
			
			var config = new MapperConfiguration();
			config.CreateMap<A, B>()
				.Ignore(i => i.FirstName)
				.ForMember(i => i.Age, i => int.Parse(i.Age));

			config.CreateCustomMap<B, A>(i => new A
			{
				FirstName = i.FirstName,
				LastName = i.LastName
			});

			config.CreateMap<C, D>();

			Mapper.Initialize(config);
			var b1 = Mapper.Map<A, B>(a);
			var b2 = Mapper.Map<B, A>(b1);

			System.Console.ReadLine();
		}
	}
}
