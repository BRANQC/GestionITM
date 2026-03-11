using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionITM.Domain.Dtos
{
    public class EstudianteCreateDto
    {
        // Este es el que suaremos para recibir los Datos
        public string Nombre{ get; set; } = string.Empty;
        public string Correo { get; set; } = string.Empty;
    }
}
