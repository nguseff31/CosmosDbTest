using CosmosDbTest.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Documents.Client;

namespace CosmosDbTest.DAL.Repository
{
	public class TagGroupRepository : BaseRepository<TagGroup>
	{
		public TagGroupRepository(DocumentClient documentClient) : base(documentClient)
		{
		}

		protected override string CollectionName => DbNames.TagGroupCollection;
	}
}
