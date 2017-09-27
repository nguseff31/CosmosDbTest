using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CosmosDbTest.DAL.Repository
{
	public abstract class BaseRepository<T> where T : class
	{
		private readonly DocumentClient _documentClient;

		protected abstract string CollectionName { get; }

		protected virtual string DbName { get; } = DbNames.DbName;

		protected virtual Uri CollectionUri => UriFactory.CreateDocumentCollectionUri(DbName, CollectionName);

		public BaseRepository(DocumentClient documentClient)
		{
			_documentClient = documentClient;
		}

		public async Task<T> Get(string id)
		{
			return await _documentClient.ReadDocumentAsync<T>(UriFactory.CreateDocumentUri(DbName, CollectionName, id));
		}

		public async Task Upsert(T entity)
		{
			await _documentClient.UpsertDocumentAsync(CollectionUri, entity);
		}
		
		public List<T> GetAll(Expression<Func<T, bool>> predicate)
		{
			return _documentClient.CreateDocumentQuery<T>(CollectionUri).ToList();
		}

		public T FindOne(Expression<Func<T, bool>> predicate)
		{
			return _documentClient.CreateDocumentQuery<T>(CollectionUri).FirstOrDefault(predicate);
		}
	}
}
