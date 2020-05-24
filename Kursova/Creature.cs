using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursova
{
	abstract class Creature
	{
		protected int ValueInNumber;

		public int Value
		{
			get
			{
				return ValueInNumber;
			}
		}
	}
}
