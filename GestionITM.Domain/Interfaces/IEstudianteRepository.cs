using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestionITM.Domain.Entities;

namespace GestionITM.Domain.Interfaces
{
    public interface IEstudianteRepository
    {
        //Definimos las operaciones asíncronas (Tasks) para manejar estudiantes
        Task<IEnumerable<Estudiante>> ObtenerTodoAsync(); // Obtener todos los estudiantes
        Task<Estudiante?> ObtenerPorIdAsync(int id); // Obtener un estudiante por su ID
        Task AgregarAsync(Estudiante estudiante); // Agregar un nuevo estudiante
    }
}
