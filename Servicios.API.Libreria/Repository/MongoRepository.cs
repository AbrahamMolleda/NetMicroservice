using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Servicios.API.Libreria.Core;
using Servicios.API.Libreria.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Servicios.API.Libreria.Repository
{
    public class MongoRepository<T> : IMongoRepository<T> where T : IDocument
    {
        private readonly IMongoCollection<T> _collection;
        public MongoRepository(IOptions<MongoSettings> options)
        {
            var client = new MongoClient(options.Value.ConnectionString);
            var db = client.GetDatabase(options.Value.Database);
            _collection = db.GetCollection<T>(GetCollectionName(typeof(T)));
        }

        private protected string GetCollectionName(Type documentType)
        {
            return ((BsonCollectionAttribute)documentType.GetCustomAttributes(typeof(BsonCollectionAttribute), true).FirstOrDefault()).CollectionName;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _collection.Find(p => true).ToListAsync();
        }

        public async Task<T> GetById(string id)
        {
            var filter = Builders<T>.Filter.Eq(doc => doc.Id, id);
            return await _collection.Find(filter).SingleOrDefaultAsync();
        }

        public async Task InsertDocument(T document)
        {
            await _collection.InsertOneAsync(document);
        }

        public async Task UpdateDocument(T document)
        {
            var filter = Builders<T>.Filter.Eq(doc => doc.Id, document.Id);
            await _collection.FindOneAndReplaceAsync(filter, document);
        }

        public async Task DeleteById(string id)
        {
            var filter = Builders<T>.Filter.Eq(doc => doc.Id, id);
            await _collection.FindOneAndDeleteAsync(filter);
        }
    }
}
