using MongoDB.Driver;
using Servicios.API.Libreria.Core.Entities;

namespace Servicios.API.Libreria.Core.ContextMongoDB
{
    public interface IAutorContext
    {
        IMongoCollection<Autor> Autores { get; }
    }
}
