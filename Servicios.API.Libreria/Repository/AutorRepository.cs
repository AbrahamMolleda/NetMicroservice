using MongoDB.Driver;
using Servicios.API.Libreria.Core.ContextMongoDB;
using Servicios.API.Libreria.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Servicios.API.Libreria.Repository
{
    public class AutorRepository : IAutorRepository
    {
        private readonly IAutorContext _context;
        public AutorRepository(IAutorContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Autor>> GetAutores()
        {
            return await _context.Autores.Find(p => true).ToListAsync();
        }
    }
}
