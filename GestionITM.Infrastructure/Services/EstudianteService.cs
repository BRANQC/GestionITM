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
    public class EstudianteService : IEstudianteService
    {
        private readonly IEstudianteRepository _repository;
        private readonly IMapper _mapper;

        public EstudianteService(IEstudianteRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<EstudianteDto>> ObtenerTodosLosEstudiantesAsync()
        {
            var estudiantes = await _repository.ObtenerTodoAsync();
            return _mapper.Map<IEnumerable<EstudianteDto>>(estudiantes);
        }

        public async Task<EstudianteDto> RegistrarEstudianteAsync(EstudianteCreateDto estudianteDto)
        {
            // REGLA DE NEGOCIO: solo correos institucionales → 400 Bad Request
            if (!estudianteDto.Correo.EndsWith("@correo.itm.edu.co"))
                throw new BadRequestException("Solo se permiten correos institucionales (@correo.itm.edu.co).");

            var estudiante = _mapper.Map<Estudiante>(estudianteDto);
            estudiante.FechaInscripcion = DateTime.UtcNow;

            await _repository.AgregarAsync(estudiante);
            return _mapper.Map<EstudianteDto>(estudiante);
        }

        public async Task<EstudianteDto?> ObtenerPorIdAsync(int id)
        {
            var estudiante = await _repository.ObtenerPorIdAsync(id);
            if (estudiante == null)
                throw new NotFoundException($"Estudiante con ID {id} no encontrado.");

            return _mapper.Map<EstudianteDto>(estudiante);
        }
    }
}