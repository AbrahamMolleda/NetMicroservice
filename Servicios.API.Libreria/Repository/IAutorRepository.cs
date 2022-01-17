using Servicios.API.Libreria.Core.Entities;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Servicios.API.Libreria.Repository
{
    public interface IAutorRepository
    {
        Task<IEnumerable<Autor>> GetAutores();

    }
}
