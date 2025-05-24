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

namespace Application.Permisos.Commands.UpdatePermisos
{
    public class UpdatePermisosCommandHandler: IRequestHandler<UpdatePermisosCommand, PermisosDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdatePermisosCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PermisosDto?> Handle(UpdatePermisosCommand request, CancellationToken cancellationToken)
        {
            var permiso = await _unitOfWork.Permisos.GetByIdAsync(request.Id, cancellationToken);

            if (permiso == null)
            {
                return null;
            }

            permiso.Update(
                request.NombreEmpleado,
                request.ApellidoEmpleado,
                request.TipoPermiso,
                request.FechaPermiso
             );

            await _unitOfWork.CompleteAsync(cancellationToken);

            return _mapper.Map<PermisosDto>(permiso);
        }
    }
}
