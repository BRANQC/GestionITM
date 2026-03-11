using System.ComponentModel.DataAnnotations;

namespace GestionITM.Domain.Entities
{
    public class Estudiante
    {
        // EF Core reconoce "Id" automáticamente como llave primaria
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Nombre { get; set; } = string.Empty;

        [EmailAddress]
        [MaxLength(200)]
        public string Correo { get; set; } = string.Empty;

        public DateTime FechaInscripcion { get; set; } = DateTime.Now;

        // Nuevo campo para el número de teléfono del estudiante
        // Práctica Migración EFCore
        [MaxLength(20)]
        public string Telefono { get; set; } = string.Empty;
     

    }
}
