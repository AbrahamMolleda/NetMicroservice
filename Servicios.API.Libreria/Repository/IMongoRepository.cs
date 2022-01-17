using Servicios.API.Libreria.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Servicios.API.Libreria.Repository
{
    public interface IMongoRepository<T> where T : IDocument
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetById(string id);
        Task InsertDocument(T document);
        Task UpdateDocument(T document);
        Task DeleteById(string id);
        Task<PaginationEntity<T>> PaginationBy(
            Expression<Func<T, bool>> filterExpression,
            PaginationEntity<T> paginationEntity
        );
        Task<PaginationEntity<T>> PaginationByFilter(
            PaginationEntity<T> paginationEntity
        );
    }
}
