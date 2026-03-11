using GestionITM.Domain.Dtos;

namespace GestionITM.Domain.Interfaces
{
    public interface IEstudianteService
    {
        Task<IEnumerable<EstudianteDto>> ObtenerTodosLosEstudiantesAsync();
        Task<bool> RegistrarEstudianteAsync(EstudianteCreateDto estudianteDto);
        Task<EstudianteDto?> ObtenerPorIdAsync(int id);
    }
}