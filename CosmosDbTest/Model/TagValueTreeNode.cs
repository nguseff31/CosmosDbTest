using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmosDbTest.Model
{
	public class TagValueTreeNode : TagValue
	{
		public List<TagValueTreeNode> Children { get; set; }
	}
}
