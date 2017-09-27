using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmosDbTest.DAL
{
	public class DbInitService
	{
		private readonly DocumentClient _documentClient;

		public DbInitService(DocumentClient documentClient)
		{
			_documentClient = documentClient;
		}

		public async Task Init()
		{
			await _documentClient.CreateDatabaseIfNotExistsAsync(new Database { Id = DbNames.DbName });
			await _documentClient.CreateDocumentCollectionIfNotExistsAsync(UriFactory.CreateDatabaseUri(DbNames.DbName), new DocumentCollection { Id = DbNames.TagGroupCollection });
			await _documentClient.CreateDocumentCollectionIfNotExistsAsync(UriFactory.CreateDatabaseUri(DbNames.DbName), new DocumentCollection { Id = DbNames.ItemMetadataCollection });
		}

		public async Task Remove()
		{
			await _documentClient.DeleteDatabaseAsync(UriFactory.CreateDatabaseUri(DbNames.DbName));
		}
	}
}
