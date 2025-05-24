using Application.DTOs;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mapping
{
    public class PermisosProfile : Profile
    {
        public PermisosProfile() {

            CreateMap<PermisosProfile, PermisosDto>().ReverseMap();


        }    
    }
}
