using GestionITM.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GestionITM.Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Cada DbSet representa una tabla en la base de datos
        public DbSet<Estudiante> Estudiantes { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Curso> Cursos { get; set; }
        public DbSet<Matricula> Matriculas { get; set; }

        // Nueva tabla para el módulo de Profesores
        public DbSet<Profesor> Profesores { get; set; }
    }
}
