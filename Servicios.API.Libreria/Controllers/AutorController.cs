using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Servicios.API.Libreria.Core.Entities;
using Servicios.API.Libreria.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Servicios.API.Libreria.Controllers
{
    [Route("api/libreria/[controller]")]
    [ApiController]
    public class AutorController : ControllerBase
    {
        private readonly IMongoRepository<AutorEntity> _autorRepository;
        public AutorController(IMongoRepository<AutorEntity> autorRepository)
        {
            _autorRepository = autorRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AutorEntity>>> Get()
        {
            return Ok(await _autorRepository.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AutorEntity>> GetAutorByID(string id)
        {
            return Ok(await _autorRepository.GetById(id));
        }

        [HttpPost]
        public async Task CreateAutor(AutorEntity autorEntity)
        {
            await _autorRepository.InsertDocument(autorEntity);
        }

        [HttpPut("{id}")]
        public async Task UpdateAutor(string id, AutorEntity autorEntity)
        {
            autorEntity.Id = id;
            await _autorRepository.UpdateDocument(autorEntity);
        }

        [HttpDelete("{id}")]
        public async Task DeleteAutor(string id)
        {
            await _autorRepository.DeleteById(id);
        }
    }
}
