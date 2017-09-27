using CosmosDbTest.DAL;
using System.Collections.Generic;

namespace CosmosDbTest.Model
{
	public class TagGroup : Entity
	{
		public string Name { get; set; }

		public List<TagValue> TagValues { get; set; }
	}
}
