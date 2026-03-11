using GestionITM.Domain.Entities;
using GestionITM.Domain.Interfaces;
using GestionITM.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace GestionITM.Infrastructure.Repositories
{
    public class CursoRepository : ICursoRepository
    {
        private readonly ApplicationDbContext _context;

        public CursoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Curso>> ObtenerTodoAsync()
        {
            return await _context.Cursos.ToListAsync();
        }

        public async Task<Curso?> ObtenerPorIdAsync(int id)
        {
            return await _context.Cursos.FindAsync(id);
        }

        public async Task AgregarAsync(Curso curso)
        {
            await _context.Cursos.AddAsync(curso);
            await _context.SaveChangesAsync();
        }
    }
}
