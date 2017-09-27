using CosmosDbTest.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmosDbTest.DAL
{
	public class TagValueConverter : JsonConverter
	{
		public override bool CanRead => true;

		public override bool CanConvert(Type objectType)
		{
			return objectType == typeof(TagGroup);
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			var obj = JObject.Load(reader);

			return ReadTagValue(obj);
		}

		private TagValue ReadTagValue(JObject obj)
		{
			var childrenProperty = obj.Properties().FirstOrDefault(p => p.Name == "Children");
			if (childrenProperty == null)
			{
				return new TagValue
				{
					Name = (string)obj["Name"]
				};
			}
			return new TagValueTreeNode
			{
				Children = childrenProperty.Value.ToObject<List<TagValueTreeNode>>(),
				Name = (string)obj["Name"]
			};
		}

		public override bool CanWrite => true;

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var tagValue = (TagValue)value;
			writer.WriteStartObject();
			writer.WritePropertyName("Name");
			serializer.Serialize(writer, tagValue.Name);

			var node = tagValue as TagValueTreeNode;
			if(node != null)
			{
				writer.WritePropertyName("Children");
				serializer.Serialize(writer, node.Children);
			}

			writer.WriteEndObject();
		}
	}
}
