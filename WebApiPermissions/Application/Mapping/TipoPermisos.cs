using Application.DTOs;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mapping
{
    public class TipoPermisosProfile : Profile
    {
        public TipoPermisosProfile() {

            CreateMap<TipoPermisosProfile, PermissionTypeDto>().ReverseMap();
        }
    }
}
