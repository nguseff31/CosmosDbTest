using CosmosDbTest.DAL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmosDbTest.Model
{
	[JsonConverter(typeof(TagValueConverter))]
	public class TagValue
	{
		public string Name { get; set; }
	}
}
