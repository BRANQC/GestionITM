using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionITM.Domain.Dtos
{
    // DTO de SALIDA: lo que la API devuelve al cliente.
    // Nota: FechaContratacion NO se expone (requisito del taller).
    public class ProfesorDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Especialidad { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}
