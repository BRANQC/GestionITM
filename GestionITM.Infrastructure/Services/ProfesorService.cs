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

        public async Task<ProfesorDto?> ObtenerPorIdAsync(int id)
        {
            var profesor = await _repository.ObtenerPorIdAsync(id);
            if (profesor == null)
                throw new NotFoundException($"Profesor con ID {id} no encontrado.");

            return _mapper.Map<ProfesorDto>(profesor);
        }

        public async Task<ProfesorDto> RegistrarProfesorAsync(ProfesorCreateDto profesorDto)
        {
            // capturado por middleware como 500
            if (profesorDto.Nombre == "Error")
                throw new Exception("Error de prueba");

            // Solo correos institucionales del ITM → 400
            if (!profesorDto.Email.EndsWith("@itm.edu.co"))
                throw new BadRequestException("Solo se permiten correos institucionales del ITM (@itm.edu.co).");

            // Especialidad obligatoria → 400
            if (string.IsNullOrWhiteSpace(profesorDto.Especialidad))
                throw new BadRequestException("La especialidad del profesor es obligatoria.");

            // Perfil Senior
            if (profesorDto.Especialidad.Trim().Equals("Arquitectura", StringComparison.OrdinalIgnoreCase))
                Console.WriteLine(">>> Perfil Senior Detectado <<<");

            // Email único → 409 Conflict
            var emailEnUso = await _repository.ExisteEmailAsync(profesorDto.Email);
            if (emailEnUso)
                throw new ConflictException($"Ya existe un profesor registrado con el email '{profesorDto.Email}'.");

            var profesor = _mapper.Map<Profesor>(profesorDto);
            profesor.FechaContratacion = DateTime.UtcNow;

            await _repository.AgregarAsync(profesor);
            return _mapper.Map<ProfesorDto>(profesor);
        }
    }
}