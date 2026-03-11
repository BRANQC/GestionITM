using System.ComponentModel.DataAnnotations;

namespace GestionITM.Domain.Entities
{
    public class Matricula
    {
        public int Id { get; set; }

        // Relación con Estudiante
        public int EstudianteId { get; set; }

        // Relación con Curso
        public int CursoId { get; set; }

        [Required]
        [MaxLength(20)]
        public string Periodo { get; set; } = string.Empty; // Ej: 2026-1

        // Estado de la matrícula (por ejemplo: Activa, Cancelada, Finalizada)
        [MaxLength(20)]
        public string Estado { get; set; } = string.Empty;
    }
}
