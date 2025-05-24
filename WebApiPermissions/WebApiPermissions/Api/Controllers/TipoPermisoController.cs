using Application.DTOs;
using Application.Permisos.Commands.CreatePermisos;
using Application.Permisos.Commands.UpdatePermisos;
using Application.Permisos.Queries.GetAllProducts;
using Application.Permisos.Queries.GetPermisosById;
using Application.TipoPermisos.Commands.CreateTipoPermisos;
using Application.TipoPermisos.Commands.UpdateTipoPermisos;
using Application.TipoPermisos.Queries.GetAllTipoPermisos;
using Application.TipoPermisos.Queries.GetTipoPermisoById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApiPermissions.Api.Controllers
{


    [ApiController]
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    public class TipoPermisoController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TipoPermisoController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<PermisosTypeDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllTipoPermisos(CancellationToken cancellationToken)
        {
            var query = new GetAllTipoPermisosQuery();
            var tipoPermisos = await _mediator.Send(query, cancellationToken);
            return Ok(tipoPermisos);
        }

        [HttpPost]
        [ProducesResponseType(typeof(PermisosTypeDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateTipoPermiso([FromBody] CreateTipoPermisosCommand command, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var tipoPermisoDto = await _mediator.Send(command, cancellationToken);
            return Ok(tipoPermisoDto);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(PermisosTypeDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetTipoPermisoById(int id, CancellationToken cancellationToken)
        {
            var query = new GetTipoPermisoByIdQuery(id);
            var tipoPermiso = await _mediator.Send(query, cancellationToken);
            return tipoPermiso != null ? Ok(tipoPermiso) : NotFound();
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(typeof(PermisosTypeDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] UpdateTipoPermisosCommand command, CancellationToken cancellationToken)
        {
            if (id != command.Id)
            {
                return BadRequest("ID en la ruta no coincide con el ID en el cuerpo de la solicitud.");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updatedTipoPermisoDto = await _mediator.Send(command, cancellationToken);
            return updatedTipoPermisoDto != null ? Ok(updatedTipoPermisoDto) : NotFound();
        }

    }
}
