using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Servicios.API.Libreria.Core;
using Servicios.API.Libreria.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

        public async Task<PaginationEntity<T>> PaginationBy(Expression<Func<T, bool>> filterExpression, PaginationEntity<T> paginationEntity)
        {
            var sort = Builders<T>.Sort.Ascending(paginationEntity.Sort);
            if(paginationEntity.SortDirection == "desc")
                sort = Builders<T>.Sort.Descending(paginationEntity.Sort);

            if (string.IsNullOrEmpty(paginationEntity.Filter))
            {
                paginationEntity.Data = await _collection.Find(p => true)
                    .Sort(sort)
                    .Skip((paginationEntity.Page - 1) * paginationEntity.PageSize)
                    .Limit(paginationEntity.PageSize)
                    .ToListAsync();
            }
            else
            {
                paginationEntity.Data = await _collection.Find(filterExpression)
                    .Sort(sort)
                    .Skip((paginationEntity.Page - 1) * paginationEntity.PageSize)
                    .Limit(paginationEntity.PageSize)
                    .ToListAsync();
            }

            long totalDocuments = await _collection.CountDocumentsAsync(FilterDefinition<T>.Empty);
            var totalPages = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(totalDocuments / paginationEntity.PageSize)));
            paginationEntity.PagesQuantity = totalPages;

            return paginationEntity;
        }

        public async Task<PaginationEntity<T>> PaginationByFilter(PaginationEntity<T> paginationEntity)
        {
            var sort = Builders<T>.Sort.Ascending(paginationEntity.Sort);
            if (paginationEntity.SortDirection == "desc")
                sort = Builders<T>.Sort.Descending(paginationEntity.Sort);

            var totalDocuments = 0;
            if (paginationEntity.FilterValue == null)
            {
                paginationEntity.Data = await _collection.Find(p => true)
                    .Sort(sort)
                    .Skip((paginationEntity.Page - 1) * paginationEntity.PageSize)
                    .Limit(paginationEntity.PageSize)
                    .ToListAsync();

                totalDocuments = (await _collection.Find(p => true).ToListAsync()).Count();
            }
            else
            {
                var valueFilter = ".*" + paginationEntity.FilterValue.Valor + ".*";
                var filter = Builders<T>.Filter.Regex(paginationEntity.FilterValue.Propiedad, new MongoDB.Bson.BsonRegularExpression(valueFilter, "i"));
                paginationEntity.Data = await _collection.Find(filter)
                    .Sort(sort)
                    .Skip((paginationEntity.Page - 1) * paginationEntity.PageSize)
                    .Limit(paginationEntity.PageSize)
                    .ToListAsync();
                totalDocuments = (await _collection.Find(filter).ToListAsync()).Count();
            }

            //long totalDocuments = await _collection.CountDocumentsAsync(FilterDefinition<T>.Empty);
            decimal rounded = Math.Ceiling(totalDocuments / Convert.ToDecimal(paginationEntity.PageSize));
            var totalPages = Convert.ToInt32(rounded);
            paginationEntity.PagesQuantity = totalPages;
            paginationEntity.TotalRows = Convert.ToInt32(totalDocuments);

            return paginationEntity;
        }
    }
}
