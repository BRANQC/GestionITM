using Microsoft.AspNetCore.Mvc;
using GestionITM.Domain.Interfaces;
using GestionITM.Domain.Dtos;
//using Microsoft.AspNetCore.Authentication;
//using Microsoft.AspNetCore.Authorization;


namespace GestionITM.API.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProfesorController : ControllerBase
    {
        private readonly IProfesorService _service;

        public ProfesorController(IProfesorService service)
        {
            _service = service;
        }

        // GET: api/profesor
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProfesorDto>>> Get()
        {
            var profesoresDto = await _service.ObtenerTodosLosProfesoresAsync();
            return Ok(profesoresDto);
        }

        // GET: api/profesor/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProfesorDto>> Get(int id)
        {
            // Si no existe lanza NotFoundException → Middleware responde 404
            var profesor = await _service.ObtenerPorIdAsync(id);
            return Ok(profesor);
        }

        // POST: api/profesor
        [HttpPost]
        public async Task<ActionResult<ProfesorDto>> Post([FromBody] ProfesorCreateDto profesorCreateDto)
        {
            var resultado = await _service.RegistrarProfesorAsync(profesorCreateDto);
            return CreatedAtAction(nameof(Get), new { id = resultado.Id }, resultado);
        }
    }
}