using CosmosDbTest.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmosDbTest
{
	public static class LogHelper
	{
		public static void LogTagGroup(this TagGroup tagGroup)
		{
			Console.WriteLine(tagGroup.Name);
			foreach(var tagValue in tagGroup.TagValues)
			{
				tagValue.LogTagValue();
			}
		}

		public static void LogTagValue<T>(this T tagValue) where T : TagValue
		{
			var treeNode = tagValue as TagValueTreeNode;
			if (treeNode != null)
			{
				LogTagValueTreeNode(treeNode, 0);
			}
			else
			{
				Console.WriteLine('\t' + tagValue.Name);
			}
			
		}

		public static void LogTagValueTreeNode(this TagValueTreeNode node, int level = 0)
		{
			Console.WriteLine('\t' + new String('-', level) + node.Name);
			if(node.Children != null && node.Children.Any())
			{
				foreach(var child in node.Children) { LogTagValueTreeNode(child, level + 1); }
			}
		}
	}
}
