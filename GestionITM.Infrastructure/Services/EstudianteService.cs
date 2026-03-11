using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using GestionITM.Domain.Dtos;
using GestionITM.Domain.Entities;
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

        public async Task<bool> RegistrarEstudianteAsync(EstudianteCreateDto estudianteDto)
        {
            //Reglas de negocio para validar el estudiante Nivel 5
            // No permitimos correos que no sean del dominio @itm.edu.co
            if (!estudianteDto.Correo.EndsWith("@correo.itm.edu.co"))
            {
                return false; // No se permite registrar el estudiante
            }

            var estudiante = _mapper.Map<Estudiante>(estudianteDto);
            estudiante.FechaInscripcion = DateTime.UtcNow; // Asignamos la fecha de inscripción actual

            await _repository.AgregarAsync(estudiante);
            return true; // Estudiante registrado exitosamente
        }

        public async Task<EstudianteDto?> ObtenerPorIdAsync(int id)
        {
            var estudiante = await _repository.ObtenerPorIdAsync(id);
            if (estudiante == null)
            {
                return null;
            }

            return _mapper.Map<EstudianteDto>(estudiante);
        }
    }
}
