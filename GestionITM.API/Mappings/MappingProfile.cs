using AutoMapper;
using GestionITM.Domain.Dtos;
using GestionITM.Domain.Entities;

namespace GestionITM.API.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // ── Estudiante ──────────────────────────────────────
            CreateMap<Estudiante, EstudianteDto>();
            CreateMap<EstudianteCreateDto, Estudiante>();

            // ── Profesor ────────────────────────────────────────
            CreateMap<Profesor, ProfesorDto>();
            CreateMap<ProfesorCreateDto, Profesor>();
        }
    }
}