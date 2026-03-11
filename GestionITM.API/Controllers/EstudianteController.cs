using Microsoft.AspNetCore.Mvc;
using GestionITM.Domain.Interfaces;
using GestionITM.Domain.Dtos;


namespace GestionITM.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstudianteController : ControllerBase
    {
        // 1. Solo dependemos de la Interfaz del Servicio
        private readonly IEstudianteService _service;

        // 2. El constructor ahora es mucho más limpio
        public EstudianteController(IEstudianteService service)
        {
            _service = service;
        }

        // GET: api/estudiante
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EstudianteDto>>> Get()
        {
            // El servicio ya nos devuelve los DTOs mapeados
            var estudiantesDto = await _service.ObtenerTodosLosEstudiantesAsync();
            return Ok(estudiantesDto);
        }

        // GET: api/estudiante/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<EstudianteDto>> Get(int id)
        {
            // Nota: Aquí podrías agregar lógica en el Servicio para manejar el Null
            // o mapearlo aquí si el servicio devuelve la entidad (pero mejor en el servicio)
            var estudianteDto = await _service.ObtenerPorIdAsync(id);

            if (estudianteDto == null)
            {
                return NotFound(new { message = $"Estudiante con ID {id} no encontrado." });
            }

            return Ok(estudianteDto);
        }

        // POST: api/estudiante
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] EstudianteCreateDto estudianteCreateDto)
        {
            // 3. El servicio valida la lógica (como el correo @itm) y guarda
            var resultado = await _service.RegistrarEstudianteAsync(estudianteCreateDto);

            if (!resultado)
            {
                return BadRequest("No se pudo registrar. Verifique que el correo sea institucional (@correo.itm.edu.co).");
            }

            // En un flujo Nivel 5 real, el servicio podría devolver el objeto creado 
            // para usar CreatedAtAction, pero por ahora lo mantenemos simple:
            return Ok(new { message = "Estudiante registrado con éxito en el sistema del ITM." });
        }
    }
}