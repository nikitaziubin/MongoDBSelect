using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDBSelect
{
	internal class Data
	{
		public double temperature { get; set; }
		public double pressure { get; set; }
		public double humidity { get; set; }
		public DateTime DateTime { get; set; }
		public int roomId { get; set; }
		Random rand = new Random();
	}
}
