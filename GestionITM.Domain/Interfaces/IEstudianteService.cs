using GestionITM.Domain.Dtos;

namespace GestionITM.Domain.Interfaces
{
    public interface IEstudianteService
    {
        Task<IEnumerable<EstudianteDto>> ObtenerTodosLosEstudiantesAsync();
        Task<EstudianteDto> RegistrarEstudianteAsync(EstudianteCreateDto estudianteDto);
        Task<EstudianteDto?> ObtenerPorIdAsync(int id);
    }
}