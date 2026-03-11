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
    public class ProfesorService : IProfesorService
    {
        private readonly IProfesorRepository _repository;
        private readonly IMapper _mapper;

        public ProfesorService(IProfesorRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProfesorDto>> ObtenerTodosLosProfesoresAsync()
        {
            var profesores = await _repository.ObtenerTodoAsync();
            return _mapper.Map<IEnumerable<ProfesorDto>>(profesores);
        }

        public async Task<ProfesorDto> RegistrarProfesorAsync(ProfesorCreateDto profesorDto)
        {
            // --- RETO DE ROBUSTEZ: el middleware capturará esta excepción ---
            if (profesorDto.Nombre == "Error")
            {
                throw new Exception("Error de prueba");
            }

            // --- REGLA DE NEGOCIO 1: Especialidad no puede ser vacía ---
            if (string.IsNullOrWhiteSpace(profesorDto.Especialidad))
            {
                throw new ArgumentException("La especialidad del profesor es obligatoria.");
            }

            // --- REGLA DE NEGOCIO 2: Perfil Senior ---
            if (profesorDto.Especialidad.Trim().Equals("Arquitectura", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine(">>> Perfil Senior Detectado <<<");
            }

            // --- BONUS NIVEL 5: Email único ---
            var emailEnUso = await _repository.ExisteEmailAsync(profesorDto.Email);
            if (emailEnUso)
            {
                throw new ArgumentException($"Ya existe un profesor registrado con el email '{profesorDto.Email}'.");
            }

            // AutoMapper hace la conversión, sin asignaciones manuales
            var profesor = _mapper.Map<Profesor>(profesorDto);
            profesor.FechaContratacion = DateTime.UtcNow;

            await _repository.AgregarAsync(profesor);

            return _mapper.Map<ProfesorDto>(profesor);
        }
    }
}