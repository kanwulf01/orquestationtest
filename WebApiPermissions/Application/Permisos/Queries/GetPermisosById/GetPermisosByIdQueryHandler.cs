using Application.DTOs;
using Application.Interfaces;
using Application.TipoPermisos.Queries.GetAllTipoPermisos;
using Application.TipoPermisos.Queries.GetTipoPermisoById;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Permisos.Queries.GetPermisosById
{


    public class GetPermisosByIdQueryHandler : IRequestHandler<GetPermisosByIdQuery, PermisosDto?>
    {
        private readonly IPermisosRepository _permisosRepository;
        private readonly IMapper _mapper;
        public GetPermisosByIdQueryHandler(IPermisosRepository permisosRepository, IMapper mapper)
        {
            _mapper = mapper;
            _permisosRepository = permisosRepository;
        }

        public async Task<PermisosDto?> Handle(GetPermisosByIdQuery request, CancellationToken cancellation)
        {
            var permiso = await _permisosRepository.GetByIdAsync(request.Id, cancellation);
            return permiso == null ? null : _mapper.Map<PermisosDto>(permiso);
        }
    }
}
