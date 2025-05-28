using Application.DTOs;
using AutoMapper;
using Domain.Entities;

namespace Application.Mapping
{
    public class PermisosProfile : Profile
    {
        public PermisosProfile() {

            CreateMap<Permiso, PermisosDto>()           
            .ForMember(dest => dest.TipoPermiso,
                       opt => opt.MapFrom(src => src.TipoPermiso));
            CreateMap<TipoPermiso, PermisosTypeDto>();

        }    
    }
}
