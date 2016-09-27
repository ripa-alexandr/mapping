using System;
using System.Collections.Generic;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mapping.Core;

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

			Mapper.CreateMap<A, B>();
			var b = Mapper.Map<A, B>(a);

			System.Console.ReadLine();
		}
	}
}
