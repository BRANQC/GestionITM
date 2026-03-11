using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestionITM.Domain.Entities;
using GestionITM.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GestionITM.Infrastructure.Repositories
{
    public class EstudianteRepository : IEstudianteRepository
    {
        //  Constructor
        private readonly ApplicationDbContext _context;

        // Inyectamos el DbContext aquí para acceder a la base de datos
        public EstudianteRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Estudiante>> ObtenerTodoAsync()
        {
            // Usamos ToListAsync para obtener todos los estudiantes de la base de datos
            return await _context.Estudiantes.ToListAsync();
        }
        public async Task<Estudiante?> ObtenerPorIdAsync(int id)
        {
            // Usamos FindAsync para buscar un estudiante por su ID
            return await _context.Estudiantes.FindAsync(id);
        }
        public async Task AgregarAsync(Estudiante estudiante)
        {
            // Agregamos el nuevo estudiante al DbSet y guardamos los cambios en la base de datos
            await _context.Estudiantes.AddAsync(estudiante);
            await _context.SaveChangesAsync(); // Persiste los cambios en SQL
        }
    }
}
