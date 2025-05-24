using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using MediatR;


namespace Application.TipoPermisos.Queries.GetTipoPermisoById
{
    public class GetTipoPermisoByIdQueryHandler: IRequestHandler<GetTipoPermisoByIdQuery, PermisosTypeDto?>
    {
        private readonly IMapper _mapper;
        private readonly ITipoPermisosRepository _repository;

        public GetTipoPermisoByIdQueryHandler(IMapper mapper, ITipoPermisosRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }   

        public async Task<PermisosTypeDto?> Handle(GetTipoPermisoByIdQuery request, CancellationToken cancellation)
        {
            var tipoPermiso = await _repository.GetByIdAsync(request.Id, cancellation);
            return tipoPermiso == null ? null : _mapper.Map<PermisosTypeDto>(tipoPermiso);
        }
    }
}
