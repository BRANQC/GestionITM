using Microsoft.AspNetCore.Mvc;
using GestionITM.Domain.Interfaces;
using GestionITM.Domain.Dtos;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;

namespace GestionITM.API.Controllers
{
    [Authorize] //Este es nuestro candado: nadie entra a este controlador sin un token valido
    [Route("api/[controller]")]
    [ApiController]
    public class EstudianteController : ControllerBase
    {
        private readonly IEstudianteService _service;

        public EstudianteController(IEstudianteService service)
        {
            _service = service;
        }

        // GET: api/estudiante
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EstudianteDto>>> Get()
        {
            var estudiantesDto = await _service.ObtenerTodosLosEstudiantesAsync();
            return Ok(estudiantesDto);
        }

        // GET: api/estudiante/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<EstudianteDto>> Get(int id)
        {
            // Si no existe, el servicio lanza NotFoundException → Middleware responde 404
            var estudianteDto = await _service.ObtenerPorIdAsync(id);

            if (estudianteDto == null) { 
                return NotFound(new {message = $"Estudiante con ID {id} no se pudo encontrar"});
            }
            return Ok(estudianteDto);
        }

        // POST: api/estudiante
        [HttpPost]
        public async Task<ActionResult<EstudianteDto>> Post([FromBody] EstudianteCreateDto estudianteCreateDto)
        {
            // Si el correo no es institucional, el servicio lanza BadRequestException → Middleware responde 400
            var resultado = await _service.RegistrarEstudianteAsync(estudianteCreateDto);
            return CreatedAtAction(nameof(Get), new { id = resultado.Id }, resultado);
        }
    }
}