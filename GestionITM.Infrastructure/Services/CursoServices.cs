using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using GestionITM.Domain.Dtos;
using GestionITM.Domain.Entities;
using GestionITM.Domain.Exceptions;
using GestionITM.Domain.Interfaces;

namespace GestionITM.Infrastructure.Services
{
    public class CursoService : ICursoService
    {
        private readonly ICursoRepository _repository;
        private readonly IMapper _mapper;

        public CursoService(ICursoRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CursoDto>> ObtenerTodosLosCursosAsync()
        {
            var cursos = await _repository.ObtenerTodoAsync();
            return _mapper.Map<IEnumerable<CursoDto>>(cursos);
        }

        public async Task<CursoDto?> ObtenerPorIdAsync(int id)
        {
            var curso = await _repository.ObtenerPorIdAsync(id);
            if (curso == null)
                throw new NotFoundException($"Curso con ID {id} no encontrado.");

            return _mapper.Map<CursoDto>(curso);
        }

        public async Task<CursoDto> RegistrarCursoAsync(CursoCreateDto cursoDto)
        {
            // REGLA 1: Código obligatorio → 400
            if (string.IsNullOrWhiteSpace(cursoDto.Codigo))
                throw new BadRequestException("El código del curso es obligatorio.");

            // REGLA 2: Mínimo 1 crédito → 400
            if (cursoDto.Creditos < 1)
                throw new BadRequestException("El curso debe tener al menos 1 crédito.");

            var curso = _mapper.Map<Curso>(cursoDto);
            await _repository.AgregarAsync(curso);
            return _mapper.Map<CursoDto>(curso);
        }
    }
}
