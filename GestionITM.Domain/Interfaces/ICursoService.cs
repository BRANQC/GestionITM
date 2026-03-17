using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestionITM.Domain.Dtos;

namespace GestionITM.Domain.Interfaces
{
    public interface ICursoService
    {
        Task<IEnumerable<CursoDto>> ObtenerTodosLosCursosAsync();
        Task<CursoDto?> ObtenerPorIdAsync(int id);
        Task<CursoDto> RegistrarCursoAsync(CursoCreateDto cursoDto);
    }
}