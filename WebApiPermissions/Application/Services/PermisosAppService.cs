using Application.DTOs;
using Application.Interfaces;
using Application.Permisos.Commands.CreatePermisos;
using Application.Permisos.Commands.UpdatePermisos;
using Application.Permisos.Queries.GetAllProducts;
using Application.Permisos.Queries.GetPermisosById;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class PermisosAppService: IPermisosAppService
    {
        private readonly IMediator _mediator;
        private readonly IKafkaServices _producer;

        public PermisosAppService(IMediator mediator, IKafkaServices producer)
        {
            _mediator = mediator;
            _producer = producer;
        }

        public async Task<IEnumerable<PermisosDto>> GetlllPermisos(CancellationToken cancellationToken) {
            var query = new GetAllPermisosQuery();

            var evt = new
            {
                Id = Guid.NewGuid(),
                NameOperation = "GET"
            };
            await _producer.ProduceAsync("permisos", evt);

            return await _mediator.Send(query, cancellationToken);
        }

        public async Task<PermisosDto> CreatePermiso(CreatePermisosCommand command, CancellationToken cancellationToken)
        {
            var query = new GetAllPermisosQuery();

            var evt = new
            {
                Id = Guid.NewGuid(),
                NameOperation = "REQUEST"
            };
            await _producer.ProduceAsync("permisos", evt);

            return await _mediator.Send(command, cancellationToken);
        }

        public async Task<PermisosDto?> UpdatePermiso(UpdatePermisosCommand command, CancellationToken cancellationToken)
        {
            var query = new GetAllPermisosQuery();

            var evt = new
            {
                Id = Guid.NewGuid(),
                NameOperation = "UPDATE"
            };
            await _producer.ProduceAsync("permisos", evt);

            return await _mediator.Send(command, cancellationToken);
        }

        public async Task<PermisosDto?> GetPermisoById(int id, CancellationToken cancellationToken)
        {
            var query = new GetPermisosByIdQuery(id);

            var evt = new
            {
                Id = Guid.NewGuid(),
                NameOperation = "GET"
            };
            await _producer.ProduceAsync("permisos", evt);

            return await _mediator.Send(query, cancellationToken);

        }
    }
}
