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
    public class ProfesorRepository : IProfesorRepository
    {
        private readonly ApplicationDbContext _context;

        public ProfesorRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Profesor>> ObtenerTodoAsync()
        {
            return await _context.Profesores.ToListAsync();
        }

        public async Task<Profesor?> ObtenerPorIdAsync(int id)
        {
            return await _context.Profesores.FindAsync(id);
        }

        public async Task AgregarAsync(Profesor profesor)
        {
            await _context.Profesores.AddAsync(profesor);
            await _context.SaveChangesAsync();
        }

      
        public async Task<bool> ExisteEmailAsync(string email)
        {
            return await _context.Profesores
                .AnyAsync(p => p.Email.ToLower() == email.ToLower());
        }
    }
}
