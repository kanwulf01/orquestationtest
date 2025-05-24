using Application.DTOs;
using Application.Interfaces;
using Application.Permisos.Commands.UpdatePermisos;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Permisos.Queries.GetAllProducts
{
    public class GetAllPermisosQueryHandler : IRequestHandler<GetAllPermisosQuery, IEnumerable<PermisosDto>>
    {
        private readonly IPermisosRepository _permisosRepository;
        private readonly IMapper _mapper;
        public GetAllPermisosQueryHandler(IPermisosRepository permisosRepository, IMapper mapper)
        {
            _mapper = mapper;
            _permisosRepository = permisosRepository;
        }

        public async Task<IEnumerable<PermisosDto>> Handle(GetAllPermisosQuery request, CancellationToken cancellationToken)
        {
            var permisos = await _permisosRepository.GetAllAsync(cancellationToken); ;

            return _mapper.Map<IEnumerable<PermisosDto>>(permisos);
        }
    }
}
