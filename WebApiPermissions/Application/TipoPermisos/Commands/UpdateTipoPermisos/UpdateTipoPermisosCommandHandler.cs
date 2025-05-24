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

namespace Application.TipoPermisos.Commands.UpdateTipoPermisos
{

    public class UpdateTipoPermisosCommandHandler : IRequestHandler<UpdateTipoPermisosCommand, PermisosTypeDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateTipoPermisosCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PermisosTypeDto?> Handle(UpdateTipoPermisosCommand request, CancellationToken cancellationToken)
        {
            var tipoPermiso = await _unitOfWork.TipoPermisos.GetByIdAsync(request.Id, cancellationToken);

            if (tipoPermiso == null)
            {
                return null;
            }

            tipoPermiso.Update(
                request.Descripcion
             );

            await _unitOfWork.CompleteAsync(cancellationToken);

            return _mapper.Map<PermisosTypeDto>(tipoPermiso);
        }
    }
}
