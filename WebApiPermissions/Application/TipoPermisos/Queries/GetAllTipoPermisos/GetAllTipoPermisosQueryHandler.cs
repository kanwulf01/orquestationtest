using Application.DTOs;
using Application.Interfaces;
using Application.Permisos.Queries.GetAllProducts;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.TipoPermisos.Queries.GetAllTipoPermisos
{


    public class GetAllTipoPermisosQueryHandler : IRequestHandler<GetAllTipoPermisosQuery, IEnumerable<PermisosTypeDto>>
    {
        private readonly ITipoPermisosRepository _tipoPermisosRepository;
        private readonly IMapper _mapper;
        public GetAllTipoPermisosQueryHandler(ITipoPermisosRepository tipopermisosRepository, IMapper mapper)
        {
            _mapper = mapper;
            _tipoPermisosRepository = tipopermisosRepository;
        }

        public async Task<IEnumerable<PermisosTypeDto>> Handle(GetAllTipoPermisosQuery request, CancellationToken cancellationToken)
        {
            var permisos = await _tipoPermisosRepository.GetAllAsync(cancellationToken); ;

            return _mapper.Map<IEnumerable<PermisosTypeDto>>(permisos);
        }
    }
}
