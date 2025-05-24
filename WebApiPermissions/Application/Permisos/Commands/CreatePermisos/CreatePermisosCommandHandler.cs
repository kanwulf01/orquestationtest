using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;


namespace Application.Permisos.Commands.CreatePermisos
{
    public class CreatePermisosCommandHandler : IRequestHandler<CreatePermisosCommand, PermisosDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreatePermisosCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PermisosDto> Handle(CreatePermisosCommand request, CancellationToken cancellation)
        {
            var permiso = Permiso.Create(
                request.Id,
                request.NombreEmpleado,
                request.ApellidoEmpleado,
                request.TipoPermiso,
                request.FechaPermiso
                );

            await _unitOfWork.Permisos.AddAsync( permiso, cancellation );
            await _unitOfWork.CompleteAsync(cancellation);

            return _mapper.Map<PermisosDto>( permiso );
        }
    }
}
