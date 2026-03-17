using Microsoft.AspNetCore.Mvc;
using GestionITM.Domain.Interfaces;
using GestionITM.Domain.Dtos;

namespace GestionITM.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CursoController : ControllerBase
    {
        // Nivel 5: el controlador depende del SERVICIO, nunca del repositorio
        private readonly ICursoService _service;

        public CursoController(ICursoService service)
        {
            _service = service;
        }

        // GET: api/curso
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CursoDto>>> GetCursos()
        {
            var cursos = await _service.ObtenerTodosLosCursosAsync();
            return Ok(cursos);
        }

        // GET: api/curso/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<CursoDto>> GetCurso(int id)
        {
            var curso = await _service.ObtenerPorIdAsync(id);
            if (curso == null)
                return NotFound(new { message = $"Curso con ID {id} no encontrado." });

            return Ok(curso);
        }

        // POST: api/curso
        [HttpPost]
        public async Task<ActionResult<CursoDto>> PostCurso([FromBody] CursoCreateDto cursoCreateDto)
        {
            var resultado = await _service.RegistrarCursoAsync(cursoCreateDto);
            return CreatedAtAction(nameof(GetCurso), new { id = resultado.Id }, resultado);
        }
    }
}
