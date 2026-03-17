using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestionITM.Domain.Entities;

namespace GestionITM.Domain.Interfaces
{
    public interface IProfesorRepository
    {
        Task<IEnumerable<Profesor>> ObtenerTodoAsync();
        Task<Profesor?> ObtenerPorIdAsync(int id);
        Task AgregarAsync(Profesor profesor);

        // BONUS Nivel 5
        Task<bool> ExisteEmailAsync(string email);
    }
}
