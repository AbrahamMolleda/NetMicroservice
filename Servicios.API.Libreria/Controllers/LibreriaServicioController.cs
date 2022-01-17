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
    public class LibreriaServicioController : ControllerBase
    {
        private readonly IAutorRepository _autorRepository;
        private readonly IMongoRepository<AutorEntity> _autorRepositoryGenerico;
        private readonly IMongoRepository<EmpleadoEntity> _empleadoRepository;
        public LibreriaServicioController(IAutorRepository autorRepository, IMongoRepository<AutorEntity> autorRepositoryGenerico, IMongoRepository<EmpleadoEntity> empleadoRepository)
        {
            _autorRepository = autorRepository;
            _autorRepositoryGenerico = autorRepositoryGenerico;
            _empleadoRepository = empleadoRepository;
        }

        [HttpGet("autores")]
        public async Task<ActionResult<IEnumerable<EmpleadoEntity>>> GetAutores()
        {
            var empleados = await _empleadoRepository.GetAllAsync();
            return Ok(empleados);
        }

    }
}
