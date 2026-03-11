using Microsoft.AspNetCore.Mvc;
using GestionITM.Domain.Interfaces;
using GestionITM.Domain.Entities;

namespace GestionITM.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CursoController : ControllerBase
    {
        private readonly ICursoRepository _repository;

        public CursoController(ICursoRepository repository)
        {
            _repository = repository;
        }

        // GET: api/curso
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Curso>>> GetCursos()
        {
            var cursos = await _repository.ObtenerTodoAsync();
            return Ok(cursos);
        }

        // GET: api/curso/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Curso>> GetCurso(int id)
        {
            var curso = await _repository.ObtenerPorIdAsync(id);
            if (curso == null)
            {
                return NotFound(new { message = $"Curso con ID {id} no encontrado." });
            }
            return Ok(curso);
        }

        // POST: api/curso
        [HttpPost]
        public async Task<ActionResult> PostCurso(Curso curso)
        {
            await _repository.AgregarAsync(curso);
            return CreatedAtAction(nameof(GetCurso), new { id = curso.Id }, curso);
        }
    }
}
