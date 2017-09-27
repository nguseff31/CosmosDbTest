using CosmosDbTest.DAL;
using CosmosDbTest.DAL.Repository;
using CosmosDbTest.Model;
using Microsoft.Azure.Documents.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmosDbTest
{
	class Program
	{
		static void Main(string[] args)
		{
			var client = GetClient();
			InitDb(client).Wait();
			InsertData(client).Wait();
			TryToGetData(client);
			Console.ReadLine();
		}

		static void TryToGetData(DocumentClient client)
		{
			var repository = new TagGroupRepository(client);
			var tagGroups = repository.GetAll(p => true);
			foreach(var tagGroup in tagGroups)
			{
				tagGroup.LogTagGroup();
			}
		}

		static async Task InsertData(DocumentClient client)
		{
			var repository = new TagGroupRepository(client);

			var simpleTagGroup = new TagGroup
			{
				Name = "ImageTypes",
				TagValues = new List<TagValue>()
				{
					new TagValue { Name = "x-ray" }, new TagValue { Name = "picture" }
				}
			};

			var contentOutlines = new TagGroup
			{
				Name = "ContentOutlines",
				TagValues = new List<TagValue>
				{
					new TagValueTreeNode
					{
						Name = "1", Children = new List<TagValueTreeNode>
						{
							new TagValueTreeNode { Name = "1.1" },
							new TagValueTreeNode { Name = "1.2", Children = new List<TagValueTreeNode> {
								new TagValueTreeNode { Name = "1.2.1" }
							} },
							new TagValueTreeNode { Name = "1.3" }
						}
					}
				}
			};

			await repository.Upsert(simpleTagGroup);
			await repository.Upsert(contentOutlines);
		}

		static async Task InitDb(DocumentClient documentClient)
		{
			var service = new DbInitService(documentClient);
			await service.Init();
		}

		static DocumentClient GetClient()
		{
			string endpoint = ConfigurationManager.AppSettings["DbUrl"];
			string token = ConfigurationManager.AppSettings["DbToken"];
			return new DocumentClient(new Uri(endpoint), token, GetSettings());
		}

		static JsonSerializerSettings GetSettings()
		{
			return new JsonSerializerSettings
			{
				TypeNameHandling = TypeNameHandling.None //you can use this settings instead of custom converter, but it adds $type property to objects 
			};
		}
	}
}
