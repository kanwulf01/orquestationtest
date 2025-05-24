using Application.DTOs;
using Application.Interfaces;
using Application.Permisos.Commands.CreatePermisos;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.TipoPermisos.Commands.CreateTipoPermisos
{

    public class CreateTipoPermisosCommandHandler : IRequestHandler<CreateTipoPermisosCommand, PermisosTypeDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateTipoPermisosCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PermisosTypeDto> Handle(CreateTipoPermisosCommand request, CancellationToken cancellation)
        {
            var tipoPermiso = TipoPermiso.Create(
                request.Id,
                request.Descripcion
                );

            await _unitOfWork.TipoPermisos.AddAsync(tipoPermiso, cancellation);
            await _unitOfWork.CompleteAsync(cancellation);
            return _mapper.Map<PermisosTypeDto>(tipoPermiso);
        }
    }
}
