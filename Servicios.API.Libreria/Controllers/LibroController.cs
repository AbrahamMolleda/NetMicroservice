using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Servicios.API.Libreria.Core.Entities;
using Servicios.API.Libreria.Repository;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Servicios.API.Libreria.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibroController : ControllerBase
    {
        private readonly IMongoRepository<LibroEntity> _libroRepository;
        public LibroController(IMongoRepository<LibroEntity> libroRepository)
        {
            _libroRepository = libroRepository;
        }

        [HttpPost]
        public async Task CreateLibro(LibroEntity libro)
        {
            await _libroRepository.InsertDocument(libro);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LibroEntity>>> GetAllLibros()
        {
            return Ok(await _libroRepository.GetAllAsync());
        }

        [HttpPost("pagination")]
        public async Task<ActionResult<PaginationEntity<LibroEntity>>> Pagination(PaginationEntity<LibroEntity> pagination)
        {
            var resultados = await _libroRepository.PaginationByFilter(pagination);
            return Ok(resultados);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LibroEntity>> GetLibroById(string id)
        {
            var libro = await _libroRepository.GetById(id);
            return Ok(libro);
        }
    }
}
