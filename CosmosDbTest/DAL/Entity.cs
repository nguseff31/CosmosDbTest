using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmosDbTest.DAL
{

	public abstract class Entity
	{
		[JsonProperty("id")]
		public string Id { get; set; }
	}
}
