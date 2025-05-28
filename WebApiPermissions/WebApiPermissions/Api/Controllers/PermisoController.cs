using Application.DTOs;
using Application.Interfaces;
using Application.Permisos.Commands.CreatePermisos;
using Application.Permisos.Commands.UpdatePermisos;
using Application.Permisos.Queries.GetAllProducts;
using Application.Permisos.Queries.GetPermisosById;
using Infrastructure.Messaging;
using MediatR;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;

namespace WebApiPermissions.Api.Controllers
{

    [ApiController]
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    public class PermisoController: ControllerBase
    {
        private readonly IKafkaServices _producer;
        private readonly IPermisosAppService _permisosAppService;
        private readonly IConfiguration _configuration;

        public PermisoController(IPermisosAppService permisosAppService, IConfiguration configuration) 
        {
            _permisosAppService = permisosAppService;
            _configuration = configuration;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<PermisosDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllPermisos(CancellationToken cancellationToken)
        {
            var query = new GetAllPermisosQuery();
            var permisos = await _permisosAppService.GetlllPermisos(cancellationToken);
            return Ok(permisos);
        }

        [HttpPost]
        [ProducesResponseType(typeof(PermisosDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreatePermiso([FromBody] CreatePermisosCommand command, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var permisoDto = await _permisosAppService.CreatePermiso(command, cancellationToken);
            return Ok(permisoDto);
        }


        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(PermisosDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetPermisoById(int id, CancellationToken cancellationToken)
        {
            var permiso = await _permisosAppService.GetPermisoById(id, cancellationToken);
            return permiso != null ? Ok(permiso) :NotFound();
        }

        [HttpPut]
        [ProducesResponseType(typeof(PermisosDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateProduct([FromBody] UpdatePermisosCommand command, CancellationToken cancellationToken)
        {
           
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updatedPermisoDto = await _permisosAppService.UpdatePermiso(command, cancellationToken);
            return updatedPermisoDto != null ? Ok(updatedPermisoDto) : NotFound();
        }

        //Test Kafka Producer

        [HttpPost("publish")]
        public async Task<IActionResult> PublishMessage([FromBody] MessageRequest request)
        {
            await _producer.ProduceAsync(request.Topic, request.Content);
            return Ok(new { Message = "Mensaje enviado correctamente" });
        }

        public record MessageRequest(string Topic, string Content);
    }
}
